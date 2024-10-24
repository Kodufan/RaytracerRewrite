using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using cursed.Raytracer;
using cursed.Raytracer.Core;

using Xamarin.Forms;
using cursed.Raytracer.Cameras;
using cursed.Raytracer.TransparentIShapes;
using cursed.Raytracer.Shapes;
using static Xamarin.Essentials.Permissions;

namespace cursed
{
	class MainPage : ContentPage
	{
		SKBitmap bitmap;
		SKCanvasView view;
		FrameBuffer frameBuffer;
		RayTracer rayTracer;
		IScene theScene;
		Label renderTime;
		Stepper numReflections;
		Label reflectionsLabel;
		Switch shadows;
		Label resolutionX = new Label();
		Entry resolutionXEntry = new Entry();
		Label resolutionY = new Label();
		Entry resolutionYEntry = new Entry();
		Vector3 cameraPos = new Vector3(0, 5, 10);
		Vector3 cameraFocus = new Vector3(0, 0, 0);
		Vector3 cameraUp = -Defs.Y_AXIS;
		double cameraFOV = Defs.PI_2;
		bool isSettingUp = true;

		public MainPage()
		{
			Config.lightPosition = new Vector3(1, 5, 1);
			Config.ResolutionX = 500;
			Config.ResolutionY = 250;
			
			theScene = new IScene();
			rayTracer = new RayTracer(new Raytracer.Color(.75, 1, .76), ref theScene);
			
			theScene.camera = new PerspectiveCamera(cameraPos, cameraFocus, cameraUp, cameraFOV, Config.ResolutionX, Config.ResolutionY);

			// Creates a new bitmap with the specified width and height
			bitmap = new SKBitmap(Config.ResolutionX, Config.ResolutionY);
			frameBuffer = new FrameBuffer(ref bitmap);
			
			// Creates a new canvas with the specified bitmap
			using (var canvas = new SKCanvas(bitmap))
			{
				// Clears the canvas with the specified color
				canvas.Clear(SKColors.White);
			}
			view = new SKCanvasView();
			view.PaintSurface += paintScene;

			renderTime = new Label();



			// Creates a stack layout to hold the name entry and the label
			var stackLayout = new StackLayout();
			stackLayout.Orientation = StackOrientation.Vertical;
			stackLayout.Spacing = 1;
			

			// Creates a stepper for the number of reflections
			numReflections = new Stepper();
			numReflections.Minimum = 0;
			numReflections.Maximum = 3;
			numReflections.Increment = 1;
			numReflections.ValueChanged += RedrawScene;
			
			// Creates a label for the number of reflections
			reflectionsLabel = new Label();

			


			
			// Creates a switch to toggle shadow visibility
			shadows = new Switch();
			shadows.Toggled += RedrawScene;
			Label label2 = new Label();
			label2.Text = "Shadows";

			// Creates a horizontal stack layout to hold the stepper and the label
			var reflectionStackLayout = new StackLayout();
			reflectionStackLayout.Orientation = StackOrientation.Horizontal;
			reflectionStackLayout.VerticalOptions = LayoutOptions.Center;
			reflectionStackLayout.Children.Add(reflectionsLabel);
			reflectionStackLayout.Children.Add(numReflections);

			// Creates a horizontal stack layout to hold the switch and the label
			var shadowStackLayout = new StackLayout();
			shadowStackLayout.Orientation = StackOrientation.Horizontal;
			shadowStackLayout.VerticalOptions = LayoutOptions.Center;
			shadowStackLayout.Spacing = 10;

			shadowStackLayout.Children.Add(label2);
			shadowStackLayout.Children.Add(shadows);

			// Creates a horizontal stack layout to hold the entry and the label
			var resolutionXStackLayout = new StackLayout();
			resolutionX = new Label();
			resolutionX.Text = String.Format("Width: {0} px", Config.ResolutionX);
			resolutionXStackLayout.Orientation = StackOrientation.Horizontal;
			resolutionXStackLayout.VerticalOptions = LayoutOptions.Center;
			resolutionXStackLayout.Spacing = 10;
			resolutionXEntry.WidthRequest = 100;

			resolutionXStackLayout.Children.Add(resolutionX);
			resolutionXStackLayout.Children.Add(resolutionXEntry);

			resolutionXEntry.Completed += RedrawScene;

			// Creates a horizontal stack layout to hold the entry and the label
			var resolutionYStackLayout = new StackLayout();
			resolutionY = new Label();
			resolutionY.Text = String.Format("Height: {0} px", Config.ResolutionY);
			resolutionYStackLayout.Orientation = StackOrientation.Horizontal;
			resolutionYStackLayout.VerticalOptions = LayoutOptions.Center;
			resolutionYStackLayout.Spacing = 10;
			resolutionYEntry.WidthRequest = 100;

			resolutionYStackLayout.Children.Add(resolutionY);
			resolutionYStackLayout.Children.Add(resolutionYEntry);

			resolutionYEntry.Completed += RedrawScene;


			// Centers the elemnts of the stack layout
			stackLayout.HorizontalOptions = LayoutOptions.Center;
			// Sets the margin to 10
			stackLayout.Margin = new Thickness(10, 10, 10, 10);
			stackLayout.Spacing = 10;
			stackLayout.Children.Add(view);
			stackLayout.Children.Add(renderTime);
			stackLayout.Children.Add(reflectionStackLayout);
			stackLayout.Children.Add(shadowStackLayout);
			stackLayout.Children.Add(resolutionXStackLayout);
			stackLayout.Children.Add(resolutionYStackLayout);

			setupResolution();

			Content = stackLayout;


			buildScene(ref theScene);

			isSettingUp = false;
		}
		
		void RedrawScene(object sender, EventArgs args)
		{
			if (!isSettingUp)
			{
				int resX = -1;
				int resY = -1;
				bool changed = false;
				bool hadError = false;
				if (resolutionXEntry.Text != null && !int.TryParse(resolutionXEntry.Text, out resX))
				{
					resolutionX.Text = "Error! Please enter an int for width";
					hadError = true;
				}
				else if (resolutionXEntry.Text != null)
				{
					Config.ResolutionX = resX;
					resolutionX.Text = String.Format("Width: {0} px", Config.ResolutionX);
					changed = true;
				} else
				{
					changed = true;
				}

				if (resolutionYEntry.Text != null && !int.TryParse(resolutionYEntry.Text, out resY))
				{
					resolutionY.Text = "Error! Please enter an int for height";
					hadError = true;
				}
				else if (resolutionYEntry.Text != null)
				{
					Config.ResolutionY = resY;
					resolutionY.Text = String.Format("Height: {0} px", Config.ResolutionY);
					changed = true;
				} else
				{
					changed = true;
				}

				if (changed && !hadError)
				{
					setupResolution();

					Config.hasShadows = shadows.IsToggled;
					Config.reflectionDepth = (int)numReflections.Value;
					view.InvalidateSurface();
				}
			}
		}

		void setupResolution()
		{
			theScene.camera = new PerspectiveCamera(cameraPos, cameraFocus, cameraUp, cameraFOV, Config.ResolutionX, Config.ResolutionY);
			bitmap = new SKBitmap(Config.ResolutionX, Config.ResolutionY);
			frameBuffer = new FrameBuffer(ref bitmap);
			view.WidthRequest = Config.ResolutionX;
			view.HeightRequest = Config.ResolutionY;
		}

		void paintScene(object sender, SKPaintSurfaceEventArgs args)
		{
			SKCanvas canvas = args.Surface.Canvas;
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

			// 2.5 seconds with synchronous ray tracing
			// No difference with asynchronous ray tracing
			sw.Start();
			rayTracer.raytraceScene(ref frameBuffer, Config.reflectionDepth, 1);
			sw.Stop();

			renderTime.Text = String.Format("Render time: {0} ms", sw.ElapsedMilliseconds);
			string reflectionString = String.Format("{0} reflection", Config.reflectionDepth);
			if (Config.reflectionDepth != 1)
			{
				reflectionString += "s";
			}
			
			reflectionsLabel.Text = reflectionString;
			

			frameBuffer.ShowColorBuffer();
			canvas.DrawBitmap(bitmap, 0, 0);
		}
		
		void buildScene(ref IScene scene)
		{
			IShape plane = new IPlane(new Vector3(0, -2, 0), new Vector3(0, 1, 0));
			IShape sphere1 = new ISphere(new Vector3(5, 5, 0), 2);
			IShape sphere2 = new ISphere(new Vector3(-3, 0, 2), 2);
			IShape sphere3 = new ISphere(new Vector3(3, 0, 2), 2);
			//IShape ellipsoid = new IEllipsoid(new Vector3(4.0, 0.0, 3.0), new Vector3(2.0, 1.0, 2.0));
			IShape YCylinder = new ICylinderY(new Vector3(-9, 0, 3), 3, 2);
			IShape disk = new IDisk(new Vector3(15, 0, 0), new Vector3(0, 0, 1), 5);
			// Creates a new transparent plane
			IShape transparentPlane = new IPlane(new Vector3(-10.0, 10.0, 0.0), new Vector3(1.0, 0.0, 0.0));

			scene.addOpaqueObject(new VisibleIShape(ref plane, Material.Tin));
			scene.addOpaqueObject(new VisibleIShape(ref sphere1, Material.Chrome));
			scene.addOpaqueObject(new VisibleIShape(ref sphere2, Material.Brass));
			scene.addOpaqueObject(new VisibleIShape(ref sphere3, Material.Gold));
			//scene.addOpaqueObject(new VisibleIShape(ref ellipsoid, redPlastic));
			scene.addOpaqueObject(new VisibleIShape(ref YCylinder, Material.Tin));
			scene.addOpaqueObject(new VisibleIShape(ref disk, Material.CyanPlastic));
			scene.addTransparentObject(new TransparentIShape(ref transparentPlane, new Raytracer.Color(1, 0, 0), 0.5));
			
			scene.addLight(new PositionalLight(Config.lightPosition, Raytracer.Color.White));
		}
	}
}
