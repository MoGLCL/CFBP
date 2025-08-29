using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using OpenCvSharp;
using OpenCvSharp.Extensions;

class Program
{
	// ================== Win32 Mouse ==================
	[DllImport("user32.dll")]
	static extern bool SetCursorPos(int X, int Y);

	[DllImport("user32.dll")]
	static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

	private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
	private const uint MOUSEEVENTF_LEFTUP = 0x04;

	static void Click(int x, int y)
	{
		SetCursorPos(x, y);
		Thread.Sleep(50);
		mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)x, (uint)y, 0, 0);
		mouse_event(MOUSEEVENTF_LEFTUP, (uint)x, (uint)y, 0, 0);
	}

	// ================== Screen Capture ==================
	static Mat CaptureScreen()
	{
		Rectangle bounds = ScreenHelper.GetScreenBounds();
		using var bmp = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format24bppRgb);
		using (var g = Graphics.FromImage(bmp))
		{
			g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
		}
		return BitmapConverter.ToMat(bmp);
	}

	// ================== Template Matching ==================
	static System.Drawing.Point? FindImageOnScreen(string imagePath, double threshold = 0.5)
	{
		if (!File.Exists(imagePath))
		{
			Console.WriteLine($"[WARN] Image not found: {imagePath}");
			return null;
		}

		using var screen = CaptureScreen();
		using var template = new Mat(imagePath, ImreadModes.Color);
		Console.WriteLine($"[INFO] Loaded template: {imagePath} ({template.Width}x{template.Height})");

		using var result = new Mat();
		Cv2.MatchTemplate(screen, template, result, TemplateMatchModes.CCoeffNormed);
		Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

		Console.WriteLine($"[DEBUG] Max match for {imagePath}: {maxVal:F3}");

		if (maxVal >= threshold)
		{
			var point = new System.Drawing.Point(maxLoc.X + template.Width / 2, maxLoc.Y + template.Height / 2);
			Console.WriteLine($"[FOUND] {imagePath} at ({point.X}, {point.Y})");
			return point;
		}
		else
		{
			Console.WriteLine($"[NOT FOUND] {imagePath} on screen.");
			return null;
		}
	}

	// ================== Main ==================
	static void Main()
	{
		string[] checkImages = { "checkbox.png", "checkbox2.png" };
		string[] trueImages = { "TrueL.png", "TrueD.png" };

		int maxAttempts = 3;
		bool success = false;

		Console.WriteLine("[*] AutoClicker started. Waiting for checkbox...");

		System.Drawing.Point? point = null;

		// ================== انتظار ظهور أي checkbox ==================
		while (point == null)
		{
			foreach (var checkImg in checkImages)
			{
				point = FindImageOnScreen(checkImg, threshold: 0.5);
				if (point != null)
				{
					Console.WriteLine($"[INFO] Found checkbox: {checkImg} at ({point.Value.X}, {point.Value.Y})");
					break;
				}
			}

			if (point == null)
			{
				Thread.Sleep(500); // انتظر قبل إعادة البحث
			}
		}

		// ================== الضغط على checkbox ==================
		for (int attempt = 0; attempt < maxAttempts; attempt++)
		{
			Click(point.Value.X, point.Value.Y);
			Console.WriteLine($"[INFO] Clicked checkbox at ({point.Value.X}, {point.Value.Y}), attempt {attempt + 1}");
			Thread.Sleep(5000); // انتظر بعد الضغط

			// ================== تحقق من True images مرة واحدة ==================
			foreach (var trueImg in trueImages)
			{
				var truePoint = FindImageOnScreen(trueImg, threshold: 0.5);
				if (truePoint != null)
				{
					Console.WriteLine($"[SUCCESS] Found True image {trueImg} at ({truePoint.Value.X}, {truePoint.Value.Y})!");
					success = true;
					break;
				}
			}

			if (success) break;
		}

		Console.WriteLine(success ? "True" : "False");
		Console.WriteLine("[*] AutoClicker finished.");
	}
}

// ================== Screen Helper ==================
static class ScreenHelper
{
	[DllImport("user32.dll")]
	private static extern IntPtr GetDesktopWindow();

	[DllImport("user32.dll")]
	private static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

	[StructLayout(LayoutKind.Sequential)]
	private struct RECT
	{
		public int Left, Top, Right, Bottom;
	}

	public static Rectangle GetScreenBounds()
	{
		IntPtr hDesktop = GetDesktopWindow();
		GetWindowRect(hDesktop, out RECT rect);
		return new Rectangle(0, 0, rect.Right - rect.Left, rect.Bottom - rect.Top);
	}
}
