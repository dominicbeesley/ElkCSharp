# ElkCSharp
An Experimental C# Emulator

## Emulation notes
In most emulations (at least the ones I've seen) the emulation proceeds either 
instruction-by-instruction or groups of instructions and then hardware is updated 
to "catch up". This makes for a more efficient emulation but makes exploring tricky
timing issues more difficult. As this emulator is more of a simulator for exploring
hardware (SCSI/Blitter/ULA) I have decided to make this emulator update hardware on
each 2MHz cycle - this may change if I run into performance problems later but
so far on a 7 year old i7 machine the emulator runs at ~380fps.

## CPU
The 6502 CPU emulation is based loosely on the MAME 6502 processors. The Makehxx.pl 
script creates the C# class bodies for the emulation from a text based representation 
of the cpu's microcode. The perl script inspects the code and turns it into C#
source-code. The code is split up into a number of methods, one per cpu cycle - this
is done by spotting read/write calls in the microcode. 

The cpu class then executes these microcode steps, one per tick. At the end of each 
tick the generated code sets a (new to c#9) method pointer to point to the next
microcode step to by executed on the next tick.
After each microcode step has executed a read or write is performed by calling the
Read or Write function of the SysCpu object which is passed to the constructor.

## Threading
Once the ULA is functional I propose to experiment with having the graphics generation
code run in a separate thread. For this to work the separate thread will need to 
maintain its own copies of the ULA registers and RAM. Writes to the ULA/RAM will be 
passed from the CPU to the graphics thread(s) via a FIFO and each write will be 
timestamped to allow the writes to be aligned with the raster etc.
