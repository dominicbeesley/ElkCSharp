// This file has been automatically produced by makehxx.pl
// do not edit it.
// from file(s) ../textdefs/dm6502.lst ../textdefs/om6502.lst
namespace cpulib_65xx {
	public unsafe partial class m6502_device {
// --- op adc_aba --- 
protected static void m6502_device_adc_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_adc_aba_1;return; // READ
}

protected static void m6502_device_adc_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_adc_aba_2;return; // READ
}

protected static void m6502_device_adc_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_adc_aba_3;return; // READ
}

protected static void m6502_device_adc_aba_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_adc((byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op adc_abx --- 
protected static void m6502_device_adc_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_adc_abx_1;return; // READ
}

protected static void m6502_device_adc_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_adc_abx_2;return; // READ
}

protected static void m6502_device_adc_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._x)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
    cpu.NextFn = &m6502_device_adc_abx_3;return; // READ
  }
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_adc_abx_4;return; // READ
}

protected static void m6502_device_adc_abx_3(m6502_device cpu) {
    cpu._tmp += cpu._x;
    cpu._doread((ushort)(cpu._tmp));
    cpu.NextFn = &m6502_device_adc_abx_4;return; // READ
}

protected static void m6502_device_adc_abx_4(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_adc((byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op adc_aby --- 
protected static void m6502_device_adc_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_adc_aby_1;return; // READ
}

protected static void m6502_device_adc_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_adc_aby_2;return; // READ
}

protected static void m6502_device_adc_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_adc_aby_3;return; // READ
  }
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_adc_aby_4;return; // READ
}

protected static void m6502_device_adc_aby_3(m6502_device cpu) {
    cpu._tmp += cpu._y;
    cpu._doread((ushort)(cpu._tmp));
    cpu.NextFn = &m6502_device_adc_aby_4;return; // READ
}

protected static void m6502_device_adc_aby_4(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_adc((byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op adc_idx --- 
protected static void m6502_device_adc_idx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_adc_idx_1;return; // READ
}

protected static void m6502_device_adc_idx_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_adc_idx_2;return; // READ
}

protected static void m6502_device_adc_idx_2(m6502_device cpu) {
  cpu._tmp2 += cpu._x;
  cpu._doread((ushort)(cpu._tmp2 & 0xff));
  cpu.NextFn = &m6502_device_adc_idx_3;return; // READ
}

protected static void m6502_device_adc_idx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_adc_idx_4;return; // READ
}

protected static void m6502_device_adc_idx_4(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_adc_idx_5;return; // READ
}

protected static void m6502_device_adc_idx_5(m6502_device cpu) {
  cpu.do_adc(cpu._dat);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op adc_idy --- 
protected static void m6502_device_adc_idy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_adc_idy_1;return; // READ
}

protected static void m6502_device_adc_idy_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_adc_idy_2;return; // READ
}

protected static void m6502_device_adc_idy_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_adc_idy_3;return; // READ
}

protected static void m6502_device_adc_idy_3(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_adc_idy_4;return; // READ
  }
  cpu._doread((ushort)(cpu._tmp+cpu._y));
  cpu.NextFn = &m6502_device_adc_idy_5;return; // READ
}

protected static void m6502_device_adc_idy_4(m6502_device cpu) {
    cpu._doread((ushort)(cpu._tmp+cpu._y));
    cpu.NextFn = &m6502_device_adc_idy_5;return; // READ
}

protected static void m6502_device_adc_idy_5(m6502_device cpu) {
  cpu.do_adc(cpu._dat);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op adc_imm --- 
protected static void m6502_device_adc_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_adc_imm_1;return; // READ
}

protected static void m6502_device_adc_imm_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_adc((byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op adc_zpg --- 
protected static void m6502_device_adc_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_adc_zpg_1;return; // READ
}

protected static void m6502_device_adc_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_adc_zpg_2;return; // READ
}

protected static void m6502_device_adc_zpg_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_adc((byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op adc_zpx --- 
protected static void m6502_device_adc_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_adc_zpx_1;return; // READ
}

protected static void m6502_device_adc_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_adc_zpx_2;return; // READ
}

protected static void m6502_device_adc_zpx_2(m6502_device cpu) {
  cpu._doread((ushort)((byte)(cpu._tmp+cpu._x)));
  cpu.NextFn = &m6502_device_adc_zpx_3;return; // READ
}

protected static void m6502_device_adc_zpx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_adc((byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op anc_imm --- 
protected static void m6502_device_anc_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_anc_imm_1;return; // READ
}

protected static void m6502_device_anc_imm_1(m6502_device cpu) {
  cpu._a &= cpu._dat;
  cpu.set_nz(cpu._a);
  if ((cpu._a & 0x80) != 0) {
    cpu._p |= F_C;
  } else {
    cpu._p &= F_C ^ 0xFF;
  }
  m6502_device_fetch(cpu);return; // fetch
}

// --- op and_aba --- 
protected static void m6502_device_and_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_and_aba_1;return; // READ
}

protected static void m6502_device_and_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_and_aba_2;return; // READ
}

protected static void m6502_device_and_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_and_aba_3;return; // READ
}

protected static void m6502_device_and_aba_3(m6502_device cpu) {
  cpu._a &= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op and_abx --- 
protected static void m6502_device_and_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_and_abx_1;return; // READ
}

protected static void m6502_device_and_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_and_abx_2;return; // READ
}

protected static void m6502_device_and_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._x)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
    cpu.NextFn = &m6502_device_and_abx_3;return; // READ
  }
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_and_abx_4;return; // READ
}

protected static void m6502_device_and_abx_3(m6502_device cpu) {
    cpu._tmp += cpu._x;
    cpu._doread((ushort)(cpu._tmp));
    cpu.NextFn = &m6502_device_and_abx_4;return; // READ
}

protected static void m6502_device_and_abx_4(m6502_device cpu) {
  cpu._a &= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op and_aby --- 
protected static void m6502_device_and_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_and_aby_1;return; // READ
}

protected static void m6502_device_and_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_and_aby_2;return; // READ
}

protected static void m6502_device_and_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_and_aby_3;return; // READ
  }
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_and_aby_4;return; // READ
}

protected static void m6502_device_and_aby_3(m6502_device cpu) {
    cpu._tmp += cpu._y;
    cpu._doread((ushort)(cpu._tmp));
    cpu.NextFn = &m6502_device_and_aby_4;return; // READ
}

protected static void m6502_device_and_aby_4(m6502_device cpu) {
  cpu._a &= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op and_idx --- 
protected static void m6502_device_and_idx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_and_idx_1;return; // READ
}

protected static void m6502_device_and_idx_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_and_idx_2;return; // READ
}

protected static void m6502_device_and_idx_2(m6502_device cpu) {
  cpu._tmp2 += cpu._x;
  cpu._doread((ushort)(cpu._tmp2 & 0xff));
  cpu.NextFn = &m6502_device_and_idx_3;return; // READ
}

protected static void m6502_device_and_idx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_and_idx_4;return; // READ
}

protected static void m6502_device_and_idx_4(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_and_idx_5;return; // READ
}

protected static void m6502_device_and_idx_5(m6502_device cpu) {
  cpu._a &= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op and_idy --- 
protected static void m6502_device_and_idy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_and_idy_1;return; // READ
}

protected static void m6502_device_and_idy_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_and_idy_2;return; // READ
}

protected static void m6502_device_and_idy_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_and_idy_3;return; // READ
}

protected static void m6502_device_and_idy_3(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_and_idy_4;return; // READ
  }
  cpu._doread((ushort)(cpu._tmp+cpu._y));
  cpu.NextFn = &m6502_device_and_idy_5;return; // READ
}

protected static void m6502_device_and_idy_4(m6502_device cpu) {
    cpu._doread((ushort)(cpu._tmp+cpu._y));
    cpu.NextFn = &m6502_device_and_idy_5;return; // READ
}

protected static void m6502_device_and_idy_5(m6502_device cpu) {
  cpu._a &= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op and_imm --- 
protected static void m6502_device_and_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_and_imm_1;return; // READ
}

protected static void m6502_device_and_imm_1(m6502_device cpu) {
  cpu._a &= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op and_zpg --- 
protected static void m6502_device_and_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_and_zpg_1;return; // READ
}

protected static void m6502_device_and_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_and_zpg_2;return; // READ
}

protected static void m6502_device_and_zpg_2(m6502_device cpu) {
  cpu._a &= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op and_zpx --- 
protected static void m6502_device_and_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_and_zpx_1;return; // READ
}

protected static void m6502_device_and_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_and_zpx_2;return; // READ
}

protected static void m6502_device_and_zpx_2(m6502_device cpu) {
  cpu._doread((ushort)((byte)(cpu._tmp+cpu._x)));
  cpu.NextFn = &m6502_device_and_zpx_3;return; // READ
}

protected static void m6502_device_and_zpx_3(m6502_device cpu) {
  cpu._a &= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ane_imm --- 
protected static void m6502_device_ane_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ane_imm_1;return; // READ
}

protected static void m6502_device_ane_imm_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._a &= (byte)(cpu._tmp2 & cpu._x);
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op arr_imm --- 
protected static void m6502_device_arr_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_arr_imm_1;return; // READ
}

protected static void m6502_device_arr_imm_1(m6502_device cpu) {
  cpu._a &= cpu._dat;
  cpu.do_arr();
  m6502_device_fetch(cpu);return; // fetch
}

// --- op asl_aba --- 
protected static void m6502_device_asl_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_asl_aba_1;return; // READ
}

protected static void m6502_device_asl_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_asl_aba_2;return; // READ
}

protected static void m6502_device_asl_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_asl_aba_3;return; // READ
}

protected static void m6502_device_asl_aba_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_asl_aba_4;return; // WRITE
}

protected static void m6502_device_asl_aba_4(m6502_device cpu) {
  cpu._tmp2 = cpu.do_asl(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_asl_aba_5;return; // WRITE
}

protected static void m6502_device_asl_aba_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op asl_abx --- 
protected static void m6502_device_asl_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_asl_abx_1;return; // READ
}

protected static void m6502_device_asl_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_asl_abx_2;return; // READ
}

protected static void m6502_device_asl_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
  cpu.NextFn = &m6502_device_asl_abx_3;return; // READ
}

protected static void m6502_device_asl_abx_3(m6502_device cpu) {
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_asl_abx_4;return; // READ
}

protected static void m6502_device_asl_abx_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_asl_abx_5;return; // WRITE
}

protected static void m6502_device_asl_abx_5(m6502_device cpu) {
  cpu._tmp2 = cpu.do_asl(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_asl_abx_6;return; // WRITE
}

protected static void m6502_device_asl_abx_6(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op asl_acc --- 
protected static void m6502_device_asl_acc_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_asl_acc_1;return; // READ
}

protected static void m6502_device_asl_acc_1(m6502_device cpu) {
  cpu._a = cpu.do_asl(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op asl_zpg --- 
protected static void m6502_device_asl_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_asl_zpg_1;return; // READ
}

protected static void m6502_device_asl_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_asl_zpg_2;return; // READ
}

protected static void m6502_device_asl_zpg_2(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_asl_zpg_3;return; // WRITE
}

protected static void m6502_device_asl_zpg_3(m6502_device cpu) {
  cpu._tmp2 = cpu.do_asl(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_asl_zpg_4;return; // WRITE
}

protected static void m6502_device_asl_zpg_4(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op asl_zpx --- 
protected static void m6502_device_asl_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_asl_zpx_1;return; // READ
}

protected static void m6502_device_asl_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_asl_zpx_2;return; // READ
}

protected static void m6502_device_asl_zpx_2(m6502_device cpu) {
  cpu._tmp = (byte)(cpu._tmp+cpu._x);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_asl_zpx_3;return; // READ
}

protected static void m6502_device_asl_zpx_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_asl_zpx_4;return; // WRITE
}

protected static void m6502_device_asl_zpx_4(m6502_device cpu) {
  cpu._tmp2 = cpu.do_asl(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_asl_zpx_5;return; // WRITE
}

protected static void m6502_device_asl_zpx_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op asr_imm --- 
protected static void m6502_device_asr_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_asr_imm_1;return; // READ
}

protected static void m6502_device_asr_imm_1(m6502_device cpu) {
  cpu._a &= cpu._dat;
  cpu._a = cpu.do_lsr(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op bcc_rel --- 
protected static void m6502_device_bcc_rel_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_bcc_rel_1;return; // READ
}

protected static void m6502_device_bcc_rel_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  if ((cpu._p & F_C) == 0) {
    cpu._doread((ushort)(cpu._pc));
    cpu.NextFn = &m6502_device_bcc_rel_2;return; // READ
  }
  m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_bcc_rel_2(m6502_device cpu) {
    if (cpu.page_changing(cpu._pc, (sbyte)(cpu._tmp))) {
      cpu._doread((ushort)(cpu.set_l(cpu._pc,(byte)(cpu._pc+(sbyte)(cpu._tmp)))));
      cpu.NextFn = &m6502_device_bcc_rel_3;return; // READ
    }
    cpu._pc = (ushort)(cpu._pc + (sbyte)(cpu._tmp));
    m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_bcc_rel_3(m6502_device cpu) {
      cpu._pc = (ushort)(cpu._pc + (sbyte)(cpu._tmp));
      m6502_device_fetch(cpu);return; // fetch
}

// --- op bcs_rel --- 
protected static void m6502_device_bcs_rel_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_bcs_rel_1;return; // READ
}

protected static void m6502_device_bcs_rel_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  if ((cpu._p & F_C) != 0) {
    cpu._doread((ushort)(cpu._pc));
    cpu.NextFn = &m6502_device_bcs_rel_2;return; // READ
  }
  m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_bcs_rel_2(m6502_device cpu) {
    if (cpu.page_changing(cpu._pc, (sbyte)(cpu._tmp))) {
      cpu._doread((ushort)(cpu.set_l(cpu._pc,(byte)(cpu._pc+(sbyte)(cpu._tmp)))));
      cpu.NextFn = &m6502_device_bcs_rel_3;return; // READ
    }
    cpu._pc = (ushort)(cpu._pc + (sbyte)(cpu._tmp));
    m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_bcs_rel_3(m6502_device cpu) {
      cpu._pc = (ushort)(cpu._pc + (sbyte)(cpu._tmp));
      m6502_device_fetch(cpu);return; // fetch
}

// --- op beq_rel --- 
protected static void m6502_device_beq_rel_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_beq_rel_1;return; // READ
}

protected static void m6502_device_beq_rel_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  if ((cpu._p & F_Z) != 0) {
    cpu._doread((ushort)(cpu._pc));
    cpu.NextFn = &m6502_device_beq_rel_2;return; // READ
  }
  m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_beq_rel_2(m6502_device cpu) {
    if (cpu.page_changing(cpu._pc, (sbyte)(cpu._tmp))) {
      cpu._doread((ushort)(cpu.set_l(cpu._pc,(byte)(cpu._pc+(sbyte)(cpu._tmp)))));
      cpu.NextFn = &m6502_device_beq_rel_3;return; // READ
    }
    cpu._pc = (ushort)(cpu._pc + (sbyte)(cpu._tmp));
    m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_beq_rel_3(m6502_device cpu) {
      cpu._pc = (ushort)(cpu._pc + (sbyte)(cpu._tmp));
      m6502_device_fetch(cpu);return; // fetch
}

// --- op bit_aba --- 
protected static void m6502_device_bit_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_bit_aba_1;return; // READ
}

protected static void m6502_device_bit_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_bit_aba_2;return; // READ
}

protected static void m6502_device_bit_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_bit_aba_3;return; // READ
}

protected static void m6502_device_bit_aba_3(m6502_device cpu) {
  cpu.do_bit(cpu._dat);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op bit_zpg --- 
protected static void m6502_device_bit_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_bit_zpg_1;return; // READ
}

protected static void m6502_device_bit_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_bit_zpg_2;return; // READ
}

protected static void m6502_device_bit_zpg_2(m6502_device cpu) {
  cpu.do_bit(cpu._dat);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op bmi_rel --- 
protected static void m6502_device_bmi_rel_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_bmi_rel_1;return; // READ
}

protected static void m6502_device_bmi_rel_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  if ((cpu._p & F_N) != 0) {
    cpu._doread((ushort)(cpu._pc));
    cpu.NextFn = &m6502_device_bmi_rel_2;return; // READ
  }
  m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_bmi_rel_2(m6502_device cpu) {
    if (cpu.page_changing(cpu._pc, (sbyte)(cpu._tmp))) {
      cpu._doread((ushort)(cpu.set_l(cpu._pc,(byte)(cpu._pc+(sbyte)(cpu._tmp)))));
      cpu.NextFn = &m6502_device_bmi_rel_3;return; // READ
    }
    cpu._pc = (ushort)(cpu._pc + (sbyte)(cpu._tmp));
    m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_bmi_rel_3(m6502_device cpu) {
      cpu._pc = (ushort)(cpu._pc + (sbyte)(cpu._tmp));
      m6502_device_fetch(cpu);return; // fetch
}

// --- op bne_rel --- 
protected static void m6502_device_bne_rel_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_bne_rel_1;return; // READ
}

protected static void m6502_device_bne_rel_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  if ((cpu._p & F_Z) == 0) {
    cpu._doread((ushort)(cpu._pc));
    cpu.NextFn = &m6502_device_bne_rel_2;return; // READ
  }
  m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_bne_rel_2(m6502_device cpu) {
    if (cpu.page_changing(cpu._pc, (sbyte)(cpu._tmp))) {
      cpu._doread((ushort)(cpu.set_l(cpu._pc,(byte)(cpu._pc+(sbyte)(cpu._tmp)))));
      cpu.NextFn = &m6502_device_bne_rel_3;return; // READ
    }
    cpu._pc = (ushort)(cpu._pc + (sbyte)(cpu._tmp));
    m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_bne_rel_3(m6502_device cpu) {
      cpu._pc = (ushort)(cpu._pc + (sbyte)(cpu._tmp));
      m6502_device_fetch(cpu);return; // fetch
}

// --- op bpl_rel --- 
protected static void m6502_device_bpl_rel_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_bpl_rel_1;return; // READ
}

protected static void m6502_device_bpl_rel_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  if ((cpu._p & F_N) == 0) {
    cpu._doread((ushort)(cpu._pc));
    cpu.NextFn = &m6502_device_bpl_rel_2;return; // READ
  }
  m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_bpl_rel_2(m6502_device cpu) {
    if (cpu.page_changing(cpu._pc, (sbyte)(cpu._tmp))) {
      cpu._doread((ushort)(cpu.set_l(cpu._pc,(byte)(cpu._pc+(sbyte)(cpu._tmp)))));
      cpu.NextFn = &m6502_device_bpl_rel_3;return; // READ
    }
    cpu._pc = (ushort)(cpu._pc + (sbyte)(cpu._tmp));
    m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_bpl_rel_3(m6502_device cpu) {
      cpu._pc = (ushort)(cpu._pc + (sbyte)(cpu._tmp));
      m6502_device_fetch(cpu);return; // fetch
}

// --- op brk_imp --- 
protected static void m6502_device_brk_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_brk_imp_1;return; // READ
}

protected static void m6502_device_brk_imp_1(m6502_device cpu) {
  if (!cpu.irq_taken) {
    cpu._pc++;
  }
  cpu._dowrite((ushort)(cpu._sp),(byte)( (byte)(cpu._pc >> 8)));
  cpu.NextFn = &m6502_device_brk_imp_2;return; // WRITE
}

protected static void m6502_device_brk_imp_2(m6502_device cpu) {
  cpu.dec_SP();
  cpu._dowrite((ushort)(cpu._sp),(byte)( (byte)cpu._pc));
  cpu.NextFn = &m6502_device_brk_imp_3;return; // WRITE
}

protected static void m6502_device_brk_imp_3(m6502_device cpu) {
  cpu.dec_SP();
  cpu._dowrite((ushort)(cpu._sp),(byte)( (byte)(cpu.irq_taken ? cpu._p & (F_B ^ 0xFF) : cpu._p)));
  cpu.NextFn = &m6502_device_brk_imp_4;return; // WRITE
}

protected static void m6502_device_brk_imp_4(m6502_device cpu) {
  cpu.dec_SP();
  if (cpu.nmi_state) {
    cpu._doread((ushort)(0xfffa));
    cpu.NextFn = &m6502_device_brk_imp_5;return; // READ
  } else {
    cpu._doread((ushort)(0xfffe));
    cpu.NextFn = &m6502_device_brk_imp_7;return; // READ
  }
  cpu.irq_taken = false;
  cpu._p |= F_I;
  m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_brk_imp_5(m6502_device cpu) {
    cpu._pc = cpu._dat;
    cpu._doread((ushort)(0xfffb));
    cpu.NextFn = &m6502_device_brk_imp_6;return; // READ
}

protected static void m6502_device_brk_imp_7(m6502_device cpu) {
    cpu._pc = cpu._dat;
    cpu._doread((ushort)(0xffff));
    cpu.NextFn = &m6502_device_brk_imp_8;return; // READ
}

protected static void m6502_device_brk_imp_6(m6502_device cpu) {
    cpu._pc = cpu.set_h(cpu._pc, cpu._dat);
    cpu.nmi_state = false;
    cpu.irq_taken = false;
    cpu._p |= F_I;
    m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_brk_imp_8(m6502_device cpu) {
    cpu._pc = cpu.set_h(cpu._pc, cpu._dat);
    cpu.irq_taken = false;
    cpu._p |= F_I;
    m6502_device_fetch(cpu);return; // fetch
}

// --- op bvc_rel --- 
protected static void m6502_device_bvc_rel_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_bvc_rel_1;return; // READ
}

protected static void m6502_device_bvc_rel_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  if ((cpu._p & F_V) == 0) {
    cpu._doread((ushort)(cpu._pc));
    cpu.NextFn = &m6502_device_bvc_rel_2;return; // READ
  }
  m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_bvc_rel_2(m6502_device cpu) {
    if (cpu.page_changing(cpu._pc, (sbyte)(cpu._tmp))) {
      cpu._doread((ushort)(cpu.set_l(cpu._pc,(byte)(cpu._pc+(sbyte)(cpu._tmp)))));
      cpu.NextFn = &m6502_device_bvc_rel_3;return; // READ
    }
    cpu._pc = (ushort)(cpu._pc + (sbyte)(cpu._tmp));
    m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_bvc_rel_3(m6502_device cpu) {
      cpu._pc = (ushort)(cpu._pc + (sbyte)(cpu._tmp));
      m6502_device_fetch(cpu);return; // fetch
}

// --- op bvs_rel --- 
protected static void m6502_device_bvs_rel_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_bvs_rel_1;return; // READ
}

protected static void m6502_device_bvs_rel_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  if ((cpu._p & F_V) != 0) {
    cpu._doread((ushort)(cpu._pc));
    cpu.NextFn = &m6502_device_bvs_rel_2;return; // READ
  }
  m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_bvs_rel_2(m6502_device cpu) {
    if (cpu.page_changing(cpu._pc, (sbyte)(cpu._tmp))) {
      cpu._doread((ushort)(cpu.set_l(cpu._pc,(byte)(cpu._pc+(sbyte)(cpu._tmp)))));
      cpu.NextFn = &m6502_device_bvs_rel_3;return; // READ
    }
    cpu._pc = (ushort)(cpu._pc + (sbyte)(cpu._tmp));
    m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_bvs_rel_3(m6502_device cpu) {
      cpu._pc = (ushort)(cpu._pc + (sbyte)(cpu._tmp));
      m6502_device_fetch(cpu);return; // fetch
}

// --- op clc_imp --- 
protected static void m6502_device_clc_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_clc_imp_1;return; // READ
}

protected static void m6502_device_clc_imp_1(m6502_device cpu) {
  cpu._p &= F_C ^ 0xFF;
  m6502_device_fetch(cpu);return; // fetch
}

// --- op cld_imp --- 
protected static void m6502_device_cld_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_cld_imp_1;return; // READ
}

protected static void m6502_device_cld_imp_1(m6502_device cpu) {
  cpu._p &= F_D ^ 0xFF;
  m6502_device_fetch(cpu);return; // fetch
}

// --- op cli_imp --- 
protected static void m6502_device_cli_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_cli_imp_1;return; // READ
}

protected static void m6502_device_cli_imp_1(m6502_device cpu) {
  cpu.PrefetchNextFn = &m6502_device_cli_imp_2;
  m6502_device_prefetch(cpu);return; // prefetch
}

protected static void m6502_device_cli_imp_2(m6502_device cpu) {
  cpu._p &= F_I ^ 0xFF;
  m6502_device_postfetch(cpu);return; // postfetch
}

// --- op clv_imp --- 
protected static void m6502_device_clv_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_clv_imp_1;return; // READ
}

protected static void m6502_device_clv_imp_1(m6502_device cpu) {
  cpu._p &= F_V ^ 0xFF;
  m6502_device_fetch(cpu);return; // fetch
}

// --- op cmp_aba --- 
protected static void m6502_device_cmp_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cmp_aba_1;return; // READ
}

protected static void m6502_device_cmp_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cmp_aba_2;return; // READ
}

protected static void m6502_device_cmp_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_cmp_aba_3;return; // READ
}

protected static void m6502_device_cmp_aba_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_cmp(cpu._a, (byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op cmp_abx --- 
protected static void m6502_device_cmp_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cmp_abx_1;return; // READ
}

protected static void m6502_device_cmp_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cmp_abx_2;return; // READ
}

protected static void m6502_device_cmp_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._x)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
    cpu.NextFn = &m6502_device_cmp_abx_3;return; // READ
  }
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_cmp_abx_4;return; // READ
}

protected static void m6502_device_cmp_abx_3(m6502_device cpu) {
    cpu._tmp += cpu._x;
    cpu._doread((ushort)(cpu._tmp));
    cpu.NextFn = &m6502_device_cmp_abx_4;return; // READ
}

protected static void m6502_device_cmp_abx_4(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_cmp(cpu._a, (byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op cmp_aby --- 
protected static void m6502_device_cmp_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cmp_aby_1;return; // READ
}

protected static void m6502_device_cmp_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cmp_aby_2;return; // READ
}

protected static void m6502_device_cmp_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_cmp_aby_3;return; // READ
  }
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_cmp_aby_4;return; // READ
}

protected static void m6502_device_cmp_aby_3(m6502_device cpu) {
    cpu._tmp += cpu._y;
    cpu._doread((ushort)(cpu._tmp));
    cpu.NextFn = &m6502_device_cmp_aby_4;return; // READ
}

protected static void m6502_device_cmp_aby_4(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_cmp(cpu._a, (byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op cmp_idx --- 
protected static void m6502_device_cmp_idx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cmp_idx_1;return; // READ
}

protected static void m6502_device_cmp_idx_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_cmp_idx_2;return; // READ
}

protected static void m6502_device_cmp_idx_2(m6502_device cpu) {
  cpu._tmp2 += cpu._x;
  cpu._doread((ushort)(cpu._tmp2 & 0xff));
  cpu.NextFn = &m6502_device_cmp_idx_3;return; // READ
}

protected static void m6502_device_cmp_idx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_cmp_idx_4;return; // READ
}

protected static void m6502_device_cmp_idx_4(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_cmp_idx_5;return; // READ
}

protected static void m6502_device_cmp_idx_5(m6502_device cpu) {
  cpu.do_cmp(cpu._a, cpu._dat);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op cmp_idy --- 
protected static void m6502_device_cmp_idy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cmp_idy_1;return; // READ
}

protected static void m6502_device_cmp_idy_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_cmp_idy_2;return; // READ
}

protected static void m6502_device_cmp_idy_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_cmp_idy_3;return; // READ
}

protected static void m6502_device_cmp_idy_3(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_cmp_idy_4;return; // READ
  }
  cpu._doread((ushort)(cpu._tmp+cpu._y));
  cpu.NextFn = &m6502_device_cmp_idy_5;return; // READ
}

protected static void m6502_device_cmp_idy_4(m6502_device cpu) {
    cpu._doread((ushort)(cpu._tmp+cpu._y));
    cpu.NextFn = &m6502_device_cmp_idy_5;return; // READ
}

protected static void m6502_device_cmp_idy_5(m6502_device cpu) {
  cpu.do_cmp(cpu._a, cpu._dat);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op cmp_imm --- 
protected static void m6502_device_cmp_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cmp_imm_1;return; // READ
}

protected static void m6502_device_cmp_imm_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_cmp(cpu._a, (byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op cmp_zpg --- 
protected static void m6502_device_cmp_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cmp_zpg_1;return; // READ
}

protected static void m6502_device_cmp_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_cmp_zpg_2;return; // READ
}

protected static void m6502_device_cmp_zpg_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_cmp(cpu._a, (byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op cmp_zpx --- 
protected static void m6502_device_cmp_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cmp_zpx_1;return; // READ
}

protected static void m6502_device_cmp_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_cmp_zpx_2;return; // READ
}

protected static void m6502_device_cmp_zpx_2(m6502_device cpu) {
  cpu._doread((ushort)((byte)(cpu._tmp+cpu._x)));
  cpu.NextFn = &m6502_device_cmp_zpx_3;return; // READ
}

protected static void m6502_device_cmp_zpx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_cmp(cpu._a, (byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op cpx_aba --- 
protected static void m6502_device_cpx_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cpx_aba_1;return; // READ
}

protected static void m6502_device_cpx_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cpx_aba_2;return; // READ
}

protected static void m6502_device_cpx_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_cpx_aba_3;return; // READ
}

protected static void m6502_device_cpx_aba_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_cmp(cpu._x, (byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op cpx_imm --- 
protected static void m6502_device_cpx_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cpx_imm_1;return; // READ
}

protected static void m6502_device_cpx_imm_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_cmp(cpu._x, (byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op cpx_zpg --- 
protected static void m6502_device_cpx_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cpx_zpg_1;return; // READ
}

protected static void m6502_device_cpx_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_cpx_zpg_2;return; // READ
}

protected static void m6502_device_cpx_zpg_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_cmp(cpu._x, (byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op cpy_aba --- 
protected static void m6502_device_cpy_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cpy_aba_1;return; // READ
}

protected static void m6502_device_cpy_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cpy_aba_2;return; // READ
}

protected static void m6502_device_cpy_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_cpy_aba_3;return; // READ
}

protected static void m6502_device_cpy_aba_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_cmp(cpu._y, (byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op cpy_imm --- 
protected static void m6502_device_cpy_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cpy_imm_1;return; // READ
}

protected static void m6502_device_cpy_imm_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_cmp(cpu._y, (byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op cpy_zpg --- 
protected static void m6502_device_cpy_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_cpy_zpg_1;return; // READ
}

protected static void m6502_device_cpy_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_cpy_zpg_2;return; // READ
}

protected static void m6502_device_cpy_zpg_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu.do_cmp(cpu._y, (byte)cpu._tmp);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op dcp_aba --- 
protected static void m6502_device_dcp_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_dcp_aba_1;return; // READ
}

protected static void m6502_device_dcp_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_dcp_aba_2;return; // READ
}

protected static void m6502_device_dcp_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_dcp_aba_3;return; // READ
}

protected static void m6502_device_dcp_aba_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dcp_aba_4;return; // WRITE
}

protected static void m6502_device_dcp_aba_4(m6502_device cpu) {
  cpu._tmp2--;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dcp_aba_5;return; // WRITE
}

protected static void m6502_device_dcp_aba_5(m6502_device cpu) {
  cpu.do_cmp(cpu._a, cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op dcp_abx --- 
protected static void m6502_device_dcp_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_dcp_abx_1;return; // READ
}

protected static void m6502_device_dcp_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_dcp_abx_2;return; // READ
}

protected static void m6502_device_dcp_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
  cpu.NextFn = &m6502_device_dcp_abx_3;return; // READ
}

protected static void m6502_device_dcp_abx_3(m6502_device cpu) {
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_dcp_abx_4;return; // READ
}

protected static void m6502_device_dcp_abx_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dcp_abx_5;return; // WRITE
}

protected static void m6502_device_dcp_abx_5(m6502_device cpu) {
  cpu._tmp2--;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dcp_abx_6;return; // WRITE
}

protected static void m6502_device_dcp_abx_6(m6502_device cpu) {
  cpu.do_cmp(cpu._a, cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op dcp_aby --- 
protected static void m6502_device_dcp_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_dcp_aby_1;return; // READ
}

protected static void m6502_device_dcp_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_dcp_aby_2;return; // READ
}

protected static void m6502_device_dcp_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_dcp_aby_3;return; // READ
}

protected static void m6502_device_dcp_aby_3(m6502_device cpu) {
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_dcp_aby_4;return; // READ
}

protected static void m6502_device_dcp_aby_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dcp_aby_5;return; // WRITE
}

protected static void m6502_device_dcp_aby_5(m6502_device cpu) {
  cpu._tmp2--;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dcp_aby_6;return; // WRITE
}

protected static void m6502_device_dcp_aby_6(m6502_device cpu) {
  cpu.do_cmp(cpu._a, cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op dcp_idx --- 
protected static void m6502_device_dcp_idx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_dcp_idx_1;return; // READ
}

protected static void m6502_device_dcp_idx_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_dcp_idx_2;return; // READ
}

protected static void m6502_device_dcp_idx_2(m6502_device cpu) {
  cpu._tmp2 += cpu._x;
  cpu._doread((ushort)(cpu._tmp2 & 0xff));
  cpu.NextFn = &m6502_device_dcp_idx_3;return; // READ
}

protected static void m6502_device_dcp_idx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_dcp_idx_4;return; // READ
}

protected static void m6502_device_dcp_idx_4(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_dcp_idx_5;return; // READ
}

protected static void m6502_device_dcp_idx_5(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dcp_idx_6;return; // WRITE
}

protected static void m6502_device_dcp_idx_6(m6502_device cpu) {
  cpu._tmp2--;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dcp_idx_7;return; // WRITE
}

protected static void m6502_device_dcp_idx_7(m6502_device cpu) {
  cpu.do_cmp(cpu._a, cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op dcp_idy --- 
protected static void m6502_device_dcp_idy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_dcp_idy_1;return; // READ
}

protected static void m6502_device_dcp_idy_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_dcp_idy_2;return; // READ
}

protected static void m6502_device_dcp_idy_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_dcp_idy_3;return; // READ
}

protected static void m6502_device_dcp_idy_3(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_dcp_idy_4;return; // READ
}

protected static void m6502_device_dcp_idy_4(m6502_device cpu) {
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_dcp_idy_5;return; // READ
}

protected static void m6502_device_dcp_idy_5(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dcp_idy_6;return; // WRITE
}

protected static void m6502_device_dcp_idy_6(m6502_device cpu) {
  cpu._tmp2--;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dcp_idy_7;return; // WRITE
}

protected static void m6502_device_dcp_idy_7(m6502_device cpu) {
  cpu.do_cmp(cpu._a, cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op dcp_zpg --- 
protected static void m6502_device_dcp_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_dcp_zpg_1;return; // READ
}

protected static void m6502_device_dcp_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_dcp_zpg_2;return; // READ
}

protected static void m6502_device_dcp_zpg_2(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dcp_zpg_3;return; // WRITE
}

protected static void m6502_device_dcp_zpg_3(m6502_device cpu) {
  cpu._tmp2--;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dcp_zpg_4;return; // WRITE
}

protected static void m6502_device_dcp_zpg_4(m6502_device cpu) {
  cpu.do_cmp(cpu._a, cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op dcp_zpx --- 
protected static void m6502_device_dcp_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_dcp_zpx_1;return; // READ
}

protected static void m6502_device_dcp_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_dcp_zpx_2;return; // READ
}

protected static void m6502_device_dcp_zpx_2(m6502_device cpu) {
  cpu._tmp = (byte)(cpu._tmp+cpu._x);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_dcp_zpx_3;return; // READ
}

protected static void m6502_device_dcp_zpx_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dcp_zpx_4;return; // WRITE
}

protected static void m6502_device_dcp_zpx_4(m6502_device cpu) {
  cpu._tmp2--;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dcp_zpx_5;return; // WRITE
}

protected static void m6502_device_dcp_zpx_5(m6502_device cpu) {
  cpu.do_cmp(cpu._a, cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op dec_aba --- 
protected static void m6502_device_dec_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_dec_aba_1;return; // READ
}

protected static void m6502_device_dec_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_dec_aba_2;return; // READ
}

protected static void m6502_device_dec_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_dec_aba_3;return; // READ
}

protected static void m6502_device_dec_aba_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dec_aba_4;return; // WRITE
}

protected static void m6502_device_dec_aba_4(m6502_device cpu) {
  cpu._tmp2--;
  cpu.set_nz(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dec_aba_5;return; // WRITE
}

protected static void m6502_device_dec_aba_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op dec_abx --- 
protected static void m6502_device_dec_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_dec_abx_1;return; // READ
}

protected static void m6502_device_dec_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_dec_abx_2;return; // READ
}

protected static void m6502_device_dec_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
  cpu.NextFn = &m6502_device_dec_abx_3;return; // READ
}

protected static void m6502_device_dec_abx_3(m6502_device cpu) {
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_dec_abx_4;return; // READ
}

protected static void m6502_device_dec_abx_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dec_abx_5;return; // WRITE
}

protected static void m6502_device_dec_abx_5(m6502_device cpu) {
  cpu._tmp2--;
  cpu.set_nz(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dec_abx_6;return; // WRITE
}

protected static void m6502_device_dec_abx_6(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op dec_zpg --- 
protected static void m6502_device_dec_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_dec_zpg_1;return; // READ
}

protected static void m6502_device_dec_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_dec_zpg_2;return; // READ
}

protected static void m6502_device_dec_zpg_2(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dec_zpg_3;return; // WRITE
}

protected static void m6502_device_dec_zpg_3(m6502_device cpu) {
  cpu._tmp2--;
  cpu.set_nz(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dec_zpg_4;return; // WRITE
}

protected static void m6502_device_dec_zpg_4(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op dec_zpx --- 
protected static void m6502_device_dec_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_dec_zpx_1;return; // READ
}

protected static void m6502_device_dec_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_dec_zpx_2;return; // READ
}

protected static void m6502_device_dec_zpx_2(m6502_device cpu) {
  cpu._tmp = (byte)(cpu._tmp+cpu._x);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_dec_zpx_3;return; // READ
}

protected static void m6502_device_dec_zpx_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dec_zpx_4;return; // WRITE
}

protected static void m6502_device_dec_zpx_4(m6502_device cpu) {
  cpu._tmp2--;
  cpu.set_nz(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_dec_zpx_5;return; // WRITE
}

protected static void m6502_device_dec_zpx_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op dex_imp --- 
protected static void m6502_device_dex_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_dex_imp_1;return; // READ
}

protected static void m6502_device_dex_imp_1(m6502_device cpu) {
  cpu._x--;
  cpu.set_nz(cpu._x);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op dey_imp --- 
protected static void m6502_device_dey_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_dey_imp_1;return; // READ
}

protected static void m6502_device_dey_imp_1(m6502_device cpu) {
  cpu._y--;
  cpu.set_nz(cpu._y);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op eor_aba --- 
protected static void m6502_device_eor_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_eor_aba_1;return; // READ
}

protected static void m6502_device_eor_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_eor_aba_2;return; // READ
}

protected static void m6502_device_eor_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_eor_aba_3;return; // READ
}

protected static void m6502_device_eor_aba_3(m6502_device cpu) {
  cpu._a ^= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op eor_abx --- 
protected static void m6502_device_eor_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_eor_abx_1;return; // READ
}

protected static void m6502_device_eor_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_eor_abx_2;return; // READ
}

protected static void m6502_device_eor_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._x)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
    cpu.NextFn = &m6502_device_eor_abx_3;return; // READ
  }
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_eor_abx_4;return; // READ
}

protected static void m6502_device_eor_abx_3(m6502_device cpu) {
    cpu._tmp += cpu._x;
    cpu._doread((ushort)(cpu._tmp));
    cpu.NextFn = &m6502_device_eor_abx_4;return; // READ
}

protected static void m6502_device_eor_abx_4(m6502_device cpu) {
  cpu._a ^= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op eor_aby --- 
protected static void m6502_device_eor_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_eor_aby_1;return; // READ
}

protected static void m6502_device_eor_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_eor_aby_2;return; // READ
}

protected static void m6502_device_eor_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_eor_aby_3;return; // READ
  }
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_eor_aby_4;return; // READ
}

protected static void m6502_device_eor_aby_3(m6502_device cpu) {
    cpu._tmp += cpu._y;
    cpu._doread((ushort)(cpu._tmp));
    cpu.NextFn = &m6502_device_eor_aby_4;return; // READ
}

protected static void m6502_device_eor_aby_4(m6502_device cpu) {
  cpu._a ^= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op eor_idx --- 
protected static void m6502_device_eor_idx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_eor_idx_1;return; // READ
}

protected static void m6502_device_eor_idx_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_eor_idx_2;return; // READ
}

protected static void m6502_device_eor_idx_2(m6502_device cpu) {
  cpu._tmp2 += cpu._x;
  cpu._doread((ushort)(cpu._tmp2 & 0xff));
  cpu.NextFn = &m6502_device_eor_idx_3;return; // READ
}

protected static void m6502_device_eor_idx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_eor_idx_4;return; // READ
}

protected static void m6502_device_eor_idx_4(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_eor_idx_5;return; // READ
}

protected static void m6502_device_eor_idx_5(m6502_device cpu) {
  cpu._a ^= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op eor_idy --- 
protected static void m6502_device_eor_idy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_eor_idy_1;return; // READ
}

protected static void m6502_device_eor_idy_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_eor_idy_2;return; // READ
}

protected static void m6502_device_eor_idy_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_eor_idy_3;return; // READ
}

protected static void m6502_device_eor_idy_3(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_eor_idy_4;return; // READ
  }
  cpu._doread((ushort)(cpu._tmp+cpu._y));
  cpu.NextFn = &m6502_device_eor_idy_5;return; // READ
}

protected static void m6502_device_eor_idy_4(m6502_device cpu) {
    cpu._doread((ushort)(cpu._tmp+cpu._y));
    cpu.NextFn = &m6502_device_eor_idy_5;return; // READ
}

protected static void m6502_device_eor_idy_5(m6502_device cpu) {
  cpu._a ^= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op eor_imm --- 
protected static void m6502_device_eor_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_eor_imm_1;return; // READ
}

protected static void m6502_device_eor_imm_1(m6502_device cpu) {
  cpu._a ^= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op eor_zpg --- 
protected static void m6502_device_eor_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_eor_zpg_1;return; // READ
}

protected static void m6502_device_eor_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_eor_zpg_2;return; // READ
}

protected static void m6502_device_eor_zpg_2(m6502_device cpu) {
  cpu._a ^= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op eor_zpx --- 
protected static void m6502_device_eor_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_eor_zpx_1;return; // READ
}

protected static void m6502_device_eor_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_eor_zpx_2;return; // READ
}

protected static void m6502_device_eor_zpx_2(m6502_device cpu) {
  cpu._doread((ushort)((byte)(cpu._tmp+cpu._x)));
  cpu.NextFn = &m6502_device_eor_zpx_3;return; // READ
}

protected static void m6502_device_eor_zpx_3(m6502_device cpu) {
  cpu._a ^= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op inc_aba --- 
protected static void m6502_device_inc_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_inc_aba_1;return; // READ
}

protected static void m6502_device_inc_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_inc_aba_2;return; // READ
}

protected static void m6502_device_inc_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_inc_aba_3;return; // READ
}

protected static void m6502_device_inc_aba_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_inc_aba_4;return; // WRITE
}

protected static void m6502_device_inc_aba_4(m6502_device cpu) {
  cpu._tmp2++;
  cpu.set_nz(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_inc_aba_5;return; // WRITE
}

protected static void m6502_device_inc_aba_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op inc_abx --- 
protected static void m6502_device_inc_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_inc_abx_1;return; // READ
}

protected static void m6502_device_inc_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_inc_abx_2;return; // READ
}

protected static void m6502_device_inc_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
  cpu.NextFn = &m6502_device_inc_abx_3;return; // READ
}

protected static void m6502_device_inc_abx_3(m6502_device cpu) {
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_inc_abx_4;return; // READ
}

protected static void m6502_device_inc_abx_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_inc_abx_5;return; // WRITE
}

protected static void m6502_device_inc_abx_5(m6502_device cpu) {
  cpu._tmp2++;
  cpu.set_nz(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_inc_abx_6;return; // WRITE
}

protected static void m6502_device_inc_abx_6(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op inc_zpg --- 
protected static void m6502_device_inc_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_inc_zpg_1;return; // READ
}

protected static void m6502_device_inc_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_inc_zpg_2;return; // READ
}

protected static void m6502_device_inc_zpg_2(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_inc_zpg_3;return; // WRITE
}

protected static void m6502_device_inc_zpg_3(m6502_device cpu) {
  cpu._tmp2++;
  cpu.set_nz(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_inc_zpg_4;return; // WRITE
}

protected static void m6502_device_inc_zpg_4(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op inc_zpx --- 
protected static void m6502_device_inc_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_inc_zpx_1;return; // READ
}

protected static void m6502_device_inc_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_inc_zpx_2;return; // READ
}

protected static void m6502_device_inc_zpx_2(m6502_device cpu) {
  cpu._tmp = (byte)(cpu._tmp+cpu._x);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_inc_zpx_3;return; // READ
}

protected static void m6502_device_inc_zpx_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_inc_zpx_4;return; // WRITE
}

protected static void m6502_device_inc_zpx_4(m6502_device cpu) {
  cpu._tmp2++;
  cpu.set_nz(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_inc_zpx_5;return; // WRITE
}

protected static void m6502_device_inc_zpx_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op inx_imp --- 
protected static void m6502_device_inx_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_inx_imp_1;return; // READ
}

protected static void m6502_device_inx_imp_1(m6502_device cpu) {
  cpu._x++;
  cpu.set_nz(cpu._x);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op iny_imp --- 
protected static void m6502_device_iny_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_iny_imp_1;return; // READ
}

protected static void m6502_device_iny_imp_1(m6502_device cpu) {
  cpu._y++;
  cpu.set_nz(cpu._y);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op isb_aba --- 
protected static void m6502_device_isb_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_isb_aba_1;return; // READ
}

protected static void m6502_device_isb_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_isb_aba_2;return; // READ
}

protected static void m6502_device_isb_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_isb_aba_3;return; // READ
}

protected static void m6502_device_isb_aba_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_isb_aba_4;return; // WRITE
}

protected static void m6502_device_isb_aba_4(m6502_device cpu) {
  cpu._tmp2++;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_isb_aba_5;return; // WRITE
}

protected static void m6502_device_isb_aba_5(m6502_device cpu) {
  cpu.do_sbc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op isb_abx --- 
protected static void m6502_device_isb_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_isb_abx_1;return; // READ
}

protected static void m6502_device_isb_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_isb_abx_2;return; // READ
}

protected static void m6502_device_isb_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
  cpu.NextFn = &m6502_device_isb_abx_3;return; // READ
}

protected static void m6502_device_isb_abx_3(m6502_device cpu) {
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_isb_abx_4;return; // READ
}

protected static void m6502_device_isb_abx_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_isb_abx_5;return; // WRITE
}

protected static void m6502_device_isb_abx_5(m6502_device cpu) {
  cpu._tmp2++;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_isb_abx_6;return; // WRITE
}

protected static void m6502_device_isb_abx_6(m6502_device cpu) {
  cpu.do_sbc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op isb_aby --- 
protected static void m6502_device_isb_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_isb_aby_1;return; // READ
}

protected static void m6502_device_isb_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_isb_aby_2;return; // READ
}

protected static void m6502_device_isb_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_isb_aby_3;return; // READ
}

protected static void m6502_device_isb_aby_3(m6502_device cpu) {
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_isb_aby_4;return; // READ
}

protected static void m6502_device_isb_aby_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_isb_aby_5;return; // WRITE
}

protected static void m6502_device_isb_aby_5(m6502_device cpu) {
  cpu._tmp2++;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_isb_aby_6;return; // WRITE
}

protected static void m6502_device_isb_aby_6(m6502_device cpu) {
  cpu.do_sbc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op isb_idx --- 
protected static void m6502_device_isb_idx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_isb_idx_1;return; // READ
}

protected static void m6502_device_isb_idx_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_isb_idx_2;return; // READ
}

protected static void m6502_device_isb_idx_2(m6502_device cpu) {
  cpu._tmp2 += cpu._x;
  cpu._doread((ushort)(cpu._tmp2 & 0xff));
  cpu.NextFn = &m6502_device_isb_idx_3;return; // READ
}

protected static void m6502_device_isb_idx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_isb_idx_4;return; // READ
}

protected static void m6502_device_isb_idx_4(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_isb_idx_5;return; // READ
}

protected static void m6502_device_isb_idx_5(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_isb_idx_6;return; // WRITE
}

protected static void m6502_device_isb_idx_6(m6502_device cpu) {
  cpu._tmp2++;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_isb_idx_7;return; // WRITE
}

protected static void m6502_device_isb_idx_7(m6502_device cpu) {
  cpu.do_sbc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op isb_idy --- 
protected static void m6502_device_isb_idy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_isb_idy_1;return; // READ
}

protected static void m6502_device_isb_idy_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_isb_idy_2;return; // READ
}

protected static void m6502_device_isb_idy_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_isb_idy_3;return; // READ
}

protected static void m6502_device_isb_idy_3(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_isb_idy_4;return; // READ
}

protected static void m6502_device_isb_idy_4(m6502_device cpu) {
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_isb_idy_5;return; // READ
}

protected static void m6502_device_isb_idy_5(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_isb_idy_6;return; // WRITE
}

protected static void m6502_device_isb_idy_6(m6502_device cpu) {
  cpu._tmp2++;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_isb_idy_7;return; // WRITE
}

protected static void m6502_device_isb_idy_7(m6502_device cpu) {
  cpu.do_sbc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op isb_zpg --- 
protected static void m6502_device_isb_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_isb_zpg_1;return; // READ
}

protected static void m6502_device_isb_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_isb_zpg_2;return; // READ
}

protected static void m6502_device_isb_zpg_2(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_isb_zpg_3;return; // WRITE
}

protected static void m6502_device_isb_zpg_3(m6502_device cpu) {
  cpu._tmp2++;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_isb_zpg_4;return; // WRITE
}

protected static void m6502_device_isb_zpg_4(m6502_device cpu) {
  cpu.do_sbc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op isb_zpx --- 
protected static void m6502_device_isb_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_isb_zpx_1;return; // READ
}

protected static void m6502_device_isb_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_isb_zpx_2;return; // READ
}

protected static void m6502_device_isb_zpx_2(m6502_device cpu) {
  cpu._tmp = (byte)(cpu._tmp+cpu._x);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_isb_zpx_3;return; // READ
}

protected static void m6502_device_isb_zpx_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_isb_zpx_4;return; // WRITE
}

protected static void m6502_device_isb_zpx_4(m6502_device cpu) {
  cpu._tmp2++;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_isb_zpx_5;return; // WRITE
}

protected static void m6502_device_isb_zpx_5(m6502_device cpu) {
  cpu.do_sbc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op jmp_adr --- 
protected static void m6502_device_jmp_adr_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_jmp_adr_1;return; // READ
}

protected static void m6502_device_jmp_adr_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_jmp_adr_2;return; // READ
}

protected static void m6502_device_jmp_adr_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._pc = cpu._tmp;
  m6502_device_fetch(cpu);return; // fetch
}

// --- op jmp_ind --- 
protected static void m6502_device_jmp_ind_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_jmp_ind_1;return; // READ
}

protected static void m6502_device_jmp_ind_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_jmp_ind_2;return; // READ
}

protected static void m6502_device_jmp_ind_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_jmp_ind_3;return; // READ
}

protected static void m6502_device_jmp_ind_3(m6502_device cpu) {
  cpu._pc = cpu._dat;
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+1))));
  cpu.NextFn = &m6502_device_jmp_ind_4;return; // READ
}

protected static void m6502_device_jmp_ind_4(m6502_device cpu) {
  cpu._pc = cpu.set_h(cpu._pc, cpu._dat);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op jsr_adr --- 
protected static void m6502_device_jsr_adr_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_jsr_adr_1;return; // READ
}

protected static void m6502_device_jsr_adr_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._sp));
  cpu.NextFn = &m6502_device_jsr_adr_2;return; // READ
}

protected static void m6502_device_jsr_adr_2(m6502_device cpu) {
  cpu._dowrite((ushort)(cpu._sp),(byte)( (byte)(cpu._pc>>8)));
  cpu.NextFn = &m6502_device_jsr_adr_3;return; // WRITE
}

protected static void m6502_device_jsr_adr_3(m6502_device cpu) {
  cpu.dec_SP();
  cpu._dowrite((ushort)(cpu._sp),(byte)( (byte)cpu._pc));
  cpu.NextFn = &m6502_device_jsr_adr_4;return; // WRITE
}

protected static void m6502_device_jsr_adr_4(m6502_device cpu) {
  cpu.dec_SP();
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_jsr_adr_5;return; // READ
}

protected static void m6502_device_jsr_adr_5(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._pc = cpu._tmp;
  m6502_device_fetch(cpu);return; // fetch
}

// --- op kil_non --- 
protected static void m6502_device_kil_non_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_kil_non_1;return; // READ
}

protected static void m6502_device_kil_non_1(m6502_device cpu) {
  cpu._doread((ushort)(0xffff));
  cpu.NextFn = &m6502_device_kil_non_2;return; // READ
}

protected static void m6502_device_kil_non_2(m6502_device cpu) {
  cpu._doread((ushort)(0xfffe));
  cpu.NextFn = &m6502_device_kil_non_3;return; // READ
}

protected static void m6502_device_kil_non_3(m6502_device cpu) {
  cpu._doread((ushort)(0xfffe));
  cpu.NextFn = &m6502_device_kil_non_4;return; // READ
}

protected static void m6502_device_kil_non_4(m6502_device cpu) {
  m6502_device_kil_non_while_1(cpu);return;
}

protected static void m6502_device_kil_non_while_1(m6502_device cpu) {
  if (!(true)) m6502_device_kil_non_whilenot_1(cpu);return;
  cpu._doread((ushort)(0xffff));
  cpu.NextFn = &m6502_device_kil_non_5;return; // READ
}

protected static void m6502_device_kil_non_whilenot_1(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

protected static void m6502_device_kil_non_5(m6502_device cpu) {
  m6502_device_kil_non_while_1(cpu);return;
}

// --- op las_aby --- 
protected static void m6502_device_las_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_las_aby_1;return; // READ
}

protected static void m6502_device_las_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_las_aby_2;return; // READ
}

protected static void m6502_device_las_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_las_aby_3;return; // READ
  }
  cpu._doread((ushort)(cpu._tmp+cpu._y));
  cpu.NextFn = &m6502_device_las_aby_4;return; // READ
}

protected static void m6502_device_las_aby_3(m6502_device cpu) {
    cpu._doread((ushort)(cpu._tmp+cpu._y));
    cpu.NextFn = &m6502_device_las_aby_4;return; // READ
}

protected static void m6502_device_las_aby_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._a = (byte)(cpu._tmp2 | 0x51);
  cpu._x = 0xff;
  cpu.set_nz(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lax_aba --- 
protected static void m6502_device_lax_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lax_aba_1;return; // READ
}

protected static void m6502_device_lax_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lax_aba_2;return; // READ
}

protected static void m6502_device_lax_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_lax_aba_3;return; // READ
}

protected static void m6502_device_lax_aba_3(m6502_device cpu) {
  cpu._a = cpu._x = cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lax_aby --- 
protected static void m6502_device_lax_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lax_aby_1;return; // READ
}

protected static void m6502_device_lax_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lax_aby_2;return; // READ
}

protected static void m6502_device_lax_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_lax_aby_3;return; // READ
  }
  cpu._doread((ushort)(cpu._tmp+cpu._y));
  cpu.NextFn = &m6502_device_lax_aby_4;return; // READ
}

protected static void m6502_device_lax_aby_3(m6502_device cpu) {
    cpu._doread((ushort)(cpu._tmp+cpu._y));
    cpu.NextFn = &m6502_device_lax_aby_4;return; // READ
}

protected static void m6502_device_lax_aby_4(m6502_device cpu) {
  cpu._a = cpu._x = cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lax_idx --- 
protected static void m6502_device_lax_idx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lax_idx_1;return; // READ
}

protected static void m6502_device_lax_idx_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_lax_idx_2;return; // READ
}

protected static void m6502_device_lax_idx_2(m6502_device cpu) {
  cpu._tmp2 += cpu._x;
  cpu._doread((ushort)(cpu._tmp2 & 0xff));
  cpu.NextFn = &m6502_device_lax_idx_3;return; // READ
}

protected static void m6502_device_lax_idx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_lax_idx_4;return; // READ
}

protected static void m6502_device_lax_idx_4(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_lax_idx_5;return; // READ
}

protected static void m6502_device_lax_idx_5(m6502_device cpu) {
  cpu._a = cpu._x = cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lax_idy --- 
protected static void m6502_device_lax_idy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lax_idy_1;return; // READ
}

protected static void m6502_device_lax_idy_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_lax_idy_2;return; // READ
}

protected static void m6502_device_lax_idy_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_lax_idy_3;return; // READ
}

protected static void m6502_device_lax_idy_3(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_lax_idy_4;return; // READ
  }
  cpu._doread((ushort)(cpu._tmp+cpu._y));
  cpu.NextFn = &m6502_device_lax_idy_5;return; // READ
}

protected static void m6502_device_lax_idy_4(m6502_device cpu) {
    cpu._doread((ushort)(cpu._tmp+cpu._y));
    cpu.NextFn = &m6502_device_lax_idy_5;return; // READ
}

protected static void m6502_device_lax_idy_5(m6502_device cpu) {
  cpu._a = cpu._x = cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lax_zpg --- 
protected static void m6502_device_lax_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lax_zpg_1;return; // READ
}

protected static void m6502_device_lax_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_lax_zpg_2;return; // READ
}

protected static void m6502_device_lax_zpg_2(m6502_device cpu) {
  cpu._a = cpu._x = cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lax_zpy --- 
protected static void m6502_device_lax_zpy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lax_zpy_1;return; // READ
}

protected static void m6502_device_lax_zpy_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_lax_zpy_2;return; // READ
}

protected static void m6502_device_lax_zpy_2(m6502_device cpu) {
  cpu._tmp = (byte)(cpu._tmp+cpu._y);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_lax_zpy_3;return; // READ
}

protected static void m6502_device_lax_zpy_3(m6502_device cpu) {
  cpu._a = cpu._x = cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lda_aba --- 
protected static void m6502_device_lda_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lda_aba_1;return; // READ
}

protected static void m6502_device_lda_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lda_aba_2;return; // READ
}

protected static void m6502_device_lda_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_lda_aba_3;return; // READ
}

protected static void m6502_device_lda_aba_3(m6502_device cpu) {
  cpu._a = cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lda_abx --- 
protected static void m6502_device_lda_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lda_abx_1;return; // READ
}

protected static void m6502_device_lda_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lda_abx_2;return; // READ
}

protected static void m6502_device_lda_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._x)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
    cpu.NextFn = &m6502_device_lda_abx_3;return; // READ
  }
  cpu._doread((ushort)(cpu._tmp + cpu._x));
  cpu.NextFn = &m6502_device_lda_abx_4;return; // READ
}

protected static void m6502_device_lda_abx_3(m6502_device cpu) {
    cpu._doread((ushort)(cpu._tmp + cpu._x));
    cpu.NextFn = &m6502_device_lda_abx_4;return; // READ
}

protected static void m6502_device_lda_abx_4(m6502_device cpu) {
  cpu._a = cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lda_aby --- 
protected static void m6502_device_lda_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lda_aby_1;return; // READ
}

protected static void m6502_device_lda_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lda_aby_2;return; // READ
}

protected static void m6502_device_lda_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_lda_aby_3;return; // READ
  }
  cpu._doread((ushort)(cpu._tmp + cpu._y));
  cpu.NextFn = &m6502_device_lda_aby_4;return; // READ
}

protected static void m6502_device_lda_aby_3(m6502_device cpu) {
    cpu._doread((ushort)(cpu._tmp + cpu._y));
    cpu.NextFn = &m6502_device_lda_aby_4;return; // READ
}

protected static void m6502_device_lda_aby_4(m6502_device cpu) {
  cpu._a = cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lda_idx --- 
protected static void m6502_device_lda_idx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lda_idx_1;return; // READ
}

protected static void m6502_device_lda_idx_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_lda_idx_2;return; // READ
}

protected static void m6502_device_lda_idx_2(m6502_device cpu) {
  cpu._tmp2 += cpu._x;
  cpu._doread((ushort)(cpu._tmp2 & 0xff));
  cpu.NextFn = &m6502_device_lda_idx_3;return; // READ
}

protected static void m6502_device_lda_idx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_lda_idx_4;return; // READ
}

protected static void m6502_device_lda_idx_4(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_lda_idx_5;return; // READ
}

protected static void m6502_device_lda_idx_5(m6502_device cpu) {
  cpu._a = cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lda_idy --- 
protected static void m6502_device_lda_idy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lda_idy_1;return; // READ
}

protected static void m6502_device_lda_idy_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_lda_idy_2;return; // READ
}

protected static void m6502_device_lda_idy_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_lda_idy_3;return; // READ
}

protected static void m6502_device_lda_idy_3(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_lda_idy_4;return; // READ
  }
  cpu._doread((ushort)(cpu._tmp+cpu._y));
  cpu.NextFn = &m6502_device_lda_idy_5;return; // READ
}

protected static void m6502_device_lda_idy_4(m6502_device cpu) {
    cpu._doread((ushort)(cpu._tmp+cpu._y));
    cpu.NextFn = &m6502_device_lda_idy_5;return; // READ
}

protected static void m6502_device_lda_idy_5(m6502_device cpu) {
  cpu._a = cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lda_imm --- 
protected static void m6502_device_lda_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lda_imm_1;return; // READ
}

protected static void m6502_device_lda_imm_1(m6502_device cpu) {
  cpu._a = cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lda_zpg --- 
protected static void m6502_device_lda_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lda_zpg_1;return; // READ
}

protected static void m6502_device_lda_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_lda_zpg_2;return; // READ
}

protected static void m6502_device_lda_zpg_2(m6502_device cpu) {
  cpu._a = cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lda_zpx --- 
protected static void m6502_device_lda_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lda_zpx_1;return; // READ
}

protected static void m6502_device_lda_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_lda_zpx_2;return; // READ
}

protected static void m6502_device_lda_zpx_2(m6502_device cpu) {
  cpu._doread((ushort)((byte)(cpu._tmp+cpu._x)));
  cpu.NextFn = &m6502_device_lda_zpx_3;return; // READ
}

protected static void m6502_device_lda_zpx_3(m6502_device cpu) {
  cpu._a = cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ldx_aba --- 
protected static void m6502_device_ldx_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ldx_aba_1;return; // READ
}

protected static void m6502_device_ldx_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ldx_aba_2;return; // READ
}

protected static void m6502_device_ldx_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ldx_aba_3;return; // READ
}

protected static void m6502_device_ldx_aba_3(m6502_device cpu) {
  cpu._x = cpu._dat;
  cpu.set_nz(cpu._x);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ldx_aby --- 
protected static void m6502_device_ldx_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ldx_aby_1;return; // READ
}

protected static void m6502_device_ldx_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ldx_aby_2;return; // READ
}

protected static void m6502_device_ldx_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_ldx_aby_3;return; // READ
  }
  cpu._doread((ushort)(cpu._tmp + cpu._y));
  cpu.NextFn = &m6502_device_ldx_aby_4;return; // READ
}

protected static void m6502_device_ldx_aby_3(m6502_device cpu) {
    cpu._doread((ushort)(cpu._tmp + cpu._y));
    cpu.NextFn = &m6502_device_ldx_aby_4;return; // READ
}

protected static void m6502_device_ldx_aby_4(m6502_device cpu) {
  cpu._x = cpu._dat;
  cpu.set_nz(cpu._x);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ldx_imm --- 
protected static void m6502_device_ldx_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ldx_imm_1;return; // READ
}

protected static void m6502_device_ldx_imm_1(m6502_device cpu) {
  cpu._x = cpu._dat;
  cpu.set_nz(cpu._x);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ldx_zpg --- 
protected static void m6502_device_ldx_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ldx_zpg_1;return; // READ
}

protected static void m6502_device_ldx_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ldx_zpg_2;return; // READ
}

protected static void m6502_device_ldx_zpg_2(m6502_device cpu) {
  cpu._x = cpu._dat;
  cpu.set_nz(cpu._x);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ldx_zpy --- 
protected static void m6502_device_ldx_zpy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ldx_zpy_1;return; // READ
}

protected static void m6502_device_ldx_zpy_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ldx_zpy_2;return; // READ
}

protected static void m6502_device_ldx_zpy_2(m6502_device cpu) {
  cpu._doread((ushort)((byte)(cpu._tmp+cpu._y)));
  cpu.NextFn = &m6502_device_ldx_zpy_3;return; // READ
}

protected static void m6502_device_ldx_zpy_3(m6502_device cpu) {
  cpu._x = cpu._dat;
  cpu.set_nz(cpu._x);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ldy_aba --- 
protected static void m6502_device_ldy_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ldy_aba_1;return; // READ
}

protected static void m6502_device_ldy_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ldy_aba_2;return; // READ
}

protected static void m6502_device_ldy_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ldy_aba_3;return; // READ
}

protected static void m6502_device_ldy_aba_3(m6502_device cpu) {
  cpu._y = cpu._dat;
  cpu.set_nz(cpu._y);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ldy_abx --- 
protected static void m6502_device_ldy_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ldy_abx_1;return; // READ
}

protected static void m6502_device_ldy_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ldy_abx_2;return; // READ
}

protected static void m6502_device_ldy_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._x)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
    cpu.NextFn = &m6502_device_ldy_abx_3;return; // READ
  }
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ldy_abx_4;return; // READ
}

protected static void m6502_device_ldy_abx_3(m6502_device cpu) {
    cpu._tmp += cpu._x;
    cpu._doread((ushort)(cpu._tmp));
    cpu.NextFn = &m6502_device_ldy_abx_4;return; // READ
}

protected static void m6502_device_ldy_abx_4(m6502_device cpu) {
  cpu._y = cpu._dat;
  cpu.set_nz(cpu._y);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ldy_imm --- 
protected static void m6502_device_ldy_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ldy_imm_1;return; // READ
}

protected static void m6502_device_ldy_imm_1(m6502_device cpu) {
  cpu._y = cpu._dat;
  cpu.set_nz(cpu._y);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ldy_zpg --- 
protected static void m6502_device_ldy_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ldy_zpg_1;return; // READ
}

protected static void m6502_device_ldy_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ldy_zpg_2;return; // READ
}

protected static void m6502_device_ldy_zpg_2(m6502_device cpu) {
  cpu._y = cpu._dat;
  cpu.set_nz(cpu._y);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ldy_zpx --- 
protected static void m6502_device_ldy_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ldy_zpx_1;return; // READ
}

protected static void m6502_device_ldy_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ldy_zpx_2;return; // READ
}

protected static void m6502_device_ldy_zpx_2(m6502_device cpu) {
  cpu._doread((ushort)((byte)(cpu._tmp+cpu._x)));
  cpu.NextFn = &m6502_device_ldy_zpx_3;return; // READ
}

protected static void m6502_device_ldy_zpx_3(m6502_device cpu) {
  cpu._y = cpu._dat;
  cpu.set_nz(cpu._y);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lsr_aba --- 
protected static void m6502_device_lsr_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lsr_aba_1;return; // READ
}

protected static void m6502_device_lsr_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lsr_aba_2;return; // READ
}

protected static void m6502_device_lsr_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_lsr_aba_3;return; // READ
}

protected static void m6502_device_lsr_aba_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_lsr_aba_4;return; // WRITE
}

protected static void m6502_device_lsr_aba_4(m6502_device cpu) {
  cpu._tmp2 = cpu.do_lsr(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_lsr_aba_5;return; // WRITE
}

protected static void m6502_device_lsr_aba_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lsr_abx --- 
protected static void m6502_device_lsr_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lsr_abx_1;return; // READ
}

protected static void m6502_device_lsr_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lsr_abx_2;return; // READ
}

protected static void m6502_device_lsr_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
  cpu.NextFn = &m6502_device_lsr_abx_3;return; // READ
}

protected static void m6502_device_lsr_abx_3(m6502_device cpu) {
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_lsr_abx_4;return; // READ
}

protected static void m6502_device_lsr_abx_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_lsr_abx_5;return; // WRITE
}

protected static void m6502_device_lsr_abx_5(m6502_device cpu) {
  cpu._tmp2 = cpu.do_lsr(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_lsr_abx_6;return; // WRITE
}

protected static void m6502_device_lsr_abx_6(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lsr_acc --- 
protected static void m6502_device_lsr_acc_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_lsr_acc_1;return; // READ
}

protected static void m6502_device_lsr_acc_1(m6502_device cpu) {
  cpu._a = cpu.do_lsr(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lsr_zpg --- 
protected static void m6502_device_lsr_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lsr_zpg_1;return; // READ
}

protected static void m6502_device_lsr_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_lsr_zpg_2;return; // READ
}

protected static void m6502_device_lsr_zpg_2(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_lsr_zpg_3;return; // WRITE
}

protected static void m6502_device_lsr_zpg_3(m6502_device cpu) {
  cpu._tmp2 = cpu.do_lsr(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_lsr_zpg_4;return; // WRITE
}

protected static void m6502_device_lsr_zpg_4(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lsr_zpx --- 
protected static void m6502_device_lsr_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lsr_zpx_1;return; // READ
}

protected static void m6502_device_lsr_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_lsr_zpx_2;return; // READ
}

protected static void m6502_device_lsr_zpx_2(m6502_device cpu) {
  cpu._tmp = (byte)(cpu._tmp+cpu._x);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_lsr_zpx_3;return; // READ
}

protected static void m6502_device_lsr_zpx_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_lsr_zpx_4;return; // WRITE
}

protected static void m6502_device_lsr_zpx_4(m6502_device cpu) {
  cpu._tmp2 = cpu.do_lsr(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_lsr_zpx_5;return; // WRITE
}

protected static void m6502_device_lsr_zpx_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op lxa_imm --- 
protected static void m6502_device_lxa_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_lxa_imm_1;return; // READ
}

protected static void m6502_device_lxa_imm_1(m6502_device cpu) {
  cpu._a = cpu._x = cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op nop_aba --- 
protected static void m6502_device_nop_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_nop_aba_1;return; // READ
}

protected static void m6502_device_nop_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_nop_aba_2;return; // READ
}

protected static void m6502_device_nop_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_nop_aba_3;return; // READ
}

protected static void m6502_device_nop_aba_3(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op nop_abx --- 
protected static void m6502_device_nop_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_nop_abx_1;return; // READ
}

protected static void m6502_device_nop_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_nop_abx_2;return; // READ
}

protected static void m6502_device_nop_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._x)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
    cpu.NextFn = &m6502_device_nop_abx_3;return; // READ
  }
  cpu._doread((ushort)(cpu._tmp + cpu._x));
  cpu.NextFn = &m6502_device_nop_abx_4;return; // READ
}

protected static void m6502_device_nop_abx_3(m6502_device cpu) {
    cpu._doread((ushort)(cpu._tmp + cpu._x));
    cpu.NextFn = &m6502_device_nop_abx_4;return; // READ
}

protected static void m6502_device_nop_abx_4(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op nop_imm --- 
protected static void m6502_device_nop_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_nop_imm_1;return; // READ
}

protected static void m6502_device_nop_imm_1(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op nop_imp --- 
protected static void m6502_device_nop_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_nop_imp_1;return; // READ
}

protected static void m6502_device_nop_imp_1(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op nop_zpg --- 
protected static void m6502_device_nop_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_nop_zpg_1;return; // READ
}

protected static void m6502_device_nop_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_nop_zpg_2;return; // READ
}

protected static void m6502_device_nop_zpg_2(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op nop_zpx --- 
protected static void m6502_device_nop_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_nop_zpx_1;return; // READ
}

protected static void m6502_device_nop_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_nop_zpx_2;return; // READ
}

protected static void m6502_device_nop_zpx_2(m6502_device cpu) {
  cpu._doread((ushort)((byte)(cpu._tmp+cpu._x)));
  cpu.NextFn = &m6502_device_nop_zpx_3;return; // READ
}

protected static void m6502_device_nop_zpx_3(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ora_aba --- 
protected static void m6502_device_ora_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ora_aba_1;return; // READ
}

protected static void m6502_device_ora_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ora_aba_2;return; // READ
}

protected static void m6502_device_ora_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ora_aba_3;return; // READ
}

protected static void m6502_device_ora_aba_3(m6502_device cpu) {
  cpu._a |= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ora_abx --- 
protected static void m6502_device_ora_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ora_abx_1;return; // READ
}

protected static void m6502_device_ora_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ora_abx_2;return; // READ
}

protected static void m6502_device_ora_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._x)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
    cpu.NextFn = &m6502_device_ora_abx_3;return; // READ
  }
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ora_abx_4;return; // READ
}

protected static void m6502_device_ora_abx_3(m6502_device cpu) {
    cpu._tmp += cpu._x;
    cpu._doread((ushort)(cpu._tmp));
    cpu.NextFn = &m6502_device_ora_abx_4;return; // READ
}

protected static void m6502_device_ora_abx_4(m6502_device cpu) {
  cpu._a |= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ora_aby --- 
protected static void m6502_device_ora_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ora_aby_1;return; // READ
}

protected static void m6502_device_ora_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ora_aby_2;return; // READ
}

protected static void m6502_device_ora_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_ora_aby_3;return; // READ
  }
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ora_aby_4;return; // READ
}

protected static void m6502_device_ora_aby_3(m6502_device cpu) {
    cpu._tmp += cpu._y;
    cpu._doread((ushort)(cpu._tmp));
    cpu.NextFn = &m6502_device_ora_aby_4;return; // READ
}

protected static void m6502_device_ora_aby_4(m6502_device cpu) {
  cpu._a |= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ora_idx --- 
protected static void m6502_device_ora_idx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ora_idx_1;return; // READ
}

protected static void m6502_device_ora_idx_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_ora_idx_2;return; // READ
}

protected static void m6502_device_ora_idx_2(m6502_device cpu) {
  cpu._tmp2 += cpu._x;
  cpu._doread((ushort)(cpu._tmp2 & 0xff));
  cpu.NextFn = &m6502_device_ora_idx_3;return; // READ
}

protected static void m6502_device_ora_idx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_ora_idx_4;return; // READ
}

protected static void m6502_device_ora_idx_4(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ora_idx_5;return; // READ
}

protected static void m6502_device_ora_idx_5(m6502_device cpu) {
  cpu._a |= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ora_idy --- 
protected static void m6502_device_ora_idy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ora_idy_1;return; // READ
}

protected static void m6502_device_ora_idy_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_ora_idy_2;return; // READ
}

protected static void m6502_device_ora_idy_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_ora_idy_3;return; // READ
}

protected static void m6502_device_ora_idy_3(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_ora_idy_4;return; // READ
  }
  cpu._doread((ushort)(cpu._tmp+cpu._y));
  cpu.NextFn = &m6502_device_ora_idy_5;return; // READ
}

protected static void m6502_device_ora_idy_4(m6502_device cpu) {
    cpu._doread((ushort)(cpu._tmp+cpu._y));
    cpu.NextFn = &m6502_device_ora_idy_5;return; // READ
}

protected static void m6502_device_ora_idy_5(m6502_device cpu) {
  cpu._a |= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ora_imm --- 
protected static void m6502_device_ora_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ora_imm_1;return; // READ
}

protected static void m6502_device_ora_imm_1(m6502_device cpu) {
  cpu._a |= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ora_zpg --- 
protected static void m6502_device_ora_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ora_zpg_1;return; // READ
}

protected static void m6502_device_ora_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ora_zpg_2;return; // READ
}

protected static void m6502_device_ora_zpg_2(m6502_device cpu) {
  cpu._a |= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ora_zpx --- 
protected static void m6502_device_ora_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ora_zpx_1;return; // READ
}

protected static void m6502_device_ora_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ora_zpx_2;return; // READ
}

protected static void m6502_device_ora_zpx_2(m6502_device cpu) {
  cpu._doread((ushort)((byte)(cpu._tmp+cpu._x)));
  cpu.NextFn = &m6502_device_ora_zpx_3;return; // READ
}

protected static void m6502_device_ora_zpx_3(m6502_device cpu) {
  cpu._a |= cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op pha_imp --- 
protected static void m6502_device_pha_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_pha_imp_1;return; // READ
}

protected static void m6502_device_pha_imp_1(m6502_device cpu) {
  cpu._dowrite((ushort)(cpu._sp),(byte)( cpu._a));
  cpu.NextFn = &m6502_device_pha_imp_2;return; // WRITE
}

protected static void m6502_device_pha_imp_2(m6502_device cpu) {
  cpu.dec_SP();
  m6502_device_fetch(cpu);return; // fetch
}

// --- op php_imp --- 
protected static void m6502_device_php_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_php_imp_1;return; // READ
}

protected static void m6502_device_php_imp_1(m6502_device cpu) {
  cpu._dowrite((ushort)(cpu._sp),(byte)( cpu._p));
  cpu.NextFn = &m6502_device_php_imp_2;return; // WRITE
}

protected static void m6502_device_php_imp_2(m6502_device cpu) {
  cpu.dec_SP();
  m6502_device_fetch(cpu);return; // fetch
}

// --- op pla_imp --- 
protected static void m6502_device_pla_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_pla_imp_1;return; // READ
}

protected static void m6502_device_pla_imp_1(m6502_device cpu) {
  cpu._doread((ushort)(cpu._sp));
  cpu.NextFn = &m6502_device_pla_imp_2;return; // READ
}

protected static void m6502_device_pla_imp_2(m6502_device cpu) {
  cpu.inc_SP();
  cpu._doread((ushort)(cpu._sp));
  cpu.NextFn = &m6502_device_pla_imp_3;return; // READ
}

protected static void m6502_device_pla_imp_3(m6502_device cpu) {
  cpu._a = cpu._dat;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op plp_imp --- 
protected static void m6502_device_plp_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_plp_imp_1;return; // READ
}

protected static void m6502_device_plp_imp_1(m6502_device cpu) {
  cpu._doread((ushort)(cpu._sp));
  cpu.NextFn = &m6502_device_plp_imp_2;return; // READ
}

protected static void m6502_device_plp_imp_2(m6502_device cpu) {
  cpu.inc_SP();
  cpu._doread((ushort)(cpu._sp));
  cpu.NextFn = &m6502_device_plp_imp_3;return; // READ
}

protected static void m6502_device_plp_imp_3(m6502_device cpu) {
  cpu._tmp = (byte)(cpu._dat | (F_B|F_E));
  cpu.PrefetchNextFn = &m6502_device_plp_imp_4;
  m6502_device_prefetch(cpu);return; // prefetch
}

protected static void m6502_device_plp_imp_4(m6502_device cpu) {
  cpu._p = (byte)cpu._tmp;
  m6502_device_postfetch(cpu);return; // postfetch
}

// --- op reset --- 
protected static void m6502_device_reset_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_reset_1;return; // READ
}

protected static void m6502_device_reset_1(m6502_device cpu) {
  cpu._tmp=cpu._dat;
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_reset_2;return; // READ
}

protected static void m6502_device_reset_2(m6502_device cpu) {
  cpu._tmp=cpu._dat;
  cpu._doread((ushort)(cpu._sp));
  cpu.NextFn = &m6502_device_reset_3;return; // READ
}

protected static void m6502_device_reset_3(m6502_device cpu) {
  cpu._tmp=cpu._dat; cpu.dec_SP();
  cpu._doread((ushort)(cpu._sp));
  cpu.NextFn = &m6502_device_reset_4;return; // READ
}

protected static void m6502_device_reset_4(m6502_device cpu) {
  cpu._tmp=cpu._dat; cpu.dec_SP();
  cpu._doread((ushort)(cpu._sp));
  cpu.NextFn = &m6502_device_reset_5;return; // READ
}

protected static void m6502_device_reset_5(m6502_device cpu) {
  cpu._tmp=cpu._dat; cpu.dec_SP();
  cpu._doread((ushort)(0xfffc));
  cpu.NextFn = &m6502_device_reset_6;return; // READ
}

protected static void m6502_device_reset_6(m6502_device cpu) {
  cpu._p |= F_I; cpu._pc = cpu._dat;
  cpu._doread((ushort)(0xfffd));
  cpu.NextFn = &m6502_device_reset_7;return; // READ
}

protected static void m6502_device_reset_7(m6502_device cpu) {
  cpu._pc = cpu.set_h(cpu._pc, cpu._dat);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rla_aba --- 
protected static void m6502_device_rla_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rla_aba_1;return; // READ
}

protected static void m6502_device_rla_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rla_aba_2;return; // READ
}

protected static void m6502_device_rla_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rla_aba_3;return; // READ
}

protected static void m6502_device_rla_aba_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rla_aba_4;return; // WRITE
}

protected static void m6502_device_rla_aba_4(m6502_device cpu) {
  cpu._tmp2 = cpu.do_rol(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rla_aba_5;return; // WRITE
}

protected static void m6502_device_rla_aba_5(m6502_device cpu) {
  cpu._a &= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rla_abx --- 
protected static void m6502_device_rla_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rla_abx_1;return; // READ
}

protected static void m6502_device_rla_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rla_abx_2;return; // READ
}

protected static void m6502_device_rla_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
  cpu.NextFn = &m6502_device_rla_abx_3;return; // READ
}

protected static void m6502_device_rla_abx_3(m6502_device cpu) {
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rla_abx_4;return; // READ
}

protected static void m6502_device_rla_abx_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rla_abx_5;return; // WRITE
}

protected static void m6502_device_rla_abx_5(m6502_device cpu) {
  cpu._tmp2 = cpu.do_rol(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rla_abx_6;return; // WRITE
}

protected static void m6502_device_rla_abx_6(m6502_device cpu) {
  cpu._a &= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rla_aby --- 
protected static void m6502_device_rla_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rla_aby_1;return; // READ
}

protected static void m6502_device_rla_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rla_aby_2;return; // READ
}

protected static void m6502_device_rla_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_rla_aby_3;return; // READ
}

protected static void m6502_device_rla_aby_3(m6502_device cpu) {
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rla_aby_4;return; // READ
}

protected static void m6502_device_rla_aby_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rla_aby_5;return; // WRITE
}

protected static void m6502_device_rla_aby_5(m6502_device cpu) {
  cpu._tmp2 = cpu.do_rol(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rla_aby_6;return; // WRITE
}

protected static void m6502_device_rla_aby_6(m6502_device cpu) {
  cpu._a &= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rla_idx --- 
protected static void m6502_device_rla_idx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rla_idx_1;return; // READ
}

protected static void m6502_device_rla_idx_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_rla_idx_2;return; // READ
}

protected static void m6502_device_rla_idx_2(m6502_device cpu) {
  cpu._tmp2 += cpu._x;
  cpu._doread((ushort)(cpu._tmp2 & 0xff));
  cpu.NextFn = &m6502_device_rla_idx_3;return; // READ
}

protected static void m6502_device_rla_idx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_rla_idx_4;return; // READ
}

protected static void m6502_device_rla_idx_4(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rla_idx_5;return; // READ
}

protected static void m6502_device_rla_idx_5(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rla_idx_6;return; // WRITE
}

protected static void m6502_device_rla_idx_6(m6502_device cpu) {
  cpu._tmp2 = cpu.do_rol(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rla_idx_7;return; // WRITE
}

protected static void m6502_device_rla_idx_7(m6502_device cpu) {
  cpu._a &= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rla_idy --- 
protected static void m6502_device_rla_idy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rla_idy_1;return; // READ
}

protected static void m6502_device_rla_idy_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_rla_idy_2;return; // READ
}

protected static void m6502_device_rla_idy_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_rla_idy_3;return; // READ
}

protected static void m6502_device_rla_idy_3(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_rla_idy_4;return; // READ
}

protected static void m6502_device_rla_idy_4(m6502_device cpu) {
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rla_idy_5;return; // READ
}

protected static void m6502_device_rla_idy_5(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rla_idy_6;return; // WRITE
}

protected static void m6502_device_rla_idy_6(m6502_device cpu) {
  cpu._tmp2 = cpu.do_rol(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rla_idy_7;return; // WRITE
}

protected static void m6502_device_rla_idy_7(m6502_device cpu) {
  cpu._a &= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rla_zpg --- 
protected static void m6502_device_rla_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rla_zpg_1;return; // READ
}

protected static void m6502_device_rla_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rla_zpg_2;return; // READ
}

protected static void m6502_device_rla_zpg_2(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rla_zpg_3;return; // WRITE
}

protected static void m6502_device_rla_zpg_3(m6502_device cpu) {
  cpu._tmp2 = cpu.do_rol(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rla_zpg_4;return; // WRITE
}

protected static void m6502_device_rla_zpg_4(m6502_device cpu) {
  cpu._a &= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rla_zpx --- 
protected static void m6502_device_rla_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rla_zpx_1;return; // READ
}

protected static void m6502_device_rla_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rla_zpx_2;return; // READ
}

protected static void m6502_device_rla_zpx_2(m6502_device cpu) {
  cpu._tmp = (byte)(cpu._tmp+cpu._x);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rla_zpx_3;return; // READ
}

protected static void m6502_device_rla_zpx_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rla_zpx_4;return; // WRITE
}

protected static void m6502_device_rla_zpx_4(m6502_device cpu) {
  cpu._tmp2 = cpu.do_rol(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rla_zpx_5;return; // WRITE
}

protected static void m6502_device_rla_zpx_5(m6502_device cpu) {
  cpu._a &= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rol_aba --- 
protected static void m6502_device_rol_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rol_aba_1;return; // READ
}

protected static void m6502_device_rol_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rol_aba_2;return; // READ
}

protected static void m6502_device_rol_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rol_aba_3;return; // READ
}

protected static void m6502_device_rol_aba_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rol_aba_4;return; // WRITE
}

protected static void m6502_device_rol_aba_4(m6502_device cpu) {
  cpu._tmp2 = cpu.do_rol(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rol_aba_5;return; // WRITE
}

protected static void m6502_device_rol_aba_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rol_abx --- 
protected static void m6502_device_rol_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rol_abx_1;return; // READ
}

protected static void m6502_device_rol_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rol_abx_2;return; // READ
}

protected static void m6502_device_rol_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
  cpu.NextFn = &m6502_device_rol_abx_3;return; // READ
}

protected static void m6502_device_rol_abx_3(m6502_device cpu) {
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rol_abx_4;return; // READ
}

protected static void m6502_device_rol_abx_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rol_abx_5;return; // WRITE
}

protected static void m6502_device_rol_abx_5(m6502_device cpu) {
  cpu._tmp2 = cpu.do_rol(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rol_abx_6;return; // WRITE
}

protected static void m6502_device_rol_abx_6(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rol_acc --- 
protected static void m6502_device_rol_acc_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_rol_acc_1;return; // READ
}

protected static void m6502_device_rol_acc_1(m6502_device cpu) {
  cpu._a = cpu.do_rol(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rol_zpg --- 
protected static void m6502_device_rol_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rol_zpg_1;return; // READ
}

protected static void m6502_device_rol_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rol_zpg_2;return; // READ
}

protected static void m6502_device_rol_zpg_2(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rol_zpg_3;return; // WRITE
}

protected static void m6502_device_rol_zpg_3(m6502_device cpu) {
  cpu._tmp2 = cpu.do_rol(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rol_zpg_4;return; // WRITE
}

protected static void m6502_device_rol_zpg_4(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rol_zpx --- 
protected static void m6502_device_rol_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rol_zpx_1;return; // READ
}

protected static void m6502_device_rol_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rol_zpx_2;return; // READ
}

protected static void m6502_device_rol_zpx_2(m6502_device cpu) {
  cpu._tmp = (byte)(cpu._tmp+cpu._x);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rol_zpx_3;return; // READ
}

protected static void m6502_device_rol_zpx_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rol_zpx_4;return; // WRITE
}

protected static void m6502_device_rol_zpx_4(m6502_device cpu) {
  cpu._tmp2 = cpu.do_rol(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rol_zpx_5;return; // WRITE
}

protected static void m6502_device_rol_zpx_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ror_aba --- 
protected static void m6502_device_ror_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ror_aba_1;return; // READ
}

protected static void m6502_device_ror_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ror_aba_2;return; // READ
}

protected static void m6502_device_ror_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ror_aba_3;return; // READ
}

protected static void m6502_device_ror_aba_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_ror_aba_4;return; // WRITE
}

protected static void m6502_device_ror_aba_4(m6502_device cpu) {
  cpu._tmp2 = cpu.do_ror(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_ror_aba_5;return; // WRITE
}

protected static void m6502_device_ror_aba_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ror_abx --- 
protected static void m6502_device_ror_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ror_abx_1;return; // READ
}

protected static void m6502_device_ror_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ror_abx_2;return; // READ
}

protected static void m6502_device_ror_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
  cpu.NextFn = &m6502_device_ror_abx_3;return; // READ
}

protected static void m6502_device_ror_abx_3(m6502_device cpu) {
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ror_abx_4;return; // READ
}

protected static void m6502_device_ror_abx_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_ror_abx_5;return; // WRITE
}

protected static void m6502_device_ror_abx_5(m6502_device cpu) {
  cpu._tmp2 = cpu.do_ror(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_ror_abx_6;return; // WRITE
}

protected static void m6502_device_ror_abx_6(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ror_acc --- 
protected static void m6502_device_ror_acc_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_ror_acc_1;return; // READ
}

protected static void m6502_device_ror_acc_1(m6502_device cpu) {
  cpu._a = cpu.do_ror(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ror_zpg --- 
protected static void m6502_device_ror_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ror_zpg_1;return; // READ
}

protected static void m6502_device_ror_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ror_zpg_2;return; // READ
}

protected static void m6502_device_ror_zpg_2(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_ror_zpg_3;return; // WRITE
}

protected static void m6502_device_ror_zpg_3(m6502_device cpu) {
  cpu._tmp2 = cpu.do_ror(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_ror_zpg_4;return; // WRITE
}

protected static void m6502_device_ror_zpg_4(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op ror_zpx --- 
protected static void m6502_device_ror_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_ror_zpx_1;return; // READ
}

protected static void m6502_device_ror_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ror_zpx_2;return; // READ
}

protected static void m6502_device_ror_zpx_2(m6502_device cpu) {
  cpu._tmp = (byte)(cpu._tmp+cpu._x);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_ror_zpx_3;return; // READ
}

protected static void m6502_device_ror_zpx_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_ror_zpx_4;return; // WRITE
}

protected static void m6502_device_ror_zpx_4(m6502_device cpu) {
  cpu._tmp2 = cpu.do_ror(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_ror_zpx_5;return; // WRITE
}

protected static void m6502_device_ror_zpx_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rra_aba --- 
protected static void m6502_device_rra_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rra_aba_1;return; // READ
}

protected static void m6502_device_rra_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rra_aba_2;return; // READ
}

protected static void m6502_device_rra_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rra_aba_3;return; // READ
}

protected static void m6502_device_rra_aba_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rra_aba_4;return; // WRITE
}

protected static void m6502_device_rra_aba_4(m6502_device cpu) {
  cpu._tmp2 = cpu.do_ror(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rra_aba_5;return; // WRITE
}

protected static void m6502_device_rra_aba_5(m6502_device cpu) {
  cpu.do_adc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rra_abx --- 
protected static void m6502_device_rra_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rra_abx_1;return; // READ
}

protected static void m6502_device_rra_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rra_abx_2;return; // READ
}

protected static void m6502_device_rra_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
  cpu.NextFn = &m6502_device_rra_abx_3;return; // READ
}

protected static void m6502_device_rra_abx_3(m6502_device cpu) {
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rra_abx_4;return; // READ
}

protected static void m6502_device_rra_abx_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rra_abx_5;return; // WRITE
}

protected static void m6502_device_rra_abx_5(m6502_device cpu) {
  cpu._tmp2 = cpu.do_ror(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rra_abx_6;return; // WRITE
}

protected static void m6502_device_rra_abx_6(m6502_device cpu) {
  cpu.do_adc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rra_aby --- 
protected static void m6502_device_rra_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rra_aby_1;return; // READ
}

protected static void m6502_device_rra_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rra_aby_2;return; // READ
}

protected static void m6502_device_rra_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_rra_aby_3;return; // READ
}

protected static void m6502_device_rra_aby_3(m6502_device cpu) {
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rra_aby_4;return; // READ
}

protected static void m6502_device_rra_aby_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rra_aby_5;return; // WRITE
}

protected static void m6502_device_rra_aby_5(m6502_device cpu) {
  cpu._tmp2 = cpu.do_ror(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rra_aby_6;return; // WRITE
}

protected static void m6502_device_rra_aby_6(m6502_device cpu) {
  cpu.do_adc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rra_idx --- 
protected static void m6502_device_rra_idx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rra_idx_1;return; // READ
}

protected static void m6502_device_rra_idx_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_rra_idx_2;return; // READ
}

protected static void m6502_device_rra_idx_2(m6502_device cpu) {
  cpu._tmp2 += cpu._x;
  cpu._doread((ushort)(cpu._tmp2 & 0xff));
  cpu.NextFn = &m6502_device_rra_idx_3;return; // READ
}

protected static void m6502_device_rra_idx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_rra_idx_4;return; // READ
}

protected static void m6502_device_rra_idx_4(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rra_idx_5;return; // READ
}

protected static void m6502_device_rra_idx_5(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rra_idx_6;return; // WRITE
}

protected static void m6502_device_rra_idx_6(m6502_device cpu) {
  cpu._tmp2 = cpu.do_ror(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rra_idx_7;return; // WRITE
}

protected static void m6502_device_rra_idx_7(m6502_device cpu) {
  cpu.do_adc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rra_idy --- 
protected static void m6502_device_rra_idy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rra_idy_1;return; // READ
}

protected static void m6502_device_rra_idy_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_rra_idy_2;return; // READ
}

protected static void m6502_device_rra_idy_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_rra_idy_3;return; // READ
}

protected static void m6502_device_rra_idy_3(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_rra_idy_4;return; // READ
}

protected static void m6502_device_rra_idy_4(m6502_device cpu) {
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rra_idy_5;return; // READ
}

protected static void m6502_device_rra_idy_5(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rra_idy_6;return; // WRITE
}

protected static void m6502_device_rra_idy_6(m6502_device cpu) {
  cpu._tmp2 = cpu.do_ror(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rra_idy_7;return; // WRITE
}

protected static void m6502_device_rra_idy_7(m6502_device cpu) {
  cpu.do_adc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rra_zpg --- 
protected static void m6502_device_rra_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rra_zpg_1;return; // READ
}

protected static void m6502_device_rra_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rra_zpg_2;return; // READ
}

protected static void m6502_device_rra_zpg_2(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rra_zpg_3;return; // WRITE
}

protected static void m6502_device_rra_zpg_3(m6502_device cpu) {
  cpu._tmp2 = cpu.do_ror(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rra_zpg_4;return; // WRITE
}

protected static void m6502_device_rra_zpg_4(m6502_device cpu) {
  cpu.do_adc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rra_zpx --- 
protected static void m6502_device_rra_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rra_zpx_1;return; // READ
}

protected static void m6502_device_rra_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rra_zpx_2;return; // READ
}

protected static void m6502_device_rra_zpx_2(m6502_device cpu) {
  cpu._tmp = (byte)(cpu._tmp+cpu._x);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_rra_zpx_3;return; // READ
}

protected static void m6502_device_rra_zpx_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rra_zpx_4;return; // WRITE
}

protected static void m6502_device_rra_zpx_4(m6502_device cpu) {
  cpu._tmp2 = cpu.do_ror(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_rra_zpx_5;return; // WRITE
}

protected static void m6502_device_rra_zpx_5(m6502_device cpu) {
  cpu.do_adc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rti_imp --- 
protected static void m6502_device_rti_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_rti_imp_1;return; // READ
}

protected static void m6502_device_rti_imp_1(m6502_device cpu) {
  cpu._doread((ushort)(cpu._sp));
  cpu.NextFn = &m6502_device_rti_imp_2;return; // READ
}

protected static void m6502_device_rti_imp_2(m6502_device cpu) {
  cpu.inc_SP();
  cpu._doread((ushort)(cpu._sp));
  cpu.NextFn = &m6502_device_rti_imp_3;return; // READ
}

protected static void m6502_device_rti_imp_3(m6502_device cpu) {
  cpu._p = (byte)(cpu._dat | (F_B|F_E));
  cpu.inc_SP();
  cpu._doread((ushort)(cpu._sp));
  cpu.NextFn = &m6502_device_rti_imp_4;return; // READ
}

protected static void m6502_device_rti_imp_4(m6502_device cpu) {
  cpu._pc = cpu._dat;
  cpu.inc_SP();
  cpu._doread((ushort)(cpu._sp));
  cpu.NextFn = &m6502_device_rti_imp_5;return; // READ
}

protected static void m6502_device_rti_imp_5(m6502_device cpu) {
  cpu._pc = cpu.set_h(cpu._pc, cpu._dat);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op rts_imp --- 
protected static void m6502_device_rts_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_rts_imp_1;return; // READ
}

protected static void m6502_device_rts_imp_1(m6502_device cpu) {
  cpu._doread((ushort)(cpu._sp));
  cpu.NextFn = &m6502_device_rts_imp_2;return; // READ
}

protected static void m6502_device_rts_imp_2(m6502_device cpu) {
  cpu.inc_SP();
  cpu._doread((ushort)(cpu._sp));
  cpu.NextFn = &m6502_device_rts_imp_3;return; // READ
}

protected static void m6502_device_rts_imp_3(m6502_device cpu) {
  cpu._pc = cpu._dat;
  cpu.inc_SP();
  cpu._doread((ushort)(cpu._sp));
  cpu.NextFn = &m6502_device_rts_imp_4;return; // READ
}

protected static void m6502_device_rts_imp_4(m6502_device cpu) {
  cpu._pc = cpu.set_h(cpu._pc, cpu._dat);
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_rts_imp_5;return; // READ
}

protected static void m6502_device_rts_imp_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sax_aba --- 
protected static void m6502_device_sax_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sax_aba_1;return; // READ
}

protected static void m6502_device_sax_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sax_aba_2;return; // READ
}

protected static void m6502_device_sax_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._tmp2 = (byte)(cpu._a & cpu._x);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sax_aba_3;return; // WRITE
}

protected static void m6502_device_sax_aba_3(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sax_idx --- 
protected static void m6502_device_sax_idx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sax_idx_1;return; // READ
}

protected static void m6502_device_sax_idx_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_sax_idx_2;return; // READ
}

protected static void m6502_device_sax_idx_2(m6502_device cpu) {
  cpu._tmp2 += cpu._x;
  cpu._doread((ushort)(cpu._tmp2 & 0xff));
  cpu.NextFn = &m6502_device_sax_idx_3;return; // READ
}

protected static void m6502_device_sax_idx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_sax_idx_4;return; // READ
}

protected static void m6502_device_sax_idx_4(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._tmp2 = (byte)(cpu._a & cpu._x);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sax_idx_5;return; // WRITE
}

protected static void m6502_device_sax_idx_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sax_zpg --- 
protected static void m6502_device_sax_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sax_zpg_1;return; // READ
}

protected static void m6502_device_sax_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._tmp2 = (byte)(cpu._a & cpu._x);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sax_zpg_2;return; // WRITE
}

protected static void m6502_device_sax_zpg_2(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sax_zpy --- 
protected static void m6502_device_sax_zpy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sax_zpy_1;return; // READ
}

protected static void m6502_device_sax_zpy_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sax_zpy_2;return; // READ
}

protected static void m6502_device_sax_zpy_2(m6502_device cpu) {
  cpu._tmp = (byte)(cpu._tmp+cpu._y);
  cpu._tmp2 = (byte)(cpu._a & cpu._x);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sax_zpy_3;return; // WRITE
}

protected static void m6502_device_sax_zpy_3(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sbc_aba --- 
protected static void m6502_device_sbc_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sbc_aba_1;return; // READ
}

protected static void m6502_device_sbc_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sbc_aba_2;return; // READ
}

protected static void m6502_device_sbc_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sbc_aba_3;return; // READ
}

protected static void m6502_device_sbc_aba_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu.do_sbc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sbc_abx --- 
protected static void m6502_device_sbc_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sbc_abx_1;return; // READ
}

protected static void m6502_device_sbc_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sbc_abx_2;return; // READ
}

protected static void m6502_device_sbc_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._x)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
    cpu.NextFn = &m6502_device_sbc_abx_3;return; // READ
  }
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sbc_abx_4;return; // READ
}

protected static void m6502_device_sbc_abx_3(m6502_device cpu) {
    cpu._tmp += cpu._x;
    cpu._doread((ushort)(cpu._tmp));
    cpu.NextFn = &m6502_device_sbc_abx_4;return; // READ
}

protected static void m6502_device_sbc_abx_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu.do_sbc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sbc_aby --- 
protected static void m6502_device_sbc_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sbc_aby_1;return; // READ
}

protected static void m6502_device_sbc_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sbc_aby_2;return; // READ
}

protected static void m6502_device_sbc_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_sbc_aby_3;return; // READ
  }
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sbc_aby_4;return; // READ
}

protected static void m6502_device_sbc_aby_3(m6502_device cpu) {
    cpu._tmp += cpu._y;
    cpu._doread((ushort)(cpu._tmp));
    cpu.NextFn = &m6502_device_sbc_aby_4;return; // READ
}

protected static void m6502_device_sbc_aby_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu.do_sbc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sbc_idx --- 
protected static void m6502_device_sbc_idx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sbc_idx_1;return; // READ
}

protected static void m6502_device_sbc_idx_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_sbc_idx_2;return; // READ
}

protected static void m6502_device_sbc_idx_2(m6502_device cpu) {
  cpu._tmp2 += cpu._x;
  cpu._doread((ushort)(cpu._tmp2 & 0xff));
  cpu.NextFn = &m6502_device_sbc_idx_3;return; // READ
}

protected static void m6502_device_sbc_idx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_sbc_idx_4;return; // READ
}

protected static void m6502_device_sbc_idx_4(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sbc_idx_5;return; // READ
}

protected static void m6502_device_sbc_idx_5(m6502_device cpu) {
  cpu.do_sbc(cpu._dat);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sbc_idy --- 
protected static void m6502_device_sbc_idy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sbc_idy_1;return; // READ
}

protected static void m6502_device_sbc_idy_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_sbc_idy_2;return; // READ
}

protected static void m6502_device_sbc_idy_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_sbc_idy_3;return; // READ
}

protected static void m6502_device_sbc_idy_3(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
    cpu.NextFn = &m6502_device_sbc_idy_4;return; // READ
  }
  cpu._doread((ushort)(cpu._tmp+cpu._y));
  cpu.NextFn = &m6502_device_sbc_idy_5;return; // READ
}

protected static void m6502_device_sbc_idy_4(m6502_device cpu) {
    cpu._doread((ushort)(cpu._tmp+cpu._y));
    cpu.NextFn = &m6502_device_sbc_idy_5;return; // READ
}

protected static void m6502_device_sbc_idy_5(m6502_device cpu) {
  cpu.do_sbc(cpu._dat);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sbc_imm --- 
protected static void m6502_device_sbc_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sbc_imm_1;return; // READ
}

protected static void m6502_device_sbc_imm_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu.do_sbc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sbc_zpg --- 
protected static void m6502_device_sbc_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sbc_zpg_1;return; // READ
}

protected static void m6502_device_sbc_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sbc_zpg_2;return; // READ
}

protected static void m6502_device_sbc_zpg_2(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu.do_sbc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sbc_zpx --- 
protected static void m6502_device_sbc_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sbc_zpx_1;return; // READ
}

protected static void m6502_device_sbc_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sbc_zpx_2;return; // READ
}

protected static void m6502_device_sbc_zpx_2(m6502_device cpu) {
  cpu._doread((ushort)((byte)(cpu._tmp+cpu._x)));
  cpu.NextFn = &m6502_device_sbc_zpx_3;return; // READ
}

protected static void m6502_device_sbc_zpx_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu.do_sbc(cpu._tmp2);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sbx_imm --- 
protected static void m6502_device_sbx_imm_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sbx_imm_1;return; // READ
}

protected static void m6502_device_sbx_imm_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._x &= cpu._a;
  if (cpu._x < cpu._tmp2) {
    cpu._p &= F_C ^ 0xFF;
  } else {
    cpu._p |= F_C;
  }
  cpu._x -= cpu._tmp2;
  cpu.set_nz(cpu._x);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sec_imp --- 
protected static void m6502_device_sec_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_sec_imp_1;return; // READ
}

protected static void m6502_device_sec_imp_1(m6502_device cpu) {
  cpu._p |= F_C;
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sed_imp --- 
protected static void m6502_device_sed_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_sed_imp_1;return; // READ
}

protected static void m6502_device_sed_imp_1(m6502_device cpu) {
  cpu._p |= F_D;
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sei_imp --- 
protected static void m6502_device_sei_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_sei_imp_1;return; // READ
}

protected static void m6502_device_sei_imp_1(m6502_device cpu) {
  cpu.PrefetchNextFn = &m6502_device_sei_imp_2;
  m6502_device_prefetch(cpu);return; // prefetch
}

protected static void m6502_device_sei_imp_2(m6502_device cpu) {
  cpu._p |= F_I;
  m6502_device_postfetch(cpu);return; // postfetch
}

// --- op sha_aby --- 
protected static void m6502_device_sha_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sha_aby_1;return; // READ
}

protected static void m6502_device_sha_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sha_aby_2;return; // READ
}

protected static void m6502_device_sha_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_sha_aby_3;return; // READ
}

protected static void m6502_device_sha_aby_3(m6502_device cpu) {
  cpu._tmp2 = (byte)(cpu._a & cpu._x & ((cpu._tmp >> 8)+1));
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._tmp = cpu.set_h((ushort)(cpu._tmp+cpu._y), cpu._tmp2);
  } else {
    cpu._tmp += cpu._y;
  }
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sha_aby_4;return; // WRITE
}

protected static void m6502_device_sha_aby_4(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sha_idy --- 
protected static void m6502_device_sha_idy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sha_idy_1;return; // READ
}

protected static void m6502_device_sha_idy_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_sha_idy_2;return; // READ
}

protected static void m6502_device_sha_idy_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_sha_idy_3;return; // READ
}

protected static void m6502_device_sha_idy_3(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_sha_idy_4;return; // READ
}

protected static void m6502_device_sha_idy_4(m6502_device cpu) {
  cpu._tmp2 =(byte)(cpu._a & cpu._x & ((cpu._tmp >> 8)+1));
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._tmp = cpu.set_h((ushort)(cpu._tmp+cpu._y), cpu._tmp2);
  } else {
    cpu._tmp += cpu._y;
  }
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sha_idy_5;return; // WRITE
}

protected static void m6502_device_sha_idy_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op shs_aby --- 
protected static void m6502_device_shs_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_shs_aby_1;return; // READ
}

protected static void m6502_device_shs_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_shs_aby_2;return; // READ
}

protected static void m6502_device_shs_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_shs_aby_3;return; // READ
}

protected static void m6502_device_shs_aby_3(m6502_device cpu) {
  cpu._sp = cpu.set_l(cpu._sp,(byte)(cpu._a & cpu._x));
  cpu._tmp2 =(byte)(cpu._a & cpu._x & ((cpu._tmp >> 8)+1));
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._tmp = cpu.set_h((ushort)(cpu._tmp+cpu._y), cpu._tmp2);
  } else {
    cpu._tmp += cpu._y;
  }
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_shs_aby_4;return; // WRITE
}

protected static void m6502_device_shs_aby_4(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op shx_aby --- 
protected static void m6502_device_shx_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_shx_aby_1;return; // READ
}

protected static void m6502_device_shx_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_shx_aby_2;return; // READ
}

protected static void m6502_device_shx_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_shx_aby_3;return; // READ
}

protected static void m6502_device_shx_aby_3(m6502_device cpu) {
  cpu._tmp2 =(byte)(cpu._x & ((cpu._tmp >> 8)+1));
  if (cpu.page_changing(cpu._tmp, cpu._y)) {
    cpu._tmp = cpu.set_h((ushort)(cpu._tmp+cpu._y), cpu._tmp2);
  } else {
    cpu._tmp += cpu._y;
  }
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_shx_aby_4;return; // WRITE
}

protected static void m6502_device_shx_aby_4(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op shy_abx --- 
protected static void m6502_device_shy_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_shy_abx_1;return; // READ
}

protected static void m6502_device_shy_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_shy_abx_2;return; // READ
}

protected static void m6502_device_shy_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
  cpu.NextFn = &m6502_device_shy_abx_3;return; // READ
}

protected static void m6502_device_shy_abx_3(m6502_device cpu) {
  cpu._tmp2 =(byte)(cpu._y & ((cpu._tmp >> 8)+1));
  if (cpu.page_changing(cpu._tmp, cpu._x)) {
    cpu._tmp = cpu.set_h((ushort)(cpu._tmp+cpu._x), cpu._tmp2);
  } else {
    cpu._tmp += cpu._x;
  }
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_shy_abx_4;return; // WRITE
}

protected static void m6502_device_shy_abx_4(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op slo_aba --- 
protected static void m6502_device_slo_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_slo_aba_1;return; // READ
}

protected static void m6502_device_slo_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_slo_aba_2;return; // READ
}

protected static void m6502_device_slo_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_slo_aba_3;return; // READ
}

protected static void m6502_device_slo_aba_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_slo_aba_4;return; // WRITE
}

protected static void m6502_device_slo_aba_4(m6502_device cpu) {
  cpu._tmp2 = cpu.do_asl(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_slo_aba_5;return; // WRITE
}

protected static void m6502_device_slo_aba_5(m6502_device cpu) {
  cpu._a |= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op slo_abx --- 
protected static void m6502_device_slo_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_slo_abx_1;return; // READ
}

protected static void m6502_device_slo_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_slo_abx_2;return; // READ
}

protected static void m6502_device_slo_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
  cpu.NextFn = &m6502_device_slo_abx_3;return; // READ
}

protected static void m6502_device_slo_abx_3(m6502_device cpu) {
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_slo_abx_4;return; // READ
}

protected static void m6502_device_slo_abx_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_slo_abx_5;return; // WRITE
}

protected static void m6502_device_slo_abx_5(m6502_device cpu) {
  cpu._tmp2 = cpu.do_asl(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_slo_abx_6;return; // WRITE
}

protected static void m6502_device_slo_abx_6(m6502_device cpu) {
  cpu._a |= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op slo_aby --- 
protected static void m6502_device_slo_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_slo_aby_1;return; // READ
}

protected static void m6502_device_slo_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_slo_aby_2;return; // READ
}

protected static void m6502_device_slo_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_slo_aby_3;return; // READ
}

protected static void m6502_device_slo_aby_3(m6502_device cpu) {
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_slo_aby_4;return; // READ
}

protected static void m6502_device_slo_aby_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_slo_aby_5;return; // WRITE
}

protected static void m6502_device_slo_aby_5(m6502_device cpu) {
  cpu._tmp2 = cpu.do_asl(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_slo_aby_6;return; // WRITE
}

protected static void m6502_device_slo_aby_6(m6502_device cpu) {
  cpu._a |= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op slo_idx --- 
protected static void m6502_device_slo_idx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_slo_idx_1;return; // READ
}

protected static void m6502_device_slo_idx_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_slo_idx_2;return; // READ
}

protected static void m6502_device_slo_idx_2(m6502_device cpu) {
  cpu._tmp2 += cpu._x;
  cpu._doread((ushort)(cpu._tmp2 & 0xff));
  cpu.NextFn = &m6502_device_slo_idx_3;return; // READ
}

protected static void m6502_device_slo_idx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_slo_idx_4;return; // READ
}

protected static void m6502_device_slo_idx_4(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_slo_idx_5;return; // READ
}

protected static void m6502_device_slo_idx_5(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_slo_idx_6;return; // WRITE
}

protected static void m6502_device_slo_idx_6(m6502_device cpu) {
  cpu._tmp2 = cpu.do_asl(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_slo_idx_7;return; // WRITE
}

protected static void m6502_device_slo_idx_7(m6502_device cpu) {
  cpu._a |= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op slo_idy --- 
protected static void m6502_device_slo_idy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_slo_idy_1;return; // READ
}

protected static void m6502_device_slo_idy_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_slo_idy_2;return; // READ
}

protected static void m6502_device_slo_idy_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_slo_idy_3;return; // READ
}

protected static void m6502_device_slo_idy_3(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_slo_idy_4;return; // READ
}

protected static void m6502_device_slo_idy_4(m6502_device cpu) {
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_slo_idy_5;return; // READ
}

protected static void m6502_device_slo_idy_5(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_slo_idy_6;return; // WRITE
}

protected static void m6502_device_slo_idy_6(m6502_device cpu) {
  cpu._tmp2 = cpu.do_asl(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_slo_idy_7;return; // WRITE
}

protected static void m6502_device_slo_idy_7(m6502_device cpu) {
  cpu._a |= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op slo_zpg --- 
protected static void m6502_device_slo_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_slo_zpg_1;return; // READ
}

protected static void m6502_device_slo_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_slo_zpg_2;return; // READ
}

protected static void m6502_device_slo_zpg_2(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_slo_zpg_3;return; // WRITE
}

protected static void m6502_device_slo_zpg_3(m6502_device cpu) {
  cpu._tmp2 = cpu.do_asl(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_slo_zpg_4;return; // WRITE
}

protected static void m6502_device_slo_zpg_4(m6502_device cpu) {
  cpu._a |= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op slo_zpx --- 
protected static void m6502_device_slo_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_slo_zpx_1;return; // READ
}

protected static void m6502_device_slo_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_slo_zpx_2;return; // READ
}

protected static void m6502_device_slo_zpx_2(m6502_device cpu) {
  cpu._tmp = (byte)(cpu._tmp+cpu._x);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_slo_zpx_3;return; // READ
}

protected static void m6502_device_slo_zpx_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_slo_zpx_4;return; // WRITE
}

protected static void m6502_device_slo_zpx_4(m6502_device cpu) {
  cpu._tmp2 = cpu.do_asl(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_slo_zpx_5;return; // WRITE
}

protected static void m6502_device_slo_zpx_5(m6502_device cpu) {
  cpu._a |= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sre_aba --- 
protected static void m6502_device_sre_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sre_aba_1;return; // READ
}

protected static void m6502_device_sre_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sre_aba_2;return; // READ
}

protected static void m6502_device_sre_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sre_aba_3;return; // READ
}

protected static void m6502_device_sre_aba_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sre_aba_4;return; // WRITE
}

protected static void m6502_device_sre_aba_4(m6502_device cpu) {
  cpu._tmp2 = cpu.do_lsr(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sre_aba_5;return; // WRITE
}

protected static void m6502_device_sre_aba_5(m6502_device cpu) {
  cpu._a ^= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sre_abx --- 
protected static void m6502_device_sre_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sre_abx_1;return; // READ
}

protected static void m6502_device_sre_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sre_abx_2;return; // READ
}

protected static void m6502_device_sre_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
  cpu.NextFn = &m6502_device_sre_abx_3;return; // READ
}

protected static void m6502_device_sre_abx_3(m6502_device cpu) {
  cpu._tmp += cpu._x;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sre_abx_4;return; // READ
}

protected static void m6502_device_sre_abx_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sre_abx_5;return; // WRITE
}

protected static void m6502_device_sre_abx_5(m6502_device cpu) {
  cpu._tmp2 = cpu.do_lsr(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sre_abx_6;return; // WRITE
}

protected static void m6502_device_sre_abx_6(m6502_device cpu) {
  cpu._a ^= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sre_aby --- 
protected static void m6502_device_sre_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sre_aby_1;return; // READ
}

protected static void m6502_device_sre_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sre_aby_2;return; // READ
}

protected static void m6502_device_sre_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_sre_aby_3;return; // READ
}

protected static void m6502_device_sre_aby_3(m6502_device cpu) {
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sre_aby_4;return; // READ
}

protected static void m6502_device_sre_aby_4(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sre_aby_5;return; // WRITE
}

protected static void m6502_device_sre_aby_5(m6502_device cpu) {
  cpu._tmp2 = cpu.do_lsr(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sre_aby_6;return; // WRITE
}

protected static void m6502_device_sre_aby_6(m6502_device cpu) {
  cpu._a ^= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sre_idx --- 
protected static void m6502_device_sre_idx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sre_idx_1;return; // READ
}

protected static void m6502_device_sre_idx_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_sre_idx_2;return; // READ
}

protected static void m6502_device_sre_idx_2(m6502_device cpu) {
  cpu._tmp2 += cpu._x;
  cpu._doread((ushort)(cpu._tmp2 & 0xff));
  cpu.NextFn = &m6502_device_sre_idx_3;return; // READ
}

protected static void m6502_device_sre_idx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_sre_idx_4;return; // READ
}

protected static void m6502_device_sre_idx_4(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sre_idx_5;return; // READ
}

protected static void m6502_device_sre_idx_5(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sre_idx_6;return; // WRITE
}

protected static void m6502_device_sre_idx_6(m6502_device cpu) {
  cpu._tmp2 = cpu.do_lsr(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sre_idx_7;return; // WRITE
}

protected static void m6502_device_sre_idx_7(m6502_device cpu) {
  cpu._a ^= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sre_idy --- 
protected static void m6502_device_sre_idy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sre_idy_1;return; // READ
}

protected static void m6502_device_sre_idy_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_sre_idy_2;return; // READ
}

protected static void m6502_device_sre_idy_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_sre_idy_3;return; // READ
}

protected static void m6502_device_sre_idy_3(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_sre_idy_4;return; // READ
}

protected static void m6502_device_sre_idy_4(m6502_device cpu) {
  cpu._tmp += cpu._y;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sre_idy_5;return; // READ
}

protected static void m6502_device_sre_idy_5(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sre_idy_6;return; // WRITE
}

protected static void m6502_device_sre_idy_6(m6502_device cpu) {
  cpu._tmp2 = cpu.do_lsr(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sre_idy_7;return; // WRITE
}

protected static void m6502_device_sre_idy_7(m6502_device cpu) {
  cpu._a ^= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sre_zpg --- 
protected static void m6502_device_sre_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sre_zpg_1;return; // READ
}

protected static void m6502_device_sre_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sre_zpg_2;return; // READ
}

protected static void m6502_device_sre_zpg_2(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sre_zpg_3;return; // WRITE
}

protected static void m6502_device_sre_zpg_3(m6502_device cpu) {
  cpu._tmp2 = cpu.do_lsr(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sre_zpg_4;return; // WRITE
}

protected static void m6502_device_sre_zpg_4(m6502_device cpu) {
  cpu._a ^= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sre_zpx --- 
protected static void m6502_device_sre_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sre_zpx_1;return; // READ
}

protected static void m6502_device_sre_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sre_zpx_2;return; // READ
}

protected static void m6502_device_sre_zpx_2(m6502_device cpu) {
  cpu._tmp = (byte)(cpu._tmp+cpu._x);
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sre_zpx_3;return; // READ
}

protected static void m6502_device_sre_zpx_3(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sre_zpx_4;return; // WRITE
}

protected static void m6502_device_sre_zpx_4(m6502_device cpu) {
  cpu._tmp2 = cpu.do_lsr(cpu._tmp2);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._tmp2));
  cpu.NextFn = &m6502_device_sre_zpx_5;return; // WRITE
}

protected static void m6502_device_sre_zpx_5(m6502_device cpu) {
  cpu._a ^= cpu._tmp2;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sta_aba --- 
protected static void m6502_device_sta_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sta_aba_1;return; // READ
}

protected static void m6502_device_sta_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sta_aba_2;return; // READ
}

protected static void m6502_device_sta_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._a));
  cpu.NextFn = &m6502_device_sta_aba_3;return; // WRITE
}

protected static void m6502_device_sta_aba_3(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sta_abx --- 
protected static void m6502_device_sta_abx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sta_abx_1;return; // READ
}

protected static void m6502_device_sta_abx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sta_abx_2;return; // READ
}

protected static void m6502_device_sta_abx_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._x))));
  cpu.NextFn = &m6502_device_sta_abx_3;return; // READ
}

protected static void m6502_device_sta_abx_3(m6502_device cpu) {
  cpu._dowrite((ushort)(cpu._tmp+cpu._x),(byte)( cpu._a));
  cpu.NextFn = &m6502_device_sta_abx_4;return; // WRITE
}

protected static void m6502_device_sta_abx_4(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sta_aby --- 
protected static void m6502_device_sta_aby_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sta_aby_1;return; // READ
}

protected static void m6502_device_sta_aby_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sta_aby_2;return; // READ
}

protected static void m6502_device_sta_aby_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_sta_aby_3;return; // READ
}

protected static void m6502_device_sta_aby_3(m6502_device cpu) {
  cpu._dowrite((ushort)(cpu._tmp+cpu._y),(byte)( cpu._a));
  cpu.NextFn = &m6502_device_sta_aby_4;return; // WRITE
}

protected static void m6502_device_sta_aby_4(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sta_idx --- 
protected static void m6502_device_sta_idx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sta_idx_1;return; // READ
}

protected static void m6502_device_sta_idx_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_sta_idx_2;return; // READ
}

protected static void m6502_device_sta_idx_2(m6502_device cpu) {
  cpu._tmp2 += cpu._x;
  cpu._doread((ushort)(cpu._tmp2 & 0xff));
  cpu.NextFn = &m6502_device_sta_idx_3;return; // READ
}

protected static void m6502_device_sta_idx_3(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_sta_idx_4;return; // READ
}

protected static void m6502_device_sta_idx_4(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._a));
  cpu.NextFn = &m6502_device_sta_idx_5;return; // WRITE
}

protected static void m6502_device_sta_idx_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sta_idy --- 
protected static void m6502_device_sta_idy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sta_idy_1;return; // READ
}

protected static void m6502_device_sta_idy_1(m6502_device cpu) {
  cpu._tmp2 = cpu._dat;
  cpu._doread((ushort)(cpu._tmp2));
  cpu.NextFn = &m6502_device_sta_idy_2;return; // READ
}

protected static void m6502_device_sta_idy_2(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)((cpu._tmp2+1) & 0xff));
  cpu.NextFn = &m6502_device_sta_idy_3;return; // READ
}

protected static void m6502_device_sta_idy_3(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._doread((ushort)(cpu.set_l(cpu._tmp,(byte)(cpu._tmp+cpu._y))));
  cpu.NextFn = &m6502_device_sta_idy_4;return; // READ
}

protected static void m6502_device_sta_idy_4(m6502_device cpu) {
  cpu._dowrite((ushort)(cpu._tmp+cpu._y),(byte)( cpu._a));
  cpu.NextFn = &m6502_device_sta_idy_5;return; // WRITE
}

protected static void m6502_device_sta_idy_5(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sta_zpg --- 
protected static void m6502_device_sta_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sta_zpg_1;return; // READ
}

protected static void m6502_device_sta_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._a));
  cpu.NextFn = &m6502_device_sta_zpg_2;return; // WRITE
}

protected static void m6502_device_sta_zpg_2(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sta_zpx --- 
protected static void m6502_device_sta_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sta_zpx_1;return; // READ
}

protected static void m6502_device_sta_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sta_zpx_2;return; // READ
}

protected static void m6502_device_sta_zpx_2(m6502_device cpu) {
  cpu._dowrite((ushort)((byte)(cpu._tmp+cpu._x)),(byte)( cpu._a));
  cpu.NextFn = &m6502_device_sta_zpx_3;return; // WRITE
}

protected static void m6502_device_sta_zpx_3(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op stx_aba --- 
protected static void m6502_device_stx_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_stx_aba_1;return; // READ
}

protected static void m6502_device_stx_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_stx_aba_2;return; // READ
}

protected static void m6502_device_stx_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._x));
  cpu.NextFn = &m6502_device_stx_aba_3;return; // WRITE
}

protected static void m6502_device_stx_aba_3(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op stx_zpg --- 
protected static void m6502_device_stx_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_stx_zpg_1;return; // READ
}

protected static void m6502_device_stx_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._x));
  cpu.NextFn = &m6502_device_stx_zpg_2;return; // WRITE
}

protected static void m6502_device_stx_zpg_2(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op stx_zpy --- 
protected static void m6502_device_stx_zpy_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_stx_zpy_1;return; // READ
}

protected static void m6502_device_stx_zpy_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_stx_zpy_2;return; // READ
}

protected static void m6502_device_stx_zpy_2(m6502_device cpu) {
  cpu._dowrite((ushort)((byte)(cpu._tmp+cpu._y)),(byte)( cpu._x));
  cpu.NextFn = &m6502_device_stx_zpy_3;return; // WRITE
}

protected static void m6502_device_stx_zpy_3(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sty_aba --- 
protected static void m6502_device_sty_aba_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sty_aba_1;return; // READ
}

protected static void m6502_device_sty_aba_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sty_aba_2;return; // READ
}

protected static void m6502_device_sty_aba_2(m6502_device cpu) {
  cpu._tmp = cpu.set_h(cpu._tmp, cpu._dat);
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._y));
  cpu.NextFn = &m6502_device_sty_aba_3;return; // WRITE
}

protected static void m6502_device_sty_aba_3(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sty_zpg --- 
protected static void m6502_device_sty_zpg_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sty_zpg_1;return; // READ
}

protected static void m6502_device_sty_zpg_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._dowrite((ushort)(cpu._tmp),(byte)( cpu._y));
  cpu.NextFn = &m6502_device_sty_zpg_2;return; // WRITE
}

protected static void m6502_device_sty_zpg_2(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op sty_zpx --- 
protected static void m6502_device_sty_zpx_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc++));
  cpu.NextFn = &m6502_device_sty_zpx_1;return; // READ
}

protected static void m6502_device_sty_zpx_1(m6502_device cpu) {
  cpu._tmp = cpu._dat;
  cpu._doread((ushort)(cpu._tmp));
  cpu.NextFn = &m6502_device_sty_zpx_2;return; // READ
}

protected static void m6502_device_sty_zpx_2(m6502_device cpu) {
  cpu._dowrite((ushort)((byte)(cpu._tmp+cpu._x)),(byte)( cpu._y));
  cpu.NextFn = &m6502_device_sty_zpx_3;return; // WRITE
}

protected static void m6502_device_sty_zpx_3(m6502_device cpu) {
  m6502_device_fetch(cpu);return; // fetch
}

// --- op tax_imp --- 
protected static void m6502_device_tax_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_tax_imp_1;return; // READ
}

protected static void m6502_device_tax_imp_1(m6502_device cpu) {
  cpu._x = cpu._a;
  cpu.set_nz(cpu._x);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op tay_imp --- 
protected static void m6502_device_tay_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_tay_imp_1;return; // READ
}

protected static void m6502_device_tay_imp_1(m6502_device cpu) {
  cpu._y = cpu._a;
  cpu.set_nz(cpu._y);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op tsx_imp --- 
protected static void m6502_device_tsx_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_tsx_imp_1;return; // READ
}

protected static void m6502_device_tsx_imp_1(m6502_device cpu) {
  cpu._x = (byte)cpu._sp;
  cpu.set_nz(cpu._x);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op txa_imp --- 
protected static void m6502_device_txa_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_txa_imp_1;return; // READ
}

protected static void m6502_device_txa_imp_1(m6502_device cpu) {
  cpu._a = cpu._x;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

// --- op txs_imp --- 
protected static void m6502_device_txs_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_txs_imp_1;return; // READ
}

protected static void m6502_device_txs_imp_1(m6502_device cpu) {
  cpu._sp = cpu.set_l(cpu._sp,(byte)(cpu._x));
  m6502_device_fetch(cpu);return; // fetch
}

// --- op tya_imp --- 
protected static void m6502_device_tya_imp_0(m6502_device cpu) {
  cpu._doread((ushort)(cpu._pc));
  cpu.NextFn = &m6502_device_tya_imp_1;return; // READ
}

protected static void m6502_device_tya_imp_1(m6502_device cpu) {
  cpu._a = cpu._y;
  cpu.set_nz(cpu._a);
  m6502_device_fetch(cpu);return; // fetch
}

		protected override void postfetch_int() {
			switch(_ir) {
				case 0x00: m6502_device_brk_imp_0(this); break;
				case 0x01: m6502_device_ora_idx_0(this); break;
				case 0x02: m6502_device_kil_non_0(this); break;
				case 0x03: m6502_device_slo_idx_0(this); break;
				case 0x04: m6502_device_nop_zpg_0(this); break;
				case 0x05: m6502_device_ora_zpg_0(this); break;
				case 0x06: m6502_device_asl_zpg_0(this); break;
				case 0x07: m6502_device_slo_zpg_0(this); break;
				case 0x08: m6502_device_php_imp_0(this); break;
				case 0x09: m6502_device_ora_imm_0(this); break;
				case 0x0a: m6502_device_asl_acc_0(this); break;
				case 0x0b: m6502_device_anc_imm_0(this); break;
				case 0x0c: m6502_device_nop_aba_0(this); break;
				case 0x0d: m6502_device_ora_aba_0(this); break;
				case 0x0e: m6502_device_asl_aba_0(this); break;
				case 0x0f: m6502_device_slo_aba_0(this); break;
				case 0x10: m6502_device_bpl_rel_0(this); break;
				case 0x11: m6502_device_ora_idy_0(this); break;
				case 0x12: m6502_device_kil_non_0(this); break;
				case 0x13: m6502_device_slo_idy_0(this); break;
				case 0x14: m6502_device_nop_zpx_0(this); break;
				case 0x15: m6502_device_ora_zpx_0(this); break;
				case 0x16: m6502_device_asl_zpx_0(this); break;
				case 0x17: m6502_device_slo_zpx_0(this); break;
				case 0x18: m6502_device_clc_imp_0(this); break;
				case 0x19: m6502_device_ora_aby_0(this); break;
				case 0x1a: m6502_device_nop_imp_0(this); break;
				case 0x1b: m6502_device_slo_aby_0(this); break;
				case 0x1c: m6502_device_nop_abx_0(this); break;
				case 0x1d: m6502_device_ora_abx_0(this); break;
				case 0x1e: m6502_device_asl_abx_0(this); break;
				case 0x1f: m6502_device_slo_abx_0(this); break;
				case 0x20: m6502_device_jsr_adr_0(this); break;
				case 0x21: m6502_device_and_idx_0(this); break;
				case 0x22: m6502_device_kil_non_0(this); break;
				case 0x23: m6502_device_rla_idx_0(this); break;
				case 0x24: m6502_device_bit_zpg_0(this); break;
				case 0x25: m6502_device_and_zpg_0(this); break;
				case 0x26: m6502_device_rol_zpg_0(this); break;
				case 0x27: m6502_device_rla_zpg_0(this); break;
				case 0x28: m6502_device_plp_imp_0(this); break;
				case 0x29: m6502_device_and_imm_0(this); break;
				case 0x2a: m6502_device_rol_acc_0(this); break;
				case 0x2b: m6502_device_anc_imm_0(this); break;
				case 0x2c: m6502_device_bit_aba_0(this); break;
				case 0x2d: m6502_device_and_aba_0(this); break;
				case 0x2e: m6502_device_rol_aba_0(this); break;
				case 0x2f: m6502_device_rla_aba_0(this); break;
				case 0x30: m6502_device_bmi_rel_0(this); break;
				case 0x31: m6502_device_and_idy_0(this); break;
				case 0x32: m6502_device_kil_non_0(this); break;
				case 0x33: m6502_device_rla_idy_0(this); break;
				case 0x34: m6502_device_nop_zpx_0(this); break;
				case 0x35: m6502_device_and_zpx_0(this); break;
				case 0x36: m6502_device_rol_zpx_0(this); break;
				case 0x37: m6502_device_rla_zpx_0(this); break;
				case 0x38: m6502_device_sec_imp_0(this); break;
				case 0x39: m6502_device_and_aby_0(this); break;
				case 0x3a: m6502_device_nop_imp_0(this); break;
				case 0x3b: m6502_device_rla_aby_0(this); break;
				case 0x3c: m6502_device_nop_abx_0(this); break;
				case 0x3d: m6502_device_and_abx_0(this); break;
				case 0x3e: m6502_device_rol_abx_0(this); break;
				case 0x3f: m6502_device_rla_abx_0(this); break;
				case 0x40: m6502_device_rti_imp_0(this); break;
				case 0x41: m6502_device_eor_idx_0(this); break;
				case 0x42: m6502_device_kil_non_0(this); break;
				case 0x43: m6502_device_sre_idx_0(this); break;
				case 0x44: m6502_device_nop_zpg_0(this); break;
				case 0x45: m6502_device_eor_zpg_0(this); break;
				case 0x46: m6502_device_lsr_zpg_0(this); break;
				case 0x47: m6502_device_sre_zpg_0(this); break;
				case 0x48: m6502_device_pha_imp_0(this); break;
				case 0x49: m6502_device_eor_imm_0(this); break;
				case 0x4a: m6502_device_lsr_acc_0(this); break;
				case 0x4b: m6502_device_asr_imm_0(this); break;
				case 0x4c: m6502_device_jmp_adr_0(this); break;
				case 0x4d: m6502_device_eor_aba_0(this); break;
				case 0x4e: m6502_device_lsr_aba_0(this); break;
				case 0x4f: m6502_device_sre_aba_0(this); break;
				case 0x50: m6502_device_bvc_rel_0(this); break;
				case 0x51: m6502_device_eor_idy_0(this); break;
				case 0x52: m6502_device_kil_non_0(this); break;
				case 0x53: m6502_device_sre_idy_0(this); break;
				case 0x54: m6502_device_nop_zpx_0(this); break;
				case 0x55: m6502_device_eor_zpx_0(this); break;
				case 0x56: m6502_device_lsr_zpx_0(this); break;
				case 0x57: m6502_device_sre_zpx_0(this); break;
				case 0x58: m6502_device_cli_imp_0(this); break;
				case 0x59: m6502_device_eor_aby_0(this); break;
				case 0x5a: m6502_device_nop_imp_0(this); break;
				case 0x5b: m6502_device_sre_aby_0(this); break;
				case 0x5c: m6502_device_nop_abx_0(this); break;
				case 0x5d: m6502_device_eor_abx_0(this); break;
				case 0x5e: m6502_device_lsr_abx_0(this); break;
				case 0x5f: m6502_device_sre_abx_0(this); break;
				case 0x60: m6502_device_rts_imp_0(this); break;
				case 0x61: m6502_device_adc_idx_0(this); break;
				case 0x62: m6502_device_kil_non_0(this); break;
				case 0x63: m6502_device_rra_idx_0(this); break;
				case 0x64: m6502_device_nop_zpg_0(this); break;
				case 0x65: m6502_device_adc_zpg_0(this); break;
				case 0x66: m6502_device_ror_zpg_0(this); break;
				case 0x67: m6502_device_rra_zpg_0(this); break;
				case 0x68: m6502_device_pla_imp_0(this); break;
				case 0x69: m6502_device_adc_imm_0(this); break;
				case 0x6a: m6502_device_ror_acc_0(this); break;
				case 0x6b: m6502_device_arr_imm_0(this); break;
				case 0x6c: m6502_device_jmp_ind_0(this); break;
				case 0x6d: m6502_device_adc_aba_0(this); break;
				case 0x6e: m6502_device_ror_aba_0(this); break;
				case 0x6f: m6502_device_rra_aba_0(this); break;
				case 0x70: m6502_device_bvs_rel_0(this); break;
				case 0x71: m6502_device_adc_idy_0(this); break;
				case 0x72: m6502_device_kil_non_0(this); break;
				case 0x73: m6502_device_rra_idy_0(this); break;
				case 0x74: m6502_device_nop_zpx_0(this); break;
				case 0x75: m6502_device_adc_zpx_0(this); break;
				case 0x76: m6502_device_ror_zpx_0(this); break;
				case 0x77: m6502_device_rra_zpx_0(this); break;
				case 0x78: m6502_device_sei_imp_0(this); break;
				case 0x79: m6502_device_adc_aby_0(this); break;
				case 0x7a: m6502_device_nop_imp_0(this); break;
				case 0x7b: m6502_device_rra_aby_0(this); break;
				case 0x7c: m6502_device_nop_abx_0(this); break;
				case 0x7d: m6502_device_adc_abx_0(this); break;
				case 0x7e: m6502_device_ror_abx_0(this); break;
				case 0x7f: m6502_device_rra_abx_0(this); break;
				case 0x80: m6502_device_nop_imm_0(this); break;
				case 0x81: m6502_device_sta_idx_0(this); break;
				case 0x82: m6502_device_nop_imm_0(this); break;
				case 0x83: m6502_device_sax_idx_0(this); break;
				case 0x84: m6502_device_sty_zpg_0(this); break;
				case 0x85: m6502_device_sta_zpg_0(this); break;
				case 0x86: m6502_device_stx_zpg_0(this); break;
				case 0x87: m6502_device_sax_zpg_0(this); break;
				case 0x88: m6502_device_dey_imp_0(this); break;
				case 0x89: m6502_device_nop_imm_0(this); break;
				case 0x8a: m6502_device_txa_imp_0(this); break;
				case 0x8b: m6502_device_ane_imm_0(this); break;
				case 0x8c: m6502_device_sty_aba_0(this); break;
				case 0x8d: m6502_device_sta_aba_0(this); break;
				case 0x8e: m6502_device_stx_aba_0(this); break;
				case 0x8f: m6502_device_sax_aba_0(this); break;
				case 0x90: m6502_device_bcc_rel_0(this); break;
				case 0x91: m6502_device_sta_idy_0(this); break;
				case 0x92: m6502_device_kil_non_0(this); break;
				case 0x93: m6502_device_sha_idy_0(this); break;
				case 0x94: m6502_device_sty_zpx_0(this); break;
				case 0x95: m6502_device_sta_zpx_0(this); break;
				case 0x96: m6502_device_stx_zpy_0(this); break;
				case 0x97: m6502_device_sax_zpy_0(this); break;
				case 0x98: m6502_device_tya_imp_0(this); break;
				case 0x99: m6502_device_sta_aby_0(this); break;
				case 0x9a: m6502_device_txs_imp_0(this); break;
				case 0x9b: m6502_device_shs_aby_0(this); break;
				case 0x9c: m6502_device_shy_abx_0(this); break;
				case 0x9d: m6502_device_sta_abx_0(this); break;
				case 0x9e: m6502_device_shx_aby_0(this); break;
				case 0x9f: m6502_device_sha_aby_0(this); break;
				case 0xa0: m6502_device_ldy_imm_0(this); break;
				case 0xa1: m6502_device_lda_idx_0(this); break;
				case 0xa2: m6502_device_ldx_imm_0(this); break;
				case 0xa3: m6502_device_lax_idx_0(this); break;
				case 0xa4: m6502_device_ldy_zpg_0(this); break;
				case 0xa5: m6502_device_lda_zpg_0(this); break;
				case 0xa6: m6502_device_ldx_zpg_0(this); break;
				case 0xa7: m6502_device_lax_zpg_0(this); break;
				case 0xa8: m6502_device_tay_imp_0(this); break;
				case 0xa9: m6502_device_lda_imm_0(this); break;
				case 0xaa: m6502_device_tax_imp_0(this); break;
				case 0xab: m6502_device_lxa_imm_0(this); break;
				case 0xac: m6502_device_ldy_aba_0(this); break;
				case 0xad: m6502_device_lda_aba_0(this); break;
				case 0xae: m6502_device_ldx_aba_0(this); break;
				case 0xaf: m6502_device_lax_aba_0(this); break;
				case 0xb0: m6502_device_bcs_rel_0(this); break;
				case 0xb1: m6502_device_lda_idy_0(this); break;
				case 0xb2: m6502_device_kil_non_0(this); break;
				case 0xb3: m6502_device_lax_idy_0(this); break;
				case 0xb4: m6502_device_ldy_zpx_0(this); break;
				case 0xb5: m6502_device_lda_zpx_0(this); break;
				case 0xb6: m6502_device_ldx_zpy_0(this); break;
				case 0xb7: m6502_device_lax_zpy_0(this); break;
				case 0xb8: m6502_device_clv_imp_0(this); break;
				case 0xb9: m6502_device_lda_aby_0(this); break;
				case 0xba: m6502_device_tsx_imp_0(this); break;
				case 0xbb: m6502_device_las_aby_0(this); break;
				case 0xbc: m6502_device_ldy_abx_0(this); break;
				case 0xbd: m6502_device_lda_abx_0(this); break;
				case 0xbe: m6502_device_ldx_aby_0(this); break;
				case 0xbf: m6502_device_lax_aby_0(this); break;
				case 0xc0: m6502_device_cpy_imm_0(this); break;
				case 0xc1: m6502_device_cmp_idx_0(this); break;
				case 0xc2: m6502_device_nop_imm_0(this); break;
				case 0xc3: m6502_device_dcp_idx_0(this); break;
				case 0xc4: m6502_device_cpy_zpg_0(this); break;
				case 0xc5: m6502_device_cmp_zpg_0(this); break;
				case 0xc6: m6502_device_dec_zpg_0(this); break;
				case 0xc7: m6502_device_dcp_zpg_0(this); break;
				case 0xc8: m6502_device_iny_imp_0(this); break;
				case 0xc9: m6502_device_cmp_imm_0(this); break;
				case 0xca: m6502_device_dex_imp_0(this); break;
				case 0xcb: m6502_device_sbx_imm_0(this); break;
				case 0xcc: m6502_device_cpy_aba_0(this); break;
				case 0xcd: m6502_device_cmp_aba_0(this); break;
				case 0xce: m6502_device_dec_aba_0(this); break;
				case 0xcf: m6502_device_dcp_aba_0(this); break;
				case 0xd0: m6502_device_bne_rel_0(this); break;
				case 0xd1: m6502_device_cmp_idy_0(this); break;
				case 0xd2: m6502_device_kil_non_0(this); break;
				case 0xd3: m6502_device_dcp_idy_0(this); break;
				case 0xd4: m6502_device_nop_zpx_0(this); break;
				case 0xd5: m6502_device_cmp_zpx_0(this); break;
				case 0xd6: m6502_device_dec_zpx_0(this); break;
				case 0xd7: m6502_device_dcp_zpx_0(this); break;
				case 0xd8: m6502_device_cld_imp_0(this); break;
				case 0xd9: m6502_device_cmp_aby_0(this); break;
				case 0xda: m6502_device_nop_imp_0(this); break;
				case 0xdb: m6502_device_dcp_aby_0(this); break;
				case 0xdc: m6502_device_nop_abx_0(this); break;
				case 0xdd: m6502_device_cmp_abx_0(this); break;
				case 0xde: m6502_device_dec_abx_0(this); break;
				case 0xdf: m6502_device_dcp_abx_0(this); break;
				case 0xe0: m6502_device_cpx_imm_0(this); break;
				case 0xe1: m6502_device_sbc_idx_0(this); break;
				case 0xe2: m6502_device_nop_imm_0(this); break;
				case 0xe3: m6502_device_isb_idx_0(this); break;
				case 0xe4: m6502_device_cpx_zpg_0(this); break;
				case 0xe5: m6502_device_sbc_zpg_0(this); break;
				case 0xe6: m6502_device_inc_zpg_0(this); break;
				case 0xe7: m6502_device_isb_zpg_0(this); break;
				case 0xe8: m6502_device_inx_imp_0(this); break;
				case 0xe9: m6502_device_sbc_imm_0(this); break;
				case 0xea: m6502_device_nop_imp_0(this); break;
				case 0xeb: m6502_device_sbc_imm_0(this); break;
				case 0xec: m6502_device_cpx_aba_0(this); break;
				case 0xed: m6502_device_sbc_aba_0(this); break;
				case 0xee: m6502_device_inc_aba_0(this); break;
				case 0xef: m6502_device_isb_aba_0(this); break;
				case 0xf0: m6502_device_beq_rel_0(this); break;
				case 0xf1: m6502_device_sbc_idy_0(this); break;
				case 0xf2: m6502_device_kil_non_0(this); break;
				case 0xf3: m6502_device_isb_idy_0(this); break;
				case 0xf4: m6502_device_nop_zpx_0(this); break;
				case 0xf5: m6502_device_sbc_zpx_0(this); break;
				case 0xf6: m6502_device_inc_zpx_0(this); break;
				case 0xf7: m6502_device_isb_zpx_0(this); break;
				case 0xf8: m6502_device_sed_imp_0(this); break;
				case 0xf9: m6502_device_sbc_aby_0(this); break;
				case 0xfa: m6502_device_nop_imp_0(this); break;
				case 0xfb: m6502_device_isb_aby_0(this); break;
				case 0xfc: m6502_device_nop_abx_0(this); break;
				case 0xfd: m6502_device_sbc_abx_0(this); break;
				case 0xfe: m6502_device_inc_abx_0(this); break;
				case 0xff: m6502_device_isb_abx_0(this); break;
				default:   m6502_device_reset_0(this); break;
		}
	}

		public override DisOpDetails[] OpCodes { get { return new DisOpDetails[] {
			new DisOpDetails() { Mnemonic = "brk", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "ora", OperandType = DisOperandType.IndirectX }
		,	new DisOpDetails() { Mnemonic = "kil", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "slo", OperandType = DisOperandType.IndirectX }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "ora", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "asl", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "slo", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "php", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "ora", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "asl", OperandType = DisOperandType.Accumulator }
		,	new DisOpDetails() { Mnemonic = "anc", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "ora", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "asl", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "slo", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "bpl", OperandType = DisOperandType.Relative }
		,	new DisOpDetails() { Mnemonic = "ora", OperandType = DisOperandType.IndirectY }
		,	new DisOpDetails() { Mnemonic = "kil", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "slo", OperandType = DisOperandType.IndirectY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "ora", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "asl", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "slo", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "clc", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "ora", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "slo", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "ora", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "asl", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "slo", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "jsr", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "and", OperandType = DisOperandType.IndirectX }
		,	new DisOpDetails() { Mnemonic = "kil", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "rla", OperandType = DisOperandType.IndirectX }
		,	new DisOpDetails() { Mnemonic = "bit", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "and", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "rol", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "rla", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "plp", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "and", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "rol", OperandType = DisOperandType.Accumulator }
		,	new DisOpDetails() { Mnemonic = "anc", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "bit", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "and", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "rol", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "rla", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "bmi", OperandType = DisOperandType.Relative }
		,	new DisOpDetails() { Mnemonic = "and", OperandType = DisOperandType.IndirectY }
		,	new DisOpDetails() { Mnemonic = "kil", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "rla", OperandType = DisOperandType.IndirectY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "and", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "rol", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "rla", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "sec", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "and", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "rla", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "and", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "rol", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "rla", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "rti", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "eor", OperandType = DisOperandType.IndirectX }
		,	new DisOpDetails() { Mnemonic = "kil", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "sre", OperandType = DisOperandType.IndirectX }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "eor", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "lsr", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "sre", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "pha", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "eor", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "lsr", OperandType = DisOperandType.Accumulator }
		,	new DisOpDetails() { Mnemonic = "asr", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "jmp", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "eor", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "lsr", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "sre", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "bvc", OperandType = DisOperandType.Relative }
		,	new DisOpDetails() { Mnemonic = "eor", OperandType = DisOperandType.IndirectY }
		,	new DisOpDetails() { Mnemonic = "kil", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "sre", OperandType = DisOperandType.IndirectY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "eor", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "lsr", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "sre", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "cli", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "eor", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "sre", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "eor", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "lsr", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "sre", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "rts", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "adc", OperandType = DisOperandType.IndirectX }
		,	new DisOpDetails() { Mnemonic = "kil", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "rra", OperandType = DisOperandType.IndirectX }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "adc", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "ror", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "rra", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "pla", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "adc", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "ror", OperandType = DisOperandType.Accumulator }
		,	new DisOpDetails() { Mnemonic = "arr", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "jmp", OperandType = DisOperandType.Indirect }
		,	new DisOpDetails() { Mnemonic = "adc", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "ror", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "rra", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "bvs", OperandType = DisOperandType.Relative }
		,	new DisOpDetails() { Mnemonic = "adc", OperandType = DisOperandType.IndirectY }
		,	new DisOpDetails() { Mnemonic = "kil", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "rra", OperandType = DisOperandType.IndirectY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "adc", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "ror", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "rra", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "sei", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "adc", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "rra", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "adc", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "ror", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "rra", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "sta", OperandType = DisOperandType.IndirectX }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "sax", OperandType = DisOperandType.IndirectX }
		,	new DisOpDetails() { Mnemonic = "sty", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "sta", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "stx", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "sax", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "dey", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "txa", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "ane", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "sty", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "sta", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "stx", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "sax", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "bcc", OperandType = DisOperandType.Relative }
		,	new DisOpDetails() { Mnemonic = "sta", OperandType = DisOperandType.IndirectY }
		,	new DisOpDetails() { Mnemonic = "kil", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "sha", OperandType = DisOperandType.IndirectY }
		,	new DisOpDetails() { Mnemonic = "sty", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "sta", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "stx", OperandType = DisOperandType.ZeropageY }
		,	new DisOpDetails() { Mnemonic = "sax", OperandType = DisOperandType.ZeropageY }
		,	new DisOpDetails() { Mnemonic = "tya", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "sta", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "txs", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "shs", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "shy", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "sta", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "shx", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "sha", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "ldy", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "lda", OperandType = DisOperandType.IndirectX }
		,	new DisOpDetails() { Mnemonic = "ldx", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "lax", OperandType = DisOperandType.IndirectX }
		,	new DisOpDetails() { Mnemonic = "ldy", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "lda", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "ldx", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "lax", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "tay", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "lda", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "tax", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "lxa", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "ldy", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "lda", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "ldx", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "lax", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "bcs", OperandType = DisOperandType.Relative }
		,	new DisOpDetails() { Mnemonic = "lda", OperandType = DisOperandType.IndirectY }
		,	new DisOpDetails() { Mnemonic = "kil", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "lax", OperandType = DisOperandType.IndirectY }
		,	new DisOpDetails() { Mnemonic = "ldy", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "lda", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "ldx", OperandType = DisOperandType.ZeropageY }
		,	new DisOpDetails() { Mnemonic = "lax", OperandType = DisOperandType.ZeropageY }
		,	new DisOpDetails() { Mnemonic = "clv", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "lda", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "tsx", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "las", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "ldy", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "lda", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "ldx", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "lax", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "cpy", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "cmp", OperandType = DisOperandType.IndirectX }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "dcp", OperandType = DisOperandType.IndirectX }
		,	new DisOpDetails() { Mnemonic = "cpy", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "cmp", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "dec", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "dcp", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "iny", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "cmp", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "dex", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "sbx", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "cpy", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "cmp", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "dec", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "dcp", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "bne", OperandType = DisOperandType.Relative }
		,	new DisOpDetails() { Mnemonic = "cmp", OperandType = DisOperandType.IndirectY }
		,	new DisOpDetails() { Mnemonic = "kil", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "dcp", OperandType = DisOperandType.IndirectY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "cmp", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "dec", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "dcp", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "cld", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "cmp", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "dcp", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "cmp", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "dec", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "dcp", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "cpx", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "sbc", OperandType = DisOperandType.IndirectX }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "isb", OperandType = DisOperandType.IndirectX }
		,	new DisOpDetails() { Mnemonic = "cpx", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "sbc", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "inc", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "isb", OperandType = DisOperandType.Zeropage }
		,	new DisOpDetails() { Mnemonic = "inx", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "sbc", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "sbc", OperandType = DisOperandType.Immediate }
		,	new DisOpDetails() { Mnemonic = "cpx", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "sbc", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "inc", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "isb", OperandType = DisOperandType.Absolute }
		,	new DisOpDetails() { Mnemonic = "beq", OperandType = DisOperandType.Relative }
		,	new DisOpDetails() { Mnemonic = "sbc", OperandType = DisOperandType.IndirectY }
		,	new DisOpDetails() { Mnemonic = "kil", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "isb", OperandType = DisOperandType.IndirectY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "sbc", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "inc", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "isb", OperandType = DisOperandType.ZeropageX }
		,	new DisOpDetails() { Mnemonic = "sed", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "sbc", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.Implied }
		,	new DisOpDetails() { Mnemonic = "isb", OperandType = DisOperandType.AbsoluteY }
		,	new DisOpDetails() { Mnemonic = "nop", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "sbc", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "inc", OperandType = DisOperandType.AbsoluteX }
		,	new DisOpDetails() { Mnemonic = "isb", OperandType = DisOperandType.AbsoluteX }
		};}}
	}
}
