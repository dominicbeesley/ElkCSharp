// license:BSD-3-Clause
// copyright-holders:Olivier Galibert
/***************************************************************************

    m65ce02.h

    6502 with Z register and some more stuff

***************************************************************************/
#ifndef MAME_CPU_M6502_M65CE02_H
#define MAME_CPU_M6502_M65CE02_H

#pragma once

#include "m65c02.h"

class m65ce02_device;

#include "m65ce02_top.gen.h"

class m65ce02_device : public m65c02_device {
public:
	m65ce02_device();

protected:
	uint16_t  TMP3;                   /* temporary internal values */
	uint8_t   Z;                      /* Z index register */
	uint16_t  B;                      /* Zero page base address (always xx00) */

	virtual void init() override;
	virtual void reset() override;

	inline void dec_SP_ce() { if(P & F_E) SP = set_l(SP, SP-1); else SP--; }
	inline void inc_SP_ce() { if(P & F_E) SP = set_l(SP, SP+1); else SP++; }

	#include "m65ce02.gen.h"

private:

};

enum {
	M65CE02_IRQ_LINE = m6502_device::IRQ_LINE,
	M65CE02_NMI_LINE = m6502_device::NMI_LINE
};



#endif // MAME_CPU_M6502_M65CE02_H
