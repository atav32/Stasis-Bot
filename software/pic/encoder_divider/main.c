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
#define TARGET_COUNTER 5

// Configu stuff for change notification pins
#define CONFIG          (CN_ON | CN_IDLE_CON)
#define PINS            (CN8_ENABLE | CN10_ENABLE)
#define PULLUPS         (CN8_PULLUP_ENABLE | CN10_PULLUP_ENABLE)
#define INTERRUPT       (CHANGE_INT_ON | CHANGE_INT_PRI_2)

// Encoder Data
typedef struct __encoder_input
{
	UINT32	counter;
	int	    pulse_out;
	int  	direction;
	int		last_value;
}encoder_input;

// Encoders
static encoder_input leftEncoder = {0, 0, 0, -1};
static encoder_input rightEncoder = {0, 0, 0, -1};

// Encoder input function
static void handle_encoder_input(encoder_input *input, int channelA, int channelB);

// Data out
// This gets updated whenever G8 is set to high
// which means the Netduino is requesting data. Counters for encoders could be
// updating in the meanwhile so we need to save the value at this instant
static UINT32 dataOut = 0;

// Data out counter
// This keeps a track of how much of the two bytes of data we send out
// we've sent out on the clock so far.
static UINT32 dataOutCounter = 0;

//
// Main loop
//
void main(void)
{
	// Initialize tsilib. This should get us 80MHz with internal FRC
	tsilib_config_t config = TSILIB_DEFAULT_CONFIG;
	tsilib_init(config);

	// Input pins for original encoder inputs
	TRISEbits.TRISE0 = 1;
	TRISEbits.TRISE2 = 1;
	TRISEbits.TRISE4 = 1;
	TRISEbits.TRISE6 = 1;

	// Initialize encoder inputs
	TRISGbits.TRISG6 = 0;
	TRISGbits.TRISG8 = 0;
	TRISEbits.TRISE1 = 0;
	TRISEbits.TRISE3 = 0;

	while(1)
	{
		UINT32_VAL port;
		port.Val = PORTE;

		// Update left encoder
		handle_encoder_input(&leftEncoder, port.bits.b0, port.bits.b2);
		_LATG8 = leftEncoder.pulse_out;
		_LATG6 = leftEncoder.direction;

		// Update right encoder
		handle_encoder_input(&rightEncoder, port.bits.b4, port.bits.b6);
		_LATE1 = rightEncoder.pulse_out;
		_LATE3 = rightEncoder.direction;
	}
}

static void handle_encoder_input(encoder_input *input, int channelA, int channelB)
{
	if(input->last_value != -1)
	{
		if(input->last_value != channelA)
		{
			// Edge. Update direction.

			// If both channels are the same, we are going one direction
			if(channelA == channelB)
			{
				if(input->direction != 0)
				{
					input->direction = 0;
					input->counter = 0;
				}
			}
			else
			{
				// Going other direction
				if(input->direction != 1)
				{
					input->direction = 1;
					input->counter = 0;
				}
			}
		}
		
		if(input->last_value == 0 && channelA == 1)
		{
			// Rising Edge. Update counter.
			input->counter++;
		}

		if(input->counter >= TARGET_COUNTER)
		{
			input->pulse_out = !input->pulse_out;
			input->counter = 0;
		}
	}

	// Save last value for next iteration
	input->last_value = channelA;
}