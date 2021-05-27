#!/usr/bin/perl

use strict;

my $DEBUG=0;

my %ops = ();
my $cycindex;
my $whileindex;


sub usage($) {
	my ($msg) = @_;
	die "USAGE: makehxx.pl <namespace> <classname> [<o.lst>|-] [<d.lst>|-] <out.cs>\n\n$msg\n";
}


my ($namespace, $classname, $infn_olst, $infn_dlst, $outfn_cxx) = @ARGV;

$namespace || usage "missing namespace";
$classname || usage "missing classname";

my $fop_cxx = 0;

open(my $fh_out_cxx, ">", $outfn_cxx) || usage "cannot open output file $outfn_cxx : $!";
$fop_cxx  = 1;

print $fh_out_cxx "// This file has been automatically produced by makehxx.pl\n";
print $fh_out_cxx "// do not edit it.\n";
print $fh_out_cxx "// from file(s) $infn_dlst $infn_olst\n";	

print $fh_out_cxx "namespace $namespace {\n";
print $fh_out_cxx "\tpublic partial class $classname {\n";


if ($infn_olst ne '-')
{
	do_o_lst();
}

if ($infn_dlst ne '-')
{
	do_d_lst();
}

print $fh_out_cxx "\t}\n";
print $fh_out_cxx "}\n";

sub archprefix($) {
	my ($i) = @_;

	my $class = "m6502_device";
	if ($i =~ /^(\w+):(\w+)/) {
		my $pre = $1;
		$i = $2;
		if ($pre eq "c")
		{
			$class = "m65c02_device";
		} elsif ($pre eq "ce") {
			$class = "m65ce02_device";
		} else {
			die "unrecognized prefix \"$pre:\"";
		}
	}
	return "${class}_${i}";
}

sub do_d_lst() {

	open(my $fh_in_dlst, "<", $infn_dlst) || usage "cannot open input file $infn_dlst : $!";

	print $fh_out_cxx "\t\tprotected override void postfetch_int() {\n";
	print $fh_out_cxx "\t\t\tswitch(IR) {\n";

	my $lin = 0;

	while (<$fh_in_dlst>) {

		my $l = $_;
		$l =~ s/[\s\r]+$//;
		$l =~ s/^\s+//;
		if ($l =~ /^#/) {
			#print $fh_out_cxx "$l\n";
		} elsif ($l =~ /^.+$/) {

			if ($lin < 16) {
				my @insts = split(/\s+/, $l);
				my $n = scalar(@insts);
				$n == 16 || die "There must be exactly 16 instructions on each line 1..16 ($n)";

				my $ix = $lin * 16;
				foreach my $i (@insts) {
					printf $fh_out_cxx "\t\t\t\tcase 0x%2.2x: %s_0(); break;\n", $ix, archprefix($i);
					$ix++;
				}

			} elsif ($lin == 16) {
				my @insts = split(/\s+/, $l);
				my $n = scalar(@insts);
				$n == 1 || die "There must be exactly 1 instructions on line 17 ($n)";
				printf $fh_out_cxx "\t\t\t\tdefault:   %s_0(); break;\n", archprefix(@insts[0]);


			} else {
				die "Error reading $fh_in_dlst more than 17 lines of data";
			}

			$lin++;
		} 
	}

	print $fh_out_cxx "\t\t}\n";
	print $fh_out_cxx "\t}\n\n";
}


sub do_o_lst() {
	open(my $fh_in_olst, "<", $infn_olst) || usage "cannot open input file $infn_olst : $!";
	my $RE_COMMENT = qr/^\s*#(.*)/;
	my $RE_EAT = qr/eat_all_cycles/;
	my $RE_MEM = qr/(read|write|(pre|post)?fetch(_noirq)?\()/;
	my $RE_NEWINST = qr/^(\w+)(.*)/;
	my $RE_READ = qr/^(.*)\bread\s*(\((([^()]|(?2))*)\))(.*)$/;
	my $RE_WRITE = qr/^(.*)\b
	write
	 \s*\(
	   ([^\(,]*((\((?:[^()]|(?4))*\))[^,\)]*)*)
	 ,
	   ([^\(,]*((\((?:[^()]|(?6))*\))[^,\)]*)*)
	\s*
	\)\s*;
	\s*
	(.*)
	$/x;
	my $RE_IF = qr/^(.*)\b
	(if|while)\s*(
		\(
			(
				(?:[^()]|(?3))*

			)
		\)
	)\s*\{\s*$
	/x;
	my $RE_ELSE = qr/\s*}\s*else\s*{\s*/;


	my $RE_PREFETCH = qr/^\s*((pre|post)?fetch(_noirq)?)\s*\(\s*\)\s*;\s*$/;


	my $cur_op;

	while (<$fh_in_olst>) {

		my $l = $_;
		$l =~ s/[\r\n\s]+$//;

		if ($l =~ $RE_COMMENT) {
			$DEBUG >= 3 && print "//$1\n";
		} elsif ($l =~ $RE_NEWINST) {
			
			end_inst($cur_op);
			
			$cur_op = { 
				name => $1			
			};
			$2 && die "unexpected trailing after instruction start $1[$2]";
		} elsif ($l =~ /^(\s+)(.*?)\s*(\/\/.*)?$/ ) {
			my $in = $1, $l = $2;		
			if ($l =~ $RE_READ) {
				my $b4 = $1;
				my $a = $3;
				my $t = $5;

				$b4 =~ /\b(read|write)\b/ && die "nested reads/writes";
				$t =~ /\b(read|write)\b/ && die "nested reads/writes";

				my @seq = (
						{ type => 'op', text => "ADDR = (ushort)($a);" },
						{ type => 'op', text => "RNW = true;" },
						{ type => 'cyc', text => "READ" }
					);
				

				if ($b4 =~ /[^\s]/ || $t =~ /[^\s;]/) {
					push @seq, { type=> 'op', text => "${b4}DAT${t}"};				
				}

				$DEBUG >= 3 && print_insts(${in}, \@seq);

				push @{$cur_op->{insts}}, @seq;

			} elsif ($l =~ $RE_WRITE) {
				my $b4 = $1;
				my $a = $2;
				my $d = $5;
				my $t = $8;

				$b4 =~ /\b(read|write)\b/ && die "nested reads/writes";
				$t =~ /\b(read|write)\b/ && die "nested reads/writes";
				$b4 =~ /^\s*$/ || die "writes must be on own line";
				$t =~ /^\s*$/ || die "writes must be on own line";

				my @seq = (
					{ type => 'op', text => "ADDR = (ushort)($a);" },
					{ type => 'op', text => "DAT = $d;" },
					{ type => 'op', text => "RNW = false;" },
					{ type => 'cyc', text => "WRITE" }
				);

				if ($b4 =~ /[^\s]/ || $t =~ /[^\s;]/) {
					push @seq, { type => 'cy', text => "${b4}DAT${t}"};
				}

				$DEBUG >= 3 && print_insts(${in}, \@seq);

				push @{$cur_op->{insts}}, @seq;

			} elsif ($l =~ $RE_IF) {

				my $b4 = $1;
				my $op = $2;
				my $cond = $4;

				$1 =~ /^\s*$/ || die "$op must be first item on line";

				my @seq = (
					{ type => "$op", text => "$cond" }
					);

				$DEBUG >= 3 && print_insts(${in}, \@seq);

				push @{$cur_op->{insts}}, @seq;
			} elsif ($l =~ $RE_ELSE) {

				my @seq = (
					{ type => "}" },
					{ type => "else" }
					);

				$DEBUG >= 3 && print_insts(${in}, \@seq);

				push @{$cur_op->{insts}}, @seq;
			} elsif ($l =~ /^\s*}\s*$/ ) {

				my @seq = (
					{ type => "}" }
					);

				$DEBUG >= 3 && print_insts(${in}, \@seq);

				push @{$cur_op->{insts}}, @seq;
			} elsif ($l =~ $RE_PREFETCH) {

				my @seq = (
					{ type => 'cyc', text => $1 }
					);

				$DEBUG >= 3 && print_insts(${in}, \@seq);

				push @{$cur_op->{insts}}, @seq;
			} elsif (!($l =~ /^\s*$/)) {

				$l =~ /\b(write|read|(pre)?fetch|eat_all_cycles|if|else|while)\b/ && die "uncaptured read/write/if/while/prefetch/eat_all_cycles";
				$l =~ /[\{\}]/ && die "uncaptured block start/end";
				$l =~ /;.*?;/ && die "more than one statement on a line";

				my @seq = (
					{ type => 'op', text => "$l" }
					);

				$DEBUG >= 3 && print_insts(${in}, \@seq);

				push @{$cur_op->{insts}}, @seq;
			}
		}
	}

	end_inst($cur_op);

	# make into a network of cycles, if, else etc as below,
	#	
	#	inst -> inst -> cycle -> if 	-true-> inst -> cycle -> inst ...
	#				 	-else-> inst -> cycle -> inst ...
	#					-next-> inst -> inst -> cycle -> inst ...
	#
	#	inst -> while -true-> inst....
	#		      -next-> inst....
	# can be nested
	# each cycle is given a number starting 1

	$DEBUG >= 2 && print "making neworks";


	foreach my $k (sort(keys(%ops))) {
		$DEBUG >=2 && print " --- op $k --- \n";

		my $op = $ops{$k};
		my $opname = $op->{name};
		my @flatinsts = @{$op->{insts}};

		my $ix = 0;
		$cycindex = 0;
		$whileindex = 0;
		my $rest = maketree($k, $ix, undef, \@flatinsts);

		$rest || $rest->{next} || die "no instructions for $k";
		$rest->{term} eq "end" || die "bad nesting for $k";

		$op->{start} = $rest->{next};

	}


	if ($DEBUG >= 2) {
		foreach my $k (sort(keys(%ops))) {
			print " --- op $k --- \n";

			my $op = $ops{$k};
			my $opname = $op->{name};
			
			showtree(0, $op->{start});
		}
	}


	foreach my $k (sort(keys(%ops))) {
		print $fh_out_cxx "// --- op $k --- \n";

		my $op = $ops{$k};
		my $opname = $op->{name};

		my @blockstack = (
			{
				curinst => $op->{start},
				cycindex => 0,
				indent => 1
			}
		);		#blocks that need to be expanded later
		my %already = ();
		
		while (my $cur = shift @blockstack) {
			emit($k, $cur, 0, \@blockstack, \%already);
		}

		
	}

}



sub appendtree($$) {
	my ($list, $add) = @_;

	if (!$list) {
		return $add;
	}
	while ($list && $list->{next}) {
		$list = $list->{next};
	}
	$list->{next} = $add;
	return $list;
}

sub maketree($$$@) {
	my ($op, $ix, $parent, $insts) = @_;

	my $curinst = @{$insts}[$ix];


	if ($curinst) {
		$curinst->{parent} = $parent;
		$DEBUG >= 2 && printf "%4d {%s}\n", $ix, $curinst->{type};
		if ($curinst->{type} eq "if") {			
			$DEBUG >= 2 && print "if\n";
			my $rest_true = maketree($op, $ix+1, $curinst, $insts);
			$rest_true && $rest_true->{term} eq "}" || die "if block not terminated in $op";
			$curinst->{if_start} = $rest_true->{next};
			my $cont = $rest_true->{cont};
			if (@{$insts}[$cont] && @{$insts}[$cont]->{type} eq "else") {
				my $rest_else = maketree($op, $cont+1, $curinst, $insts);
				$rest_else && $rest_else->{term} eq "}" || die "else block not terminated in $op";
				$curinst->{else_start} = $rest_else->{next};
				$cont = $rest_else->{cont};
			}
			my $rest = maketree($op, $cont, $parent, $insts);
			$curinst->{next} = $rest->{next};
			return { term => $rest->{term}, cont => $rest->{cont}, next => $curinst };
		} elsif ($curinst->{type} eq "else") {
			die "else without if in $op";
		} elsif ($curinst->{type} eq "cyc") {
			$DEBUG >= 2 && print "cyc\n";
			$curinst->{cycindex} = ++$cycindex;
			my $rest = maketree($op, $ix+1, $parent, $insts);
			$curinst->{next} = $rest->{next};
			return { term => $rest->{term}, cont => $rest->{cont}, next => $curinst };		
		} elsif ($curinst->{type} eq "while") {
			$DEBUG >= 2 && print "while\n";	
			$curinst->{whileindex} = ++$whileindex;		
			my $rest_true = maketree($op, $ix+1, $curinst, $insts);
			$rest_true && $rest_true->{term} eq "}" || die "if block not terminated in $op";
			$rest_true - appendtree($rest_true, {type=>'wend', whileindex=>$whileindex, text=>'wend'});
			$curinst->{while_inner} = $rest_true->{next};
			my $cont = $rest_true->{cont};
			my $rest = maketree($op, $cont, $parent, $insts);
			$curinst->{next} = $rest->{next};
			return { term => $rest->{term}, cont => $rest->{cont}, next => $curinst };
		} elsif ($curinst->{type} eq "}") {
			$DEBUG >= 2 && print "}\n";			
			return { term => '}', cont => ($ix+1) };
		} else {
			$DEBUG >= 2 && print "other\n";			
			my $rest = maketree($op, $ix+1, $parent, $insts);
			$curinst->{next} = $rest->{next};
			return { term => $rest->{term}, cont => $rest->{cont}, next => $curinst };
		}	
	} else {
		return { term => 'end' };
	}
}


sub showtree($$) {
	my ($indent, $i) = @_;

	if ($i->{type} eq "if") {
		printf "%sif (%s)\n", "  " x $indent, $i->{text};
		if ($i->{if_start}) {
			showtree($indent+1, $i->{if_start});
		}
		printf "%s%s", "  " x $indent, "}";
		if ($i->{else_start}) {
			print " else {\n";
			showtree($indent+1, $i->{else_start});
			printf "%s%s\n", "  " x $indent, "}";
		}
		print "\n";
	} elsif ($i->{type} eq "while") {
		printf "%swhile (%s)\n", "  " x $indent, $i->{text};
		if ($i->{while_inner}) {
			showtree($indent+1, $i->{while_inner});
		}
	} else {
		printf "%s%s\n", "  " x $indent, $i->{text};
	}

	if ($i->{next}) {
		showtree($indent, $i->{next});
	}
}

sub expand_members($) {
	my ($l) = @_;
	$l =~ s/(?!(true|false|int8_t|uint8_t))(\b[A-Z_]\w*\b)/\2/gi;
	return $l;
}

sub emit($$$@%) 
{
	my ($k, $cur, $fnopen, $blockstack, $already) = @_;

	my $curinst = $cur->{curinst};
	my $cycindex = $cur->{cycindex};
	my $whileindex = $cur->{whileindex};
	my $whilenotindex = $cur->{whilenotindex};
	my $indent = $cur->{indent};
	my $parent = $curinst->{parent};
	my $maxparent = $cur->{maxparent};

	if (!$fnopen) {
		if ($whilenotindex) {
			print $fh_out_cxx "protected void ${classname}_${k}_whilenot_${whilenotindex}() {\n";
		}
		elsif ($whileindex) {
			print $fh_out_cxx "protected void ${classname}_${k}_while_${whileindex}() {\n";
			print $fh_out_cxx "  if (!(${\ expand_members($parent->{text}) })) ${classname}_${k}_whilenot_${whileindex}();return;\n";
		} else {
			print $fh_out_cxx "protected void ${classname}_${k}_${cycindex}() {\n";
		}

		$already->{$cycindex} = 1;
	}

	while ($curinst) {

		if ($curinst->{type} eq "if") {
			print $fh_out_cxx  "  " x $indent . "if (${\expand_members($curinst->{text})}) {\n";			
			emit($k, 
				{ 
					curinst => $curinst->{if_start}, 
					cycindex => $cycindex, 
					indent => $indent+1,
					maxparent => $curinst

				}, 1, $blockstack, $already);
			if ($curinst->{else_start})
			{
				print $fh_out_cxx  "  " x $indent . "} else {\n";			
				emit($k, 
					{ 
						curinst => $curinst->{else_start}, 
						cycindex => $cycindex, 
						indent => $indent+1,
						maxparent => $curinst
					}, 1, $blockstack, $already);
			}
			print $fh_out_cxx  "  " x $indent . "}\n";			
		} elsif ($curinst->{type} eq "while") {

			my $whileindex = $curinst->{whileindex};

			push @{$blockstack}, {
				curinst => $curinst->{while_inner},
				whileindex => $whileindex,
				indent => $indent
			};
			my $next_inst = next_inst($curinst, undef);
			if ($next_inst) {
				push @{$blockstack}, {
					curinst => $next_inst,
					whilenotindex => $whileindex,
					indent => $indent
				};
			}

			print $fh_out_cxx  "  " x $indent . "${classname}_${k}_while_$whileindex();return;\n";
			if (!$fnopen) {
				print $fh_out_cxx "}\n\n";
			}
			return;

		} elsif ($curinst->{type} eq 'wend') {
			my $whileindex = $curinst->{whileindex};
			print $fh_out_cxx  "  " x $indent . "${classname}_${k}_while_$whileindex();return;\n";
			if (!$fnopen) {
				print $fh_out_cxx "}\n\n";
			}
			return;
		} elsif ($curinst->{type} eq "cyc") {
			$cycindex =  $curinst->{cycindex};
			if (!($curinst->{text} =~ /^(READ|WRITE)$/)) {
				if ($curinst->{text} =~ /^post/) {
					print $fh_out_cxx  "  " x $indent . "m6502_device_$curinst->{text}();return; // $curinst->{text}\n";	
				} else {
					if ($curinst->{text} =~ /^pre/) {
						print $fh_out_cxx  "  " x $indent . "PrefetchNextFn = ${classname}_${k}_${cycindex};\n";
					}
					print $fh_out_cxx  "  " x $indent . "m6502_device_$curinst->{text}();return; // $curinst->{text}\n";
				}
			} else {
				print $fh_out_cxx  "  " x $indent . "NextFn = ${classname}_${k}_${cycindex};return; // $curinst->{text}\n";
			}
			if (!$fnopen) {
				print $fh_out_cxx "}\n\n";
			}

			my $next_inst = next_inst($curinst, undef);

			if ($next_inst && !already($cycindex, $already, $blockstack)) {
				push @{$blockstack}, {
					curinst => $next_inst,
					cycindex => $cycindex,
					indent => $indent
				};
			}
			return;
		} else {
			print $fh_out_cxx  "  " x $indent . "${\ expand_members($curinst->{text}) }\n";
		}

		$curinst = next_inst($curinst, $maxparent);
	}

	if (!$fnopen) {
		die "Error ${classname}_${k}_${cycindex} terminated without a cycle at end";
	}

}

sub next_inst($$) {
	my ($curinst,$maxparent) = @_;

	while ($curinst && !$curinst->{next} && $curinst->{parent} != $maxparent) {
		$curinst = $curinst->{parent};
	};

	if ($curinst) {
		return $curinst->{next};
	}

}

sub already ($$$) {
	my ($cycindex, $already, $blockstack) = @_;
	if ($already->{$cycindex}) {
		return 1;
	} else {
		foreach my $s (@{$blockstack}) {
			if ($s->{cycindex} == $cycindex) {
				return 1;
			}
		}
	}

	return 0;

}


sub end_inst($) 
{
	my ($cur_op) = @_;
	if ($cur_op) {
		$DEBUG >= 2 && print "$cur_op->{name}\n";
		$ops{$cur_op->{name}} = $cur_op;
	}
}

sub print_insts()
{
	my ($in, $seq) = @_;

	foreach my $i (@{$seq}) {
		printf "%4s%s%s\n", $i->{type}, $in, $i->{text};
	}
}

$SIG{__DIE__} = sub {
    $fop_cxx && unlink $outfn_cxx;
    CORE::die @_;
}