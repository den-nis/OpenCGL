using System;

namespace OpenCGL.Drawing
{
	public partial class Canvas
    {
        private void DrawSquare(int x, int y, int size, Color fill)
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

        public void FillRectanlge(Vec2i point1, Vec2i point2, Color fill) => FillRectanlge(point1.X, point1.Y, point2.X, point2.Y, fill);
        public void FillRectanlge(int x1, int y1, int x2, int y2, Color fill)
        {
            var xe = Math.Max(x1, x2);
            var ye = Math.Max(y1, y2);

            for (int y = Math.Min(y1, y2); y <= ye; ++y)
                for (int x = Math.Min(x1, x2); x <= xe; ++x)
                    DrawCharacter(x, y, fill);
        }

        public void DrawRectangle(Vec2i point1, Vec2i point2, int strokeWidth, Color stroke) => DrawRectangle(point1.X, point1.Y, point2.X, point2.Y, strokeWidth, stroke);
        public void DrawRectangle(int x1, int y1, int x2, int y2, int strokeWidth, Color stroke)
        {
            var xs = Math.Min(x1, x2);
            var ys = Math.Min(y1, y2);
            var xe = Math.Max(x1, x2);
            var ye = Math.Max(y1, y2);

            for (int y = ys; y <= ye; ++y)
            {
                DrawSquare(xs, y, strokeWidth, stroke);
                DrawSquare(xe, y, strokeWidth, stroke);
            }

            for (int x = xs; x <= xe; ++x)
            {
                DrawSquare(x, ys, strokeWidth, stroke);
                DrawSquare(x, ye, strokeWidth, stroke);
            }
        }

        public void DrawLine(float x1, float y1, float x2, float y2, float strokeWidth, Color fill) => DrawLine(new Vec2f(x1,y1), new Vec2f(x2, y2), strokeWidth, fill);
        public void DrawLine(Vec2f p1, Vec2f p2, float strokeWidth, Color fill)
        {
            float dx = (p2.X - p1.X); //Delta x
            float dy = (p2.Y - p1.Y); //Delta y

            float angle = (float)Math.Atan2(dx, dy);
            float d90 = (float)Math.PI / 2;

            Vec2f r = new Vec2f((float)Math.Sin(angle + d90) * strokeWidth, (float)Math.Cos(angle + d90) * strokeWidth); 
            Vec2f l = new Vec2f((float)Math.Sin(angle - d90) * strokeWidth, (float)Math.Cos(angle - d90) * strokeWidth); 
            
            FillTriangle(p1 + r, p2 + l, p1 + l, fill);
            FillTriangle(p2 + r, p2 + l, p1 + r, fill);
        }

        public void DrawTriangle(Vec2f p1, Vec2f p2, Vec2f p3, int strokeWidth, Color stroke) => DrawTriangle(p1.X, p1.Y, p2.X, p2.Y, p3.X, p3.Y, strokeWidth, stroke);
        public void DrawTriangle(float x1, float y1, float x2, float y2, float x3, float y3, float strokeWidth, Color stroke)
        {
            DrawLine(x1, y1, x2, y2, strokeWidth, stroke);
            DrawLine(x2, y2, x3, y3, strokeWidth, stroke);
            DrawLine(x3, y3, x1, y1, strokeWidth, stroke);
        }

        public void FillTriangle(float x1, float y1, float x2, float y2, float x3, float y3, Color fill) => FillTriangle(new Vec2f(x1,y1), new Vec2f(x2,y2), new Vec2f(x3,y3), fill);
        public void FillTriangle(Vec2f p1, Vec2f p2, Vec2f p3, Color fill)
        {
            p1 = Context.Apply(p1);
            p2 = Context.Apply(p2);
            p3 = Context.Apply(p3);

            var left =   Math.Min((int)p1.X, Math.Min((int)p2.X, (int)p3.X)) - 1;
            var top =    Math.Min((int)p1.Y, Math.Min((int)p2.Y, (int)p3.Y)) - 1;
            var right =  Math.Max((int)p1.X, Math.Max((int)p2.X, (int)p3.X)) + 1;
            var bottom = Math.Max((int)p1.Y, Math.Max((int)p2.Y, (int)p3.Y)) + 1;

            for (int y = top; y < bottom; ++y) 
            {
                for (int x = left; x < right; ++x) 
                {
                    float w0 = Edge(x + .5f, y + .5f, p3.X, p3.Y, p2.X, p2.Y); 
                    float w1 = Edge(x + .5f, y + .5f, p1.X, p1.Y, p3.X, p3.Y); 
                    float w2 = Edge(x + .5f, y + .5f, p2.X, p2.Y, p1.X, p1.Y); 

                    if (w0 >= 0 && w1 >= 0 && w2 >= 0) 
                    {
                        if (
                         (w0 != 0 || (p3.Y < p2.Y || (p3.Y == p2.Y && p3.X < p2.X))) &&
                         (w1 != 0 || (p1.Y < p3.Y || (p1.Y == p3.Y && p1.X < p3.X))) &&
                         (w2 != 0 || (p2.Y < p1.Y || (p2.Y == p1.Y && p2.X < p1.X)))
                        )
                        {
                            DrawCharacter(x, y, fill);
                        }
                    }
                }
            }
        }

        public void FillCircle(Vec2i point, float radius, Color fill) => FillCircle(point.X, point.Y, radius, fill);
        public void FillCircle(int x, int y, float radius, Color fill)
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

        public void DrawCircle(Vec2i point, float radius, int strokeWidth, Color stroke) => DrawCircle(point.X, point.Y, radius, strokeWidth, stroke);
        public void DrawCircle(int x, int y, float radius, int strokeWidth, Color stroke)
        {
            for (int iy = 0; iy < (int)radius + 1; iy++)
            {
                for (int ix = 0; ix < (int)radius + 1; ix++)
                {
                    var distance = Math.Sqrt(ix * ix + iy * iy);

                    if (distance < radius + .5f && distance >= radius - 1)
                    {
                        DrawSquare(x + ix, y + iy, strokeWidth, stroke);
                        DrawSquare(x - ix, y + iy, strokeWidth, stroke);
                        DrawSquare(x + ix, y - iy, strokeWidth, stroke);
                        DrawSquare(x - ix, y - iy, strokeWidth, stroke);
                    }
                }
            }
        }

        private static float Edge(float px, float py, float x1, float y1, float x2, float y2)
        {
            return (px - x1) * (y2 - y1) - (py - y1) * (x2 - x1);
        }
    }
}