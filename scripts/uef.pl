#!/bin/perl

use strict;

# create or add a file to a UEF

$#ARGV == 4 or die "Incorrect arguments: uef <filename.uef> <filetoadd> <start> <exec> <name on tape>$#ARGV";

my $ueffilename = @ARGV[0];
my $datafilename = @ARGV[1];
my $start = hex(@ARGV[2]);
my $exex = hex(@ARGV[3]);
my $tapename = @ARGV[4];

my $fh;

if (-e $ueffilename) {
	open($fh, "+<", $ueffilename) or die "Cannot open uef $!";
} else {
	open($fh, "+>", $ueffilename) or die "Cannot open uef $!";
}
binmode $fh;
my $header;
my $ll = read $fh, $header, 10;
if (
	(($ll == 10) && ($header ne "UEF File!\0")) 
||	($ll !=0 && $ll != 10)
) {
	die "Not an uncompressed uef file #$header# $ll";
} 

if ($ll == 0) {
	print $fh pack "Z10 S", "UEF File!", 6;
} 

seek $fh, 0, 2;

open (my $fhdat, "<", $datafilename) or die "Cannot open $datafilename for input $!";
my $p = 0;
my $data = '';
while (1) {
	my $l = read($fhdat, $data, 256, length($data));
	defined($l) or die "Error reading from file $!";
	$l or last;
}
close $fhdat;

printf "DATA LEN:%d\n", length($data); 

my $blk = 0;
while (length($data) || !$blk) {

	if ($blk == 0) {
		# carrier for 1500/1200s 
		print $fh pack("S L S", 0x110, 2, 0x5DC);
		# dummy byte
		print $fh pack("S L C", 0x100, 1, 0xAA);
		# carrier for 1500/1200s 
		print $fh pack("S L S", 0x110, 2, 0x5DC);
	} else {
		# carrier for 600/1200s 
		print $fh pack("S L S", 0x110, 2, 0x258);		
	}

	my $blockdata = substr($data, 0, 256);
	my $blockpacked = '';

	my $flags = ((length($data) <= 256)?0x80:0) | ((length($blockdata) == 0)?0x40:0);

	$blockpacked .= pack("C", 0x2A);

	my $hdr = $tapename . pack("C L L S S C L", 0, $start, $exex, $blk, length($blockdata), $flags, 0);

	my $crc_hdr = crc16($hdr);

	$blockpacked .= $hdr;	
	$blockpacked .= pack("C C", $crc_hdr >> 8, $crc_hdr & 0xFF);
	if (length($blockdata)) {
		my $crc_dat = crc16($blockdata);
		$blockpacked .= $blockdata;
		$blockpacked .= pack("C C", $crc_dat >> 8, $crc_dat & 0xFF);
	}

	print $fh pack("S L", 0x100, length($blockpacked));
	print $fh $blockpacked;

	$data = substr($data, 256);
	$blk++;

}

#printf "CHECK:%04X\n", crc16("123456789");

sub crc16 {
   my ($string) = @_;
   my $crc = 0;
   for my $c ( unpack 'C*', $string ) {
      $crc ^= $c << 8;
      for ( 0 .. 7 ) {
         my $t = 0;
         if ($crc & 0x8000) {
         	$crc ^= 0x810;
         	$t = 1;
         }
         $crc = ($crc * 2 + $t) & 0xFFFF;
      }
   }
   return $crc;
}