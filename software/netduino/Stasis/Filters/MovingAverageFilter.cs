using System;
using Microsoft.SPOT;
using System.Collections;

namespace Stasis.Software.Netduino
{
	/// <summary>
	/// Averaging filter. Add values. Get moving average out
	/// </summary>
	public class MovingAverageFilter
	{

		/// <summary>
		/// Gets the window size for the moving average
		/// </summary>
		public int Size
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the moving average value.
		/// </summary>
		public double Value
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the number of values currently in the filter
		/// </summary>
		public int Count
		{
			get;
			private set;
		}

		/// <summary>
		/// Values array
		/// </summary>
		private double[] values;

		/// <summary>
		/// Index of next open spot (where next value added will go)
		/// </summary>
		private int valueBufferIndex = 0;

		/// <summary>
		/// Current running sum of all values in the filter
		/// </summary>
		private double sum = 0;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="size">Window size</param>
		public MovingAverageFilter(int size)
		{
			this.Size = size;
			this.values = new double[size];
		}

		/// <summary>
		/// Adds a value to the filter
		/// </summary>
		/// <param name="value"></param>
		public void AddValue(double value)
		{
			if (this.Count == this.Size)
			{
				// We are full, take out value at the next spot we are about to replace
				sum -= this.values[this.valueBufferIndex];

				// Took out out
				this.Count--;
			}

			this.values[this.valueBufferIndex] = value;
			this.sum += value;
			this.Count++;
			this.valueBufferIndex = (this.valueBufferIndex + 1) % this.Size;

			this.Value = this.sum / (double)this.Count;
		}
	}
}
