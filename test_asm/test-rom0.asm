; (c) Dossytronics 2017
; test harness ROM for VHDL testbench for MEMC mk2
; makes a 4k ROM


vec_nmi		:=	$D00

		.ZEROPAGE
ZP_PTR:		.RES 2

		.CODE






mos_handle_res:
		ldx	#$10
		lda	$20FF,X
		lda	$20E0,X

		bit	10


		ldx	#$FF
		txs

		lda	#$40
		sta	vec_nmi

here:		txa
		inx
		bne	here
		iny
		bne	here
		jmp 	here

mos_handle_irq:
		rti

		.SEGMENT "VECTORS"
hanmi:  .addr   vec_nmi                         
hares:  .addr   mos_handle_res                  
hairq:  .addr   mos_handle_irq                  

		.END
