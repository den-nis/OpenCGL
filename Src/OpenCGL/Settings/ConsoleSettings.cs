using System;
using System.Runtime.InteropServices;
using static OpenCGL.ConsolePInvoke;

namespace OpenCGL.Settings
{
    public static class ConsoleSettings
    {
        public static void ApplyDefaultSettings()
        {
            SetConsoleFontFace("Terminal");
            SetConsoleFontSize(8, 8);
            DisableResize();
        }

        public static void SetSize(int width, int height)
		{
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }

        public static void DisableResize()
        {
            var window = GetConsoleWindow();
            var menu = GetSystemMenu(window, false);

            DeleteMenu(menu, SC_MINIMIZE, MF_BYCOMMAND);
            DeleteMenu(menu, SC_MAXIMIZE, MF_BYCOMMAND);
            DeleteMenu(menu, SC_SIZE, MF_BYCOMMAND);
        }

        public static void SetConsoleFontFace(string fontName)
        {
            var info = GetCurrentConsoleFontInfo();
            info.FaceName = fontName;
            if (!SetCurrentConsoleFontEx(ConsoleHandle, false, ref info))
                throw new Exception("Failed to set console font");
        }

        public static void SetConsoleFontSize(int x, int y)
        {
            var info = GetCurrentConsoleFontInfo();
            info.dwFontSize = new COORD((short)x, (short)y);
            if (!SetCurrentConsoleFontEx(ConsoleHandle, false, ref info))
                throw new Exception("Failed to set console font size");
        }

        private static CONSOLE_FONT_INFOEX GetCurrentConsoleFontInfo()
        {
            CONSOLE_FONT_INFOEX info = new CONSOLE_FONT_INFOEX();
            info.cbSize = (uint)Marshal.SizeOf(info);
            if (!GetCurrentConsoleFontEx(ConsoleHandle, false, ref info))
                throw new Exception("Failed to get current console font info");

            return info;
        }

        public static Vec2i GetMousePosition()
        {
            var info = GetCurrentConsoleFontInfo();
            var point = new POINT() { X = 0, Y = 0 };
            if (GetCursorPos(ref point) && ScreenToClient(GetConsoleWindow(), ref point))
                return new Vec2i((int)(point.X / (float)info.dwFontSize.X), (int)(point.Y / (float)info.dwFontSize.Y));
            return new Vec2i();
        }
    }
}
