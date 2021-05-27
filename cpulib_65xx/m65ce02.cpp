// license:BSD-3-Clause
// copyright-holders:Olivier Galibert
/***************************************************************************

    m65ce02.c

    6502 with Z register and some more stuff

***************************************************************************/

#include "m65ce02.h"


m65ce02_device::m65ce02_device() :
	m65c02_device()
{
}


void m65ce02_device::init()
{
	m65c02_device::init();
	Z = 0x00;
	B = 0x0000;
	TMP3 = 0x0000;
}

void m65ce02_device::reset()
{
	m65c02_device::reset();
	Z = 0x00;
	B = 0x0000;
}

