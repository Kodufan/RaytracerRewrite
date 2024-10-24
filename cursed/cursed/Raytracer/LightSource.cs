using System;
using System.Collections.Generic;
using System.ComponentModel;
using cursed.Raytracer.Core;
using System.Text;

namespace cursed.Raytracer
{
    abstract class LightSource
	{
		public bool isOn;          //!< True if the light is active; otherwise, has no effect.
		public Color lightColor;   //!< The color of this light
		
		public LightSource() : this(new Color(1, 1, 1))
		{
			isOn = true;
		}
		public LightSource(Color C)
		{
			lightColor = C;
		}
		
		public abstract Color Illuminate(Vector3 interceptWorldCoords, Vector3 normal, ref Material material, ref Frame eyeFrame, bool inShadow);
		public abstract Ray getShadowFeeler(Vector3 interceptWorldCoords, Vector3 normal, ref Frame eyeFrame);
		public abstract bool pointIsInAShadow(Vector3 intercept, Vector3 normal, List<VisibleIShape> objects, ref Frame eyeFrame);
		
		public Color ambientColor(Color matAmbient, Color lightColor) 
		{
			return Color.Clamp(matAmbient * lightColor, 0.0, 1.0);
		}

		public Color diffuseColor(Color matDiffuse, Color lightColor, Vector3 l, Vector3 n) 
		{
			return Color.Clamp((matDiffuse * lightColor) * (Vector3.Dot(l, n)), 0.0, 1.0);
		}

		public Color specularColor(Color matSpecular, Color lightColor, double shininess, Vector3 r, Vector3 v) 
		{
			double dot = Vector3.Dot(r, v);
			if (dot< 0) {
				dot = 0;
			}
			double exp = Math.Pow(dot, shininess);
			return Color.Clamp((matSpecular * lightColor) * exp, 0.0, 1.0);
		}
		
		public Color totalColor(ref Material mat, Color lightColor, Vector3 v, Vector3 n, Vector3 lightPos, 
			Vector3 intersectionPt, bool attenuationOn, LightATParams ATparams) 
		{
			Vector3 l = Vector3.Normalize(lightPos - intersectionPt);
			Vector3 r = Vector3.Normalize((2 * Vector3.Dot(l, n)) * n - l);
			Color ambient = ambientColor(mat.ambient, lightColor);
			Color diffuse = diffuseColor(mat.diffuse, lightColor, l, n);
			Color specular = specularColor(mat.specular, lightColor, mat.shininess, r, v);
			double attenuationFactor = 1;
			if (attenuationOn) {
				attenuationFactor = ATparams.factor(Vector3.Distance(lightPos, intersectionPt));
			}
			
			Color total = Color.Clamp(ambient + (diffuse * attenuationFactor) + (specular * attenuationFactor), 0.0, 1.0);
			return total;
		}
	}
}
