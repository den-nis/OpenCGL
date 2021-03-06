﻿using System;
using System.Linq;

namespace OpenCGL.Drawing
{
    public partial class Canvas
    {
        private void DrawPoint(int x, int y, int size, ConsoleChar fill)
        {
            if (size == 1)
            {
                DrawCharacter(x, y, fill);
                return;
            }

            int halfSize = size / 2;
            for (int iy = -halfSize; iy <= halfSize; iy++)
            {
                for (int ix = -halfSize; ix <= halfSize; ix++)
                {
                    DrawCharacter(x + ix, y + iy, fill);
                }
            }
        }

        public void FillRectanlge(Vec2i point1, Vec2i point2, ConsoleChar fill) => FillRectanlge(point1.X, point1.Y, point2.X, point2.Y, fill);
        public void FillRectanlge(int x1, int y1, int x2, int y2, ConsoleChar fill)
        {
            var xe = Math.Max(x1, x2);
            var ye = Math.Max(y1, y2);

            for (int y = Math.Min(y1, y2); y <= ye; ++y)
                for (int x = Math.Min(x1, x2); x <= xe; ++x)
                    DrawCharacter(x, y, fill);
        }

        public void DrawRectangle(Vec2i point1, Vec2i point2, int strokeWidth, ConsoleChar stroke) => DrawRectangle(point1.X, point1.Y, point2.X, point2.Y, strokeWidth, stroke);
        public void DrawRectangle(int x1, int y1, int x2, int y2, int strokeWidth, ConsoleChar stroke)
        {
            var xs = Math.Min(x1, x2);
            var ys = Math.Min(y1, y2);
            var xe = Math.Max(x1, x2);
            var ye = Math.Max(y1, y2);

            for (int y = ys; y <= ye; ++y)
            {
                DrawPoint(xs, y, strokeWidth, stroke);
                DrawPoint(xe, y, strokeWidth, stroke);
            }

            for (int x = xs; x <= xe; ++x)
            {
                DrawPoint(x, ys, strokeWidth, stroke);
                DrawPoint(x, ye, strokeWidth, stroke);
            }
        }

        public void DrawLine(Vec2i point1, Vec2i point2, int strokeWidth, ConsoleChar stroke) => DrawLine(point1.X, point1.Y, point2.X, point2.Y, strokeWidth, stroke);
        public void DrawLine(int x1, int y1, int x2, int y2, int strokeWidth, ConsoleChar stroke)
        {
            int dx = Math.Abs(x2 - x1); //Delta x
            int dy = Math.Abs(y2 - y1); //Delta y

            var xd = Math.Sign(x2 - x1); //X direction
            var yd = Math.Sign(y2 - y1); //Y direction

            if (dx > dy)
            {
                var slope = dy / (float)dx;

                float y = y1;
                for (int x = x1; x != x2; x += xd)
                {
                    DrawPoint(x, (int)Math.Round(y), strokeWidth, stroke);
                    y += slope * yd;
                }

                DrawPoint(x2, (int)Math.Round(y), strokeWidth, stroke);
            }
            else
            {
                var slope = dx / (float)dy;

                float x = x1;
                for (int y = y1; y != y2; y += yd)
                {
                    DrawPoint((int)Math.Round(x), y, strokeWidth, stroke);
                    x += slope * xd;
                }

                DrawPoint((int)Math.Round(x), y2, strokeWidth, stroke);
            }
        }

        public void DrawTriangle(Vec2i point1, Vec2i point2, Vec2i point3, int strokeWidth, ConsoleChar stroke) => DrawTriangle(point1.X, point1.Y, point2.X, point2.Y, point3.X, point3.Y, strokeWidth, stroke);
        public void DrawTriangle(int x1, int y1, int x2, int y2, int x3, int y3, int strokeWidth, ConsoleChar stroke)
        {
            DrawLine(x1, y1, x2, y2, strokeWidth, stroke);
            DrawLine(x2, y2, x3, y3, strokeWidth, stroke);
            DrawLine(x3, y3, x1, y1, strokeWidth, stroke);
        }

        public void FillTriangle(Vec2i point1, Vec2i point2, Vec2i point3, ConsoleChar fill) => FillTriangle(point1.X, point1.Y, point2.X, point2.Y, point3.X, point3.Y, fill);
        public void FillTriangle(int x1, int y1, int x2, int y2, int x3, int y3, ConsoleChar fill)
        {
            var left =   Math.Min(x1, Math.Min(x2, x3));
            var top =    Math.Min(y1, Math.Min(y2, y3));
            var right =  Math.Max(x1, Math.Max(x2, x3));
            var bottom = Math.Max(y1, Math.Max(y2, y3));

            bool orientation = (y2 - y1) * (x3 - x2) - (y3 - y2) * (x2 - x1) < 0;

            for (int y = top; y <= bottom; y++)
			{
                for (int x = left; x <= right; x++)
				{
                    bool inside = true;

                    inside &= (GraphicsMath.Edge(x, y, x1, y1, x2, y2) > 0) ^ orientation;
                    inside &= (GraphicsMath.Edge(x, y, x2, y2, x3, y3) > 0) ^ orientation;
                    inside &= (GraphicsMath.Edge(x, y, x3, y3, x1, y1) > 0) ^ orientation;

                    if (inside)
                        DrawCharacter(x, y, fill);
				}
			}
        }

        public void FillCircle(Vec2i point, float radius, ConsoleChar fill) => FillCircle(point.X, point.Y, radius, fill);
        public void FillCircle(int x, int y, float radius, ConsoleChar fill)
        {
            for (int iy = 0; iy < (int)radius + 1; iy++)
            {
                for (int ix = 0; ix < (int)radius + 1; ix++)
                {
                    var distance = Math.Sqrt(ix * ix + iy * iy);

                    if (distance < radius + .5f)
                    {
                        DrawCharacter(x + ix, y + iy, fill);
                        DrawCharacter(x - ix, y + iy, fill);
                        DrawCharacter(x + ix, y - iy, fill);
                        DrawCharacter(x - ix, y - iy, fill);
                    }
                }
            }
        }

        public void DrawCircle(Vec2i point, float radius, int strokeWidth, ConsoleChar stroke) => DrawCircle(point.X, point.Y, radius, strokeWidth, stroke);
        public void DrawCircle(int x, int y, float radius, int strokeWidth, ConsoleChar stroke)
        {
            for (int iy = 0; iy < (int)radius + 1; iy++)
            {
                for (int ix = 0; ix < (int)radius + 1; ix++)
                {
                    var distance = Math.Sqrt(ix * ix + iy * iy);

                    if (distance < radius + .5f && distance >= radius - 1)
                    {
                        DrawPoint(x + ix, y + iy, strokeWidth, stroke);
                        DrawPoint(x - ix, y + iy, strokeWidth, stroke);
                        DrawPoint(x + ix, y - iy, strokeWidth, stroke);
                        DrawPoint(x - ix, y - iy, strokeWidth, stroke);
                    }
                }
            }
        }
    }
}