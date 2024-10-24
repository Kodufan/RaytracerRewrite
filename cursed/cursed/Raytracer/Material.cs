using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace cursed.Raytracer
{
	internal class Material
	{
		public Color ambient;      //!< ambient material property
		public Color diffuse;      //!< diffuse material property
		public Color specular;     //!< specular material property
		public double shininess;   //!< shininess material property
		
		public static Material Brass
		{
			get
			{
				return new Material(new List<double> { 0.329412, 0.223529, 0.027451, 0.780392, 0.568627, 0.113725, 0.992157, 0.941176, 0.807843, 27.8974 });
			}
		}

		public static Material Bronze
		{
			get
			{
				return new Material(new List<double> { 0.2125, 0.1275, 0.054, 0.714, 0.4284, 0.18144, 0.393548, 0.271906, 0.166721, 25.6 });
			}
		}

		public static Material Chrome
		{
			get
			{
				return new Material(new List<double> { 0.25, 0.25, 0.25, 0.4, 0.4, 0.4, 0.774597, 0.774597, 0.774597, 76.8 });
			}
		}

		public static Material Tin
		{
			get
			{
				return new Material(new List<double> { 0.105882, 0.058824, 0.113725, 0.427451, 0.470588, 0.541176, 0.9, 0.9, 0.9, 12.8 });
			}
		}

		public static Material CyanPlastic
		{
			get
			{
				return new Material(new List<double> { 0.0, 0.1, 0.06, 0.0, 0.50980392, 0.50980392, 0.50196078, 0.50196078, 0.50196078, 32.0 });
			}
		}

		public static Material Gold
		{
			get
			{
				return new Material(new List<double> { 0.24725, 0.1995, 0.0745, 0.75164, 0.60648, 0.22648, 0.628281, 0.555802, 0.366065, 51.2 });
			}
		}

		public static Material Silver
		{
			get
			{
				return new Material(new List<double> { 0.19225, 0.19225, 0.19225, 0.50754, 0.50754, 0.50754, 0.508273, 0.508273, 0.508273, 51.2 });
			}
		}

		public static Material RedPlastic
		{
			get
			{
				return new Material(new List<double> { 0.0, 0.0, 0.0, 0.5, 0.0, 0.0, 0.7, 0.6, 0.6, 32.0 });
			}
		}

		public Material() : this(Color.Black, Color.Black, Color.Black, 0.0) { }
		public Material(Color amb, Color diff, Color spec, double shininess) 
		{
			ambient = amb;
			diffuse = diff;
			specular = spec;
			this.shininess = shininess;
		}
		public Material(List<double> d) : this(new Color(d[0], d[1], d[2]), 
			new Color(d[3], d[4], d[5]), 
			new Color(d[6], d[7], d[8]), d[9]) { }
	}
}
