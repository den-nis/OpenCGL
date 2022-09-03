using System;
using System.Runtime.InteropServices;

namespace OpenCGL;

public static class ConsoleFont
{
	/// <summary>
	/// Will only work on Windows
	/// </summary>
	public static void WinSetConsoleFontFaceName(string faceName)
	{
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			var info = GetCurrentConsoleFontInfo();
			info.FaceName = faceName;
			if (!WinSystemCalls.SetCurrentConsoleFontEx(WinSystemCalls.ConsoleHandle, false, ref info))
			{
				throw new Exception("Failed to set console font");
			}
		}
		else
		{
			throw NotSupportedException(nameof(WinSetConsoleFontFaceName));
		}
	}

	/// <summary>
	/// Will only work on Windows
	/// </summary>
	public static void WinSetConsoleFontSize(int x, int y)
	{
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			var info = GetCurrentConsoleFontInfo();
			info.dwFontSize = new WinSystemCalls.COORD((short)x, (short)y);
			if (!WinSystemCalls.SetCurrentConsoleFontEx(WinSystemCalls.ConsoleHandle, false, ref info))
			{
				throw new Exception("Failed to set console font size");
			}
		}
		else
		{
			throw NotSupportedException(nameof(WinSetConsoleFontSize));
		}
	}

	private static WinSystemCalls.CONSOLE_FONT_INFOEX GetCurrentConsoleFontInfo()
	{
		WinSystemCalls.CONSOLE_FONT_INFOEX info = new();
		info.cbSize = (uint)Marshal.SizeOf(info);
		if (!WinSystemCalls.GetCurrentConsoleFontEx(WinSystemCalls.ConsoleHandle, false, ref info))
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
