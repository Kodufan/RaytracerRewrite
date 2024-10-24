using System;
using System.Collections.Generic;
using System.Text;

namespace cursed.Raytracer
{
	class LightATParams
	{
		double constant, linear, quadratic; //!< Parameters controlling attenuation.
		
		public LightATParams(double C, double L, double Q)
		{
			constant = C;
			linear = L;
			quadratic = Q;
		}

		public double factor(double distance) 
		{
			return 1.0 / (constant + linear* distance + quadratic* distance * distance);
		}
	}
}
