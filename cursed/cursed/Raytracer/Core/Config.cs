using System;
using System.Collections.Generic;
using System.Text;

namespace cursed.Raytracer.Core
{
	internal class Config
	{
		public static bool hasShadows
		{
			get;
			set;
		}

		public static int reflectionDepth
		{
			get;
			set;
		}

		public static Vector3 lightPosition
		{
			get;
			set;
		}

		public static int ResolutionX
		{
			get;
			set;
		}

		public static int ResolutionY
		{
			get;
			set;
		}
	}
}
