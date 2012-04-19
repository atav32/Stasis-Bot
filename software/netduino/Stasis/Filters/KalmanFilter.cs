using System;
using Microsoft.SPOT;
using System.Collections;

namespace Stasis.Software.Netduino
{
    public class KalmanFilter
    {
        /// <summary>
        /// Gets the filtered value.
        /// </summary>
        public double Value
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the Estimation Error
        /// </summary>
        public double EstimationError
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Kalman Gain
        /// </summary>
        public double KalmanGain
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Process Noise
        /// </summary>
        public double ProcessNoise
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the MeasurementNoise
        /// </summary>
        public double MeasurementNoise
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="size">Window size</param>
        public KalmanFilter(double estimationError, double kalmanGain, double processNoise, double measurementNoise)
        {
            this.Value = 0;
            this.EstimationError = estimationError;
            this.KalmanGain = kalmanGain;
            this.ProcessNoise = processNoise;
            this.MeasurementNoise = measurementNoise;
        }

        /// <summary>
        /// Adds a value to the filter
        /// </summary>
        /// <param name="value">Newest value</param>
        public void AddValue(double previousValue, double newValue)
        {
            this.EstimationError += this.ProcessNoise;
            this.KalmanGain = this.EstimationError / (this.EstimationError + this.MeasurementNoise);
            this.EstimationError = (1 - this.KalmanGain) * this.EstimationError;

            this.Value = previousValue + this.KalmanGain * (newValue - previousValue);
        }       
    }
}
