using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.Collections;
using System.IO.Ports;
using Stasis.Software.Netduino.Sensors;

namespace Stasis.Software.Netduino
{
	public class Program
	{
		
		public static void Main()
		{
			// Motors
			Motor motorA = new Motor(Pins.GPIO_PIN_D10, Pins.GPIO_PIN_D12, Pins.GPIO_PIN_D11, Pins.GPIO_PIN_D4, Pins.GPIO_PIN_D5);
			Motor motorB = new Motor(Pins.GPIO_PIN_D9, Pins.GPIO_PIN_D7, Pins.GPIO_PIN_D8, Pins.GPIO_PIN_D6, Pins.GPIO_PIN_D13);
            motorA.Reversed = true;
            motorB.Reversed = false;

			// Rangers
			InfraredDistanceSensor irFront = new InfraredDistanceSensor(Pins.GPIO_PIN_A4);
			InfraredDistanceSensor irBack = new InfraredDistanceSensor(Pins.GPIO_PIN_A5);

			// Robot and its controller
			StasisRobot bot = new StasisRobot(motorA, motorB, irFront, irBack);
			StasisController controller = new StasisController(bot);

			Timer thinkTimer = new Timer(new TimerCallback(delegate
			{
				controller.Think();
			}), null, 0, 15);

			// Keep alive
			Thread.Sleep(Timeout.Infinite);
		}
	}
}
