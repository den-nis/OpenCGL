using System;
using System.Collections.Generic;

namespace OpenCGL.Drawing;

public partial class Canvas
{
    public void Composite(int x, int y, Canvas other)
    {
        var maxWidth = Math.Min(other.Width + x, Width);
        var maxHeight = Math.Min(other.Height + y, Height);

        for (int iy = y; iy < maxHeight; ++iy)
        {
            for (int ix = x; ix < maxWidth; ++ix)
            {
                DrawCharacter(ix, iy, other[ix - x, iy - y]);
            }
        }
    }

    public void Composite(int x, int y, Canvas other, ConsoleChar filter)
    {
        var maxWidth = Math.Min(other.Width + x, Width);
        var maxHeight = Math.Min(other.Height + y, Height);

        for (int iy = y; iy < maxHeight; ++iy)
        {
            for (int ix = x; ix < maxWidth; ++ix)
            {
                if (other[ix - x, iy - y] != filter)
                    DrawCharacter(ix, iy, other[ix - x, iy - y]);
            }
        }
    }

    public void Composite(int x, int y, Canvas other, HashSet<ConsoleChar> filters)
    {
        var maxWidth = Math.Min(other.Width + x, Width);
        var maxHeight = Math.Min(other.Height + y, Height);

        for (int iy = y; iy < maxHeight; iy++)
        {
            for (int ix = x; ix < maxWidth; ix++)
            {
                if (!filters.Contains(other[ix - x, iy - y]))
                    DrawCharacter(ix, iy, other[ix - x, iy - y]);
            }
        }
    }
}
