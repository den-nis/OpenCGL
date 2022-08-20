﻿using System;
using System.Runtime.InteropServices;

namespace OpenCGL;

[StructLayout(LayoutKind.Sequential)]
public struct ConsoleChar : IEquatable<ConsoleChar>
{
    public char Character { get; private set; }

    internal short Attributes { get; set; }

    public ConsoleColor BackgroundColor => (ConsoleColor)((Attributes >> 4) & 0x000F);
    public ConsoleColor ForegroundColor => (ConsoleColor)(Attributes & 0x000F);

    public ConsoleChar(char character) : this(ConvertToAttribute(ConsoleColor.White, ConsoleColor.Black), character) { }

    public ConsoleChar(ConsoleColor textColor, ConsoleColor backColor, char character) : this(ConvertToAttribute(textColor, backColor), character) { }

    public ConsoleChar(ConsoleColor textColor, char character) : this(ConvertToAttribute(textColor, ConsoleColor.Black), character) { }

    internal ConsoleChar(short attributes, char character)
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
        var o = (ConsoleChar)obj;
        return o.Character == Character && o.Attributes == Attributes;
    }

    public override int GetHashCode() => (Character, Attributes).GetHashCode();

    public bool Equals(ConsoleChar obj) => obj.Character == Character && obj.Attributes == Attributes;

    public static bool operator ==(ConsoleChar a, ConsoleChar b) => a.Equals(b);

    public static bool operator !=(ConsoleChar a, ConsoleChar b) => !(a == b);

    private const char blankCharacter = ' ';

    public static ConsoleChar Black =>       new(default, ConsoleColor.Black,           blankCharacter);
    public static ConsoleChar DarkBlue =>    new(default, ConsoleColor.DarkBlue,        blankCharacter);
    public static ConsoleChar DarkGreen =>   new(default, ConsoleColor.DarkGreen,       blankCharacter);
    public static ConsoleChar DarkCyan =>    new(default, ConsoleColor.DarkCyan,        blankCharacter);
    public static ConsoleChar DarkRed =>     new(default, ConsoleColor.DarkRed,         blankCharacter);
    public static ConsoleChar DarkMagenta => new(default, ConsoleColor.DarkMagenta,     blankCharacter);
    public static ConsoleChar DarkYellow =>  new(default, ConsoleColor.DarkYellow,      blankCharacter);
    public static ConsoleChar Gray =>        new(default, ConsoleColor.Gray,            blankCharacter);
    public static ConsoleChar DarkGray =>    new(default, ConsoleColor.DarkGray,        blankCharacter);
    public static ConsoleChar Blue =>        new(default, ConsoleColor.Blue,            blankCharacter);
    public static ConsoleChar Green =>       new(default, ConsoleColor.Green,           blankCharacter);
    public static ConsoleChar Cyan =>        new(default, ConsoleColor.Cyan,            blankCharacter);
    public static ConsoleChar Red =>         new(default, ConsoleColor.Red,             blankCharacter);
    public static ConsoleChar Magenta =>     new(default, ConsoleColor.Magenta,         blankCharacter);
    public static ConsoleChar Yellow =>      new(default, ConsoleColor.Yellow,          blankCharacter);
    public static ConsoleChar White =>       new(default, ConsoleColor.White,           blankCharacter);
}
