using System;
using System.Collections.Generic;
using System.Text;

namespace cursed.Raytracer.Core
{
	internal class Matrix3
	{
		public double[,] m;

		public Vector3 Column0
		{
			get
			{
				return new Vector3(m[0, 0], m[0, 1], m[0, 2]);
			}

			set
			{
				m[0, 0] = value.x;
				m[0, 1] = value.y;
				m[0, 2] = value.z;
			}
		}

		public Vector3 Column1
		{
			get
			{
				return new Vector3(m[1, 0], m[1, 1], m[1, 2]);
			}

			set
			{
				m[1, 0] = value.x;
				m[1, 1] = value.y;
				m[1, 2] = value.z;
			}
		}

		public Vector3 Column2
		{
			get
			{
				return new Vector3(m[2, 0], m[2, 1], m[2, 2]);
			}

			set
			{
				m[2, 0] = value.x;
				m[2, 1] = value.y;
				m[2, 2] = value.z;
			}
		}

		public Vector3 Row0
		{
			get
			{
				return new Vector3(m[0, 0], m[1, 0], m[2, 0]);
			}

			set
			{
				m[0, 0] = value.x;
				m[1, 0] = value.y;
				m[2, 0] = value.z;
			}
		}

		public Vector3 Row1
		{
			get
			{
				return new Vector3(m[0, 1], m[1, 1], m[2, 1]);
			}

			set
			{
				m[0, 1] = value.x;
				m[1, 1] = value.y;
				m[2, 1] = value.z;
			}
		}

		public Vector3 Row2
		{
			get
			{
				return new Vector3(m[0, 2], m[1, 2], m[2, 2]);
			}
			set
			{
				m[0, 2] = value.x;
				m[1, 2] = value.y;
				m[2, 2] = value.z;
			}
		}

		public double Determinant
		{
			get
			{
				return m[0, 0] * (m[1, 1] * m[2, 2] - m[2, 3] * m[3, 2]) -
				m[0, 1] * (m[1, 0] * m[2, 2] - m[2, 1] * m[3, 2]) +
					m[0, 2] * (m[1, 0] * m[2, 1] - m[2, 0] * m[3, 1]);
			}
		}
		
		public Matrix3()
		{
		}

		public Matrix3(double[,] m)
		{
			this.m = m;
		}

		public Matrix3(double xx, double xy, double xz,
			double yx, double yy, double yz,
			double zx, double zy, double zz)
		{
			m = new double[3, 3];
			m[0, 0] = xx;
			m[0, 1] = xy;
			m[0, 2] = xz;
			m[1, 0] = yx;
			m[1, 1] = yy;
			m[1, 2] = yz;
			m[2, 0] = zx;
			m[2, 1] = zy;
			m[2, 2] = zz;
		}

		public static Matrix3 Inverse(Matrix3 m)
		{
			double[,] inv = new double[3, 3];
			inv[0, 0] = m.m[1, 1] * m.m[2, 2] - m.m[1, 2] * m.m[2, 1];
			inv[0, 1] = m.m[0, 2] * m.m[2, 1] - m.m[0, 1] * m.m[2, 2];
			inv[0, 2] = m.m[0, 1] * m.m[1, 2] - m.m[0, 2] * m.m[1, 1];
			inv[1, 0] = m.m[1, 2] * m.m[2, 0] - m.m[1, 0] * m.m[2, 2];
			inv[1, 1] = m.m[0, 0] * m.m[2, 2] - m.m[0, 2] * m.m[2, 0];
			inv[1, 2] = m.m[0, 2] * m.m[1, 0] - m.m[0, 0] * m.m[1, 2];
			inv[2, 0] = m.m[1, 0] * m.m[2, 1] - m.m[1, 1] * m.m[2, 0];
			inv[2, 1] = m.m[0, 1] * m.m[2, 0] - m.m[0, 0] * m.m[2, 1];
			inv[2, 2] = m.m[0, 0] * m.m[1, 1] - m.m[0, 1] * m.m[1, 0];
			double det = m.m[0, 0] * inv[0, 0] + m.m[0, 1] * inv[1, 0] + m.m[0, 2] * inv[2, 0];
			return new Matrix3(inv[0, 0] / det, inv[0, 1] / det, inv[0, 2] / det,
				inv[1, 0] / det, inv[1, 1] / det, inv[1, 2] / det,
				inv[2, 0] / det, inv[2, 1] / det, inv[2, 2] / det);
		}

		public static Vector3 operator *(Matrix3 a, Vector3 v)
		{
			return new Vector3(
				a.m[0, 0] * v.x + a.m[0, 1] * v.y + a.m[0, 2] * v.z,
				a.m[1, 0] * v.x + a.m[1, 1] * v.y + a.m[1, 2] * v.z,
				a.m[2, 0] * v.x + a.m[2, 1] * v.y + a.m[2, 2] * v.z);
		}
	}
}
