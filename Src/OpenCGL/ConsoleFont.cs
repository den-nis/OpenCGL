using System;
using System.Runtime.InteropServices;

namespace OpenCGL;

public static class ConsoleFont
{
	/// <summary>
	/// Will only work on Windows
	/// </summary>
	public static void WindowsSetConsoleFontFaceName(string faceName)
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
			throw NotSupportedException(nameof(WindowsSetConsoleFontFaceName));
		}
	}

	/// <summary>
	/// Will only work on Windows
	/// </summary>
	public static void WindowsSetConsoleFontSize(int x, int y)
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
			throw NotSupportedException(nameof(WindowsSetConsoleFontSize));
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
