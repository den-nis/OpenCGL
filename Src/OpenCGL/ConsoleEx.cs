using System;
using System.Runtime.InteropServices;

namespace OpenCGL;

public static class ConsoleEx
{
	public static void SetSize(int width, int height)
	{
		if (Console.BufferWidth != width || 
		    Console.BufferHeight != height || 
		    Console.WindowWidth != width || 
		    Console.WindowHeight != height)
		{
			Console.SetWindowSize(1, 1);
			Console.SetBufferSize(width, height);
			Console.SetWindowSize(width, height);
		}
	}

	public static void DisableResizing()
	{
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			var window = WindowsSystemCalls.GetConsoleWindow();
			var menu = WindowsSystemCalls.GetSystemMenu(window, false);

			WindowsSystemCalls.DeleteMenu(menu, WindowsSystemCalls.SC_MINIMIZE, WindowsSystemCalls.MF_BYCOMMAND);
			WindowsSystemCalls.DeleteMenu(menu, WindowsSystemCalls.SC_MAXIMIZE, WindowsSystemCalls.MF_BYCOMMAND);
			WindowsSystemCalls.DeleteMenu(menu, WindowsSystemCalls.SC_SIZE, WindowsSystemCalls.MF_BYCOMMAND);
		}
		else
		{
			throw NotSupportedException(nameof(DisableResizing));
		}
	}

	public static Vec2f GetMousePosition()
	{
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			var info = GetCurrentConsoleFontInfo();
			var point = new WindowsSystemCalls.POINT() { X = 0, Y = 0 };
			if (WindowsSystemCalls.GetCursorPos(ref point) && WindowsSystemCalls.ScreenToClient(WindowsSystemCalls.GetConsoleWindow(), ref point))
			{
				return new Vec2f(point.X / (float)info.dwFontSize.X, point.Y / (float)info.dwFontSize.Y);
			}
			return new Vec2f();
		}
		else
		{
			throw NotSupportedException(nameof(GetMousePosition));
		}
	}

	public static void SetConsoleFontFaceName(string faceName)
	{
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			var info = GetCurrentConsoleFontInfo();
			info.FaceName = faceName;
			if (!WindowsSystemCalls.SetCurrentConsoleFontEx(WindowsSystemCalls.ConsoleHandle, false, ref info))
			{
				throw new Exception("Failed to set console font");
			}
		}
		else
		{
			throw NotSupportedException(nameof(SetConsoleFontFaceName));
		}
	}

	public static void SetConsoleFontSize(int x, int y)
	{
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			var info = GetCurrentConsoleFontInfo();
			info.dwFontSize = new WindowsSystemCalls.COORD((short)x, (short)y);
			if (!WindowsSystemCalls.SetCurrentConsoleFontEx(WindowsSystemCalls.ConsoleHandle, false, ref info))
			{
				throw new Exception("Failed to set console font size");
			}
		}
		else
		{
			throw NotSupportedException(nameof(SetConsoleFontSize));
		}
	}

	private static WindowsSystemCalls.CONSOLE_FONT_INFOEX GetCurrentConsoleFontInfo()
	{
		WindowsSystemCalls.CONSOLE_FONT_INFOEX info = new();
		info.cbSize = (uint)Marshal.SizeOf(info);
		if (!WindowsSystemCalls.GetCurrentConsoleFontEx(WindowsSystemCalls.ConsoleHandle, false, ref info))
		{
			throw new Exception("Failed to get current console font info");
		}

		return info;
	}

	private static Exception NotSupportedException(string methodName)
	{
		return new NotImplementedException($"{methodName} is not supported on the current OS");
	}
}
