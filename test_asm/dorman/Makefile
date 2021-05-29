.PHONY:		all clean

ASMTARGETS=	6502_functional_test.bin 6502_interrupt_test.bin
TARGETS=	$(ASMTARGETS) 

INCS=		report.i65
LIBS=		

#BLIT_LIB_OBJS=	blit_lib.o blit_lib_ctl_full.o blit_lib_copy.o blit_lib__rd_bloc_be.o blit_lib_plot.o

AS=ca65
LD=ld65
DEPS=
AR=ar65

all:		$(TARGETS)

%.mos:	%.cfg
%.rom:	%.cfg

#blit.lib:	$(BLIT_LIB_OBJS)
#		rm -f blit.lib
#		$(AR) r blit.lib $(BLIT_LIB_OBJS)


%.bin:		%.o $(LIBS)
		$(LD) -vm -Ln $(basename $@).sy2 -m $(basename $@).map -o $@ -C dorman.cfg $< $(LIBS)
		#../../scripts/getsymbols.pl <$(basename $@).sy2 >$(basename $@).sym.noi
		da65 -S 0x0000 --comments 4 $(basename $@).bin >$(basename $@).da.s


%.o:		%.a65 $(DEPS) $(INCS)
		$(AS) -g -l $(basename $@).lst $<


clean:		$(clean_extra)
		$(foreach cln,$(TARGETS), \
		rm -f $(cln) $(basename $(cln)).lst $(basename $(cln)).sym $(basename $(cln)).map $(basename $(cln)).hex $(basename $(cln)).o $(basename $(cln)).da.s \
		)
		rm -f *.o
		rm -f *.rom
		rm -f *.bin
		rm -f *.mos



