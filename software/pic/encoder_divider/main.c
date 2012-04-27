/**
 * Stasis Wheel Encoder Divider
 * ----------------------------------------------------------------------------
 * Simple program that divides the pulse output of the wheel encoders so the
 * Netduino doesn't get as many pulses per second.
 *
 */

#include "tsilib.h"

/**
 * Default clock parameters
 *
 */
#pragma config FNOSC = FRCPLL
#pragma config FPLLIDIV = DIV_2, FPLLMUL = MUL_20, FPLLODIV = DIV_1
#pragma config FPBDIV = DIV_2, FWDTEN = OFF, CP = OFF, BWP = OFF
#pragma config ICESEL = ICS_PGx1, FCKSM = CSECMD

// Target count we want to reach before passing on edge changes
#define TARGET_COUNTER 40

// Data structure for encoder inputs
typedef struct _encoder_input
{
	int counter;
	int current_value;
	int last_value;
}encoder_input;

static encoder_input leftA = {0, 0, -1};
static encoder_input leftB = {TARGET_COUNTER/4, 0, -1};
static encoder_input rightA = {0, 0, -1};
static encoder_input rightB = {TARGET_COUNTER/4, 0, -1};

static int handle_encoder_input(encoder_input *input, int lastValue, int newValue);

void main(void)
{
	// Initialize tsilib. This should get us 80MHz with internal FRC
	tsilib_config_t config = TSILIB_DEFAULT_CONFIG;
	tsilib_init(config);

	TRISEbits.TRISE0 = 1;
	TRISEbits.TRISE2 = 1;
	TRISEbits.TRISE4 = 1;
	TRISEbits.TRISE6 = 1;

	TRISEbits.TRISE1 = 0;
	TRISEbits.TRISE3 = 0;
	TRISEbits.TRISE5 = 0;
	TRISEbits.TRISE7 = 0;

	LATEbits.LATE1 = 0;
	LATEbits.LATE3 = 0;
	LATEbits.LATE5 = 0;
	LATEbits.LATE7 = 0;
	
	while(1)
	{
		UINT32_VAL port;
		port.Val = PORTE;

		port.bits.b1 = handle_encoder_input(&leftA, port.bits.b1, port.bits.b0);
		port.bits.b3 = handle_encoder_input(&leftB, port.bits.b3, port.bits.b2);
		port.bits.b5 = handle_encoder_input(&rightA, port.bits.b5, port.bits.b4);
		port.bits.b7 = handle_encoder_input(&rightB, port.bits.b7, port.bits.b6);

		LATE = port.Val;
	}
}

static int handle_encoder_input(encoder_input *input, int lastValue, int newValue)
{
	int ret = lastValue;

	// Save new value
	input->current_value = newValue;

	// If we have a last value, then we can check for edges
	if(input->last_value != -1)
	{
		// Have last value, check for a rising edge
		if(input->last_value == 0 && input->current_value == 1)
		{
			// Rising Edge
			input->counter++;
		}
		if(input->last_value == 1 && input->current_value == 0 && input->counter == (TARGET_COUNTER / 2))
		{
			// Falling edge, reset pin
			ret = 0;
		}
		if(input->counter >= TARGET_COUNTER)
		{
			// Reached target counter, make a rising edge on output
			input->counter = 0;
			ret = 1;
		}
	}

	// Save current value as last value
	input->last_value = input->current_value;

	// All done
	return ret;
}