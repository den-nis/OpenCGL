using System;

namespace OpenCGL.Drawing;

public partial class Canvas
{ 
    private const int SpacesInTab = 4;

    public void DrawText(Vec2i p, string text) => DrawText(p.X, p.Y, text);
    public void DrawText(int x, int y, string text) => DrawText(x, y, text, ConsoleColor.White);

    public void DrawText(Vec2i p, string text, ConsoleColor foreColor) => DrawText(p.X, p.Y, text, foreColor);
    public void DrawText(int x, int y, string text, ConsoleColor foreColor) => DrawText(x, y, text, foreColor, ConsoleColor.Black);

    public void DrawText(Vec2i p, string text, ConsoleColor foreColor, ConsoleColor backColor) => DrawText(p.X, p.Y, text, foreColor, backColor);
    public void DrawText(int x, int y, string text, ConsoleColor foreColor, ConsoleColor backColor)
    {
        int sx = x;
        int sy = y;

        int MaxWidth = 0;
        int MaxHeight = 0;

        for (int i = 0; i < text.Length; i++)
        {
            MaxWidth = Math.Max(MaxWidth, sx);
            MaxHeight = Math.Max(MaxHeight, sy);

            if (char.IsWhiteSpace(text[i]) || char.IsControl(text[i]))
            {
                switch (text[i])
                {
                    case '\n':
                        sx = x;
                        sy++;
                        continue;

                    case '\t':
                        var tab = new string(' ', SpacesInTab);
                        DrawText(sx, sy, tab, foreColor, backColor);
                        sx += tab.Length;
                        continue;

                    case ' ':
                        DrawCharacter(sx, sy, new ConsoleChar(foreColor, backColor, ' '));
                        sx++;
                        continue;

                    default:
                        continue;
                }
            }

            DrawCharacter(sx, sy, new ConsoleChar(foreColor, backColor, text[i]));
            sx++;
        }
    }
}