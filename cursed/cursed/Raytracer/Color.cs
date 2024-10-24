using cursed.Raytracer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace cursed.Raytracer
{
	internal class Color
	{
		public double r, g, b, a;

		public static Color Red
		{
			get
			{
				return new Color(1, 0, 0);
			}
		}

		public static Color Green
		{
			get
			{
				return new Color(0, 1, 0);
			}
		}

		public static Color Blue
		{
			get
			{
				return new Color(0, 0, 1);
			}
		}

		public static Color Black
		{
			get
			{
				return new Color(0, 0, 0);
			}
		}

		public static Color White
		{
			get
			{
				return new Color(1, 1, 1);
			}
		}

		public Color() : this(new Vector3(1, 1, 1)) { }

		public Color(Vector3 v) : this(new Vector4(v.x, v.y, v.z, 1)) { }

		public Color(Vector4 v) : this(v.x, v.y, v.z, v.w) { }

		public Color(double r, double g, double b, double a = 0)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		public static Color operator /(Color c, double d)
		{
			return new Color(c.r / d, c.g / d, c.b / d, c.a / d);
		}

		public static Color operator *(Color c, double d)
		{
			return new Color(c.r * d, c.g * d, c.b * d, c.a * d);
		}

		public static Color operator *(Color c, Color d)
		{
			return new Color(c.r * d.r, c.g * d.g, c.b * d.b, c.a * d.a);
		}

		public static Color operator +(Color c, Color d)
		{
			return new Color(c.r + d.r, c.g + d.g, c.b + d.b, c.a + d.a);
		}

		public static Color Clamp(Color c, double min, double max)
		{
			return new Color(Math.Min(Math.Max(c.r, min), max), 
				Math.Min(Math.Max(c.g, min), max), 
				Math.Min(Math.Max(c.b, min), max),
				Math.Min(Math.Max(c.a, min), max));
		}
	}
}
