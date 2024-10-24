using System;
using System.Collections.Generic;
using cursed.Raytracer.Core;
using System.Text;

namespace cursed.Raytracer.Shapes
{
	internal abstract class ICylinder : IQuadricSurface
	{
		public double radius, height;

		protected ICylinder(QuadricParameters parameters, Vector3 position) : base(parameters, position) {}
	}
}
