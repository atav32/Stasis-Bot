using System;
using Microsoft.SPOT;

namespace Stasis.Software.Netduino.Utility
{
	public class Vector
	{
		/// <summary>
		/// Gets or sets the x-component of the vector
		/// </summary>
		public double X
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the y-component of the vector
		/// </summary>
		public double Y
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the z-component of the vector
		/// </summary>
		public double Z
		{
			get;
			set;
		}

		public Vector(double _x = double.PositiveInfinity, double _y = double.PositiveInfinity, double _z = double.PositiveInfinity)
		{
			this.X = _x;
			this.Y = _y;
			this.Z = _z;
		}

	}
}
