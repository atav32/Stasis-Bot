using System;
using Microsoft.SPOT;

namespace Stasis.Software.Netduino
{
	public class PID
	{
		/// <summary>
		/// Gets or sets the proportional constant in the PID loop
		/// </summary>
		public double ProportionalConstant
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the integration constatnt for the PID loop
		/// </summary>
		public double IntegrationConstant
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the derivative constant for the PID loop
		/// </summary>
		public double DerivativeConstant
		{
			get;
			set;
		}

        public double ProportionalError
        {
            get;
            set;
        }

        public double IntegrationError
        {
            get;
            set;
        }

        public double DerivativeError
        {
            get;
            set;
        }


		/// <summary>
		/// Gets or sets the set point for the PID
		/// </summary>
		public double SetPoint
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the calculated manipulated value for this think iteration
		/// </summary>
		public double Output
		{
			get;
			private set;
		}

		/// <summary>
		/// Current error in the PID to the set point
		/// </summary>
        private double currentError
        {
            get;
            set;
        }

		/// <summary>
		/// Accumulative error
		/// </summary>
        public double accumulativeError
        {
            get;
            set;
        }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="setPoint"></param>
		public PID(double setPoint = 0, double proportionalConstant = 0, double integrationConstant = 0, double derivativeConstant = 0)
		{
			this.SetPoint = setPoint;
			this.ProportionalConstant = proportionalConstant;
			this.IntegrationConstant = integrationConstant;
			this.DerivativeConstant = derivativeConstant;

            this.currentError = 0;
		}

		/// <summary>
		/// Update the PID logic with a new value for the process variable.
		/// </summary>
		/// <param name="newProcessValue"></param>
		public double Update(double newProcessValue)
		{
			// Calculate new error
			double newError = newProcessValue - this.SetPoint;
			
			// Add to accumulated error
			this.accumulativeError += newError;

			// Calculate terms
			ProportionalError = this.ProportionalConstant * newError;
			IntegrationError = this.IntegrationConstant * this.accumulativeError;
			DerivativeError = this.DerivativeConstant * (newError - this.currentError);

            // Store current error
            this.currentError = newError;

			// Set output
			this.Output = ProportionalError + IntegrationError + DerivativeError;

			// Return PID output
			return this.Output;
		}
	}
}
