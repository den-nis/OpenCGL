using System;
using System.Runtime.InteropServices;

namespace OpenCGL;

internal class WinSystemCalls
{
    public const int MF_BYCOMMAND = 0x00000000;
    public const int SC_MINIMIZE = 0xF020;
    public const int SC_MAXIMIZE = 0xF030;
    public const int SC_SIZE = 0xF000;

	public static IntPtr ConsoleHandle { get; set; } = GetStdHandle(StdHandle.OutputHandle);

	public enum StdHandle
    {
        OutputHandle = -11
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COORD
    {
        public short X;
        public short Y;

        public COORD(short x, short y)
        {
            X = x;
            Y = y;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SMALL_RECT
    {
        public short Left { get; set; }
        public short Top { get; set; }
        public short Right { get; set; }
        public short Bottom { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CONSOLE_FONT_INFOEX
    {
        public uint cbSize;
        public uint nFont;
        public COORD dwFontSize;
        public int FontFamily;
        public int FontWeight;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FaceName;
    }

    [DllImport("kernel32")]
    public static extern IntPtr GetStdHandle(StdHandle index);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public extern static bool GetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool bMaximumWindow, ref CONSOLE_FONT_INFOEX lpConsoleCurrentFontEx);

    [DllImport("Kernel32.dll", CharSet = CharSet.Unicode, BestFitMapping = false)]
    public static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool bMaximumWindow, ref CONSOLE_FONT_INFOEX lpConsoleCurrentFontEx);

    [DllImport("Kernel32.dll", CharSet = CharSet.Ansi, BestFitMapping = false)]
    public static extern bool WriteConsoleOutput(IntPtr hConsoleOutput, ConsoleChar[] buffer, COORD bufferSize, COORD bufferCoord, ref SMALL_RECT writeRegion);

    [DllImport("kernel32.dll", ExactSpelling = true)]
    public static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

    [DllImport("user32.dll")]
    public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(ref POINT lpPoint);

    [DllImport("user32.dll")]
    public static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool SetConsoleScreenBufferSize(IntPtr hConsoleOutput, COORD dwSize);

    [DllImport("User32.dll", SetLastError = true)]
    public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
}
