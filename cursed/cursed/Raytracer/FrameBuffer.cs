using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace cursed.Raytracer
{
	internal class FrameBuffer
	{
		
		public readonly int WIDTH;
		public readonly int HEIGHT;
		SKBitmap bitmap;
		public Color[,] pixels;

		public FrameBuffer(ref SKBitmap bitmap)
		{
			WIDTH = bitmap.Width;
			HEIGHT = bitmap.Height;
			this.bitmap = bitmap;
			pixels = new Color[WIDTH, HEIGHT];
			for(int i = 0; i < WIDTH; i++)
			{
				for(int j = 0; j < HEIGHT; j++)
				{
					pixels[i, j] = new Color(.1, .1, .1);
				}
			}
		}
		
		public void SetColor(int x, int y, Color color)
		{
			pixels[x, y] = color;
		}

		public void ShowColorBuffer()
		{
			for(int i = 0; i < WIDTH; i++)
			{
				for(int j = 0; j < HEIGHT; j++)
				{
					Color color = pixels[i, j];
					byte red = (byte)Utilities.map(color.r, 0, 1, 0, 255);
					byte green = (byte)Utilities.map(color.g, 0, 1, 0, 255);
					byte blue = (byte)Utilities.map(color.b, 0, 1, 0, 255);

					bitmap.SetPixel(i, j, new SKColor(red, green, blue));
				}
			}
		}
	}
}
