using System;
using System.Runtime.InteropServices;

namespace OpenCGL;

[StructLayout(LayoutKind.Sequential)]
public struct Color : IEquatable<Color>
{
    public char Character { get; private set; }

    internal short Attributes { get; set; }

    public ConsoleColor BackgroundColor => (ConsoleColor)((Attributes >> 4) & 0x000F);
    public ConsoleColor ForegroundColor => (ConsoleColor)(Attributes & 0x000F);

    public Color(char character) : this(ConvertToAttribute(ConsoleColor.White, ConsoleColor.Black), character) { }

    public Color(ConsoleColor textColor, ConsoleColor backColor, char character) : this(ConvertToAttribute(textColor, backColor), character) { }

    public Color(ConsoleColor textColor, char character) : this(ConvertToAttribute(textColor, ConsoleColor.Black), character) { }

    internal Color(short attributes, char character)
    {
        Character = character;
        Attributes = attributes;
    }

    private static short ConvertToAttribute(ConsoleColor textColor, ConsoleColor backColor)
    {
        return (short)((int)backColor << 4 | (int)textColor);
    }

    public override bool Equals(object obj)
    {
        var o = (Color)obj;
        return o.Character == Character && o.Attributes == Attributes;
    }

    public override int GetHashCode() => (Character, Attributes).GetHashCode();

    public bool Equals(Color obj) => obj.Character == Character && obj.Attributes == Attributes;

    public static bool operator ==(Color a, Color b) => a.Equals(b);

    public static bool operator !=(Color a, Color b) => !(a == b);

    private const char blankCharacter = ' ';

    public static Color Black =>       new(default, ConsoleColor.Black,           blankCharacter);
    public static Color DarkBlue =>    new(default, ConsoleColor.DarkBlue,        blankCharacter);
    public static Color DarkGreen =>   new(default, ConsoleColor.DarkGreen,       blankCharacter);
    public static Color DarkCyan =>    new(default, ConsoleColor.DarkCyan,        blankCharacter);
    public static Color DarkRed =>     new(default, ConsoleColor.DarkRed,         blankCharacter);
    public static Color DarkMagenta => new(default, ConsoleColor.DarkMagenta,     blankCharacter);
    public static Color DarkYellow =>  new(default, ConsoleColor.DarkYellow,      blankCharacter);
    public static Color Gray =>        new(default, ConsoleColor.Gray,            blankCharacter);
    public static Color DarkGray =>    new(default, ConsoleColor.DarkGray,        blankCharacter);
    public static Color Blue =>        new(default, ConsoleColor.Blue,            blankCharacter);
    public static Color Green =>       new(default, ConsoleColor.Green,           blankCharacter);
    public static Color Cyan =>        new(default, ConsoleColor.Cyan,            blankCharacter);
    public static Color Red =>         new(default, ConsoleColor.Red,             blankCharacter);
    public static Color Magenta =>     new(default, ConsoleColor.Magenta,         blankCharacter);
    public static Color Yellow =>      new(default, ConsoleColor.Yellow,          blankCharacter);
    public static Color White =>       new(default, ConsoleColor.White,           blankCharacter);
}
