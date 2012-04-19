using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.Collections;
using System.IO.Ports;

namespace Stasis.Software.Netduino
{
	public class Program
	{
		
		public static void Main()
		{
	
			// Motors
			Motor motorA = new Motor(Pins.GPIO_PIN_D5, Pins.GPIO_PIN_D7, Pins.GPIO_PIN_D8, Pins.GPIO_PIN_A0);
			Motor motorB = new Motor(Pins.GPIO_PIN_D6, Pins.GPIO_PIN_D9, Pins.GPIO_PIN_D10, Pins.GPIO_PIN_A1);
			motorB.Reversed = true;

			// Rangers
			InfraredDistanceSensor irFront = new InfraredDistanceSensor(Pins.GPIO_PIN_A3);
			InfraredDistanceSensor irBack = new InfraredDistanceSensor(Pins.GPIO_PIN_A2);

			// Robot and its controller
			StasisRobot bot = new StasisRobot(motorA, motorB, irFront, irBack);
			StasisController controller = new StasisController(bot);

			// Calibrate controller
			controller.Calibrate();

			// Think Loop
			while (true)
			{
				controller.Think();
			}
		}
	}
}
