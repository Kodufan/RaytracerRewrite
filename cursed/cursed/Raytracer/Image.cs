using System;
using System.Collections.Generic;
using System.Text;

namespace cursed.Raytracer
{
	class Image
	{
		int W, H;
		Color pixels;
		
		public Image(string ppmFileName)
		{
			
		}
		
		public Color getPixelUV(double u, double v)
		{
			return new Color(0, 0, 0);
		}
	}
}
