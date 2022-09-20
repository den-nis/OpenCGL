using System;

namespace OpenCGL.Drawing
{
	public partial class Canvas
    {
        public void DrawRectanlge(Vec2i point1, Vec2i point2, Color color) => DrawRectanlge(point1.X, point1.Y, point2.X, point2.Y, color);
        public void DrawRectanlge(int x1, int y1, int x2, int y2, Color color)
        {
            DrawTriangleAdvanced(x1, y1, x1, y2, x2, y2, color, false);
            DrawTriangleAdvanced(x1, y1, x2, y1, x2, y2, color, false);    
        }

        public void DrawLine(float x1, float y1, float x2, float y2, float lineWidth, Color color) => DrawLine(new Vec2f(x1,y1), new Vec2f(x2, y2), lineWidth, color);
        public void DrawLine(Vec2f p1, Vec2f p2, float strokeWidth, Color color)
        {
            float dx = (p2.X - p1.X);
            float dy = (p2.Y - p1.Y);

            float angle = (float)Math.Atan2(dx, dy);
            float d90 = (float)Math.PI / 2;

            Vec2f r = new Vec2f((float)Math.Sin(angle + d90) * strokeWidth, (float)Math.Cos(angle + d90) * strokeWidth); 
            Vec2f l = new Vec2f((float)Math.Sin(angle - d90) * strokeWidth, (float)Math.Cos(angle - d90) * strokeWidth); 
            
            DrawTriangleAdvanced(p1 + r, p2 + l, p1 + l, color, false);
            DrawTriangleAdvanced(p2 + r, p2 + l, p1 + r, color, false);
        }

        public void DrawTriangle(float x1, float y1, float x2, float y2, float x3, float y3, Color color) => DrawTriangle(new Vec2f(x1,y1), new Vec2f(x2,y2), new Vec2f(x3,y3), color);
        public void DrawTriangle(Vec2f p1, Vec2f p2, Vec2f p3, Color color)
        {
            DrawTriangleAdvanced(p1, p2, p3, color, true);
        }

        private void DrawTriangleAdvanced(float x1, float y1, float x2, float y2, float x3, float y3, Color color, bool preventOverlap) => DrawTriangleAdvanced(new Vec2f(x1,y1), new Vec2f(x2,y2), new Vec2f(x3,y3), color, preventOverlap);
        private void DrawTriangleAdvanced(Vec2f p1, Vec2f p2, Vec2f p3, Color color, bool preventOverlap)
        {
            p1 = Context.Apply(p1);
            p2 = Context.Apply(p2);
            p3 = Context.Apply(p3);

            //Fix triangle winding 
            Vec2f l = p2 - p1, r = p3 - p1;
            if (l.X * r.Y - l.Y * r.X < 0) 
            { 
                var t = p1; p1 = p2; p2 = t; 
            }

            //Select drawing area
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
                            !preventOverlap 
                            || 
                            (
                                (w0 != 0 || (p3.Y < p2.Y || (p3.Y == p2.Y && p3.X < p2.X))) &&
                                (w1 != 0 || (p1.Y < p3.Y || (p1.Y == p3.Y && p1.X < p3.X))) &&
                                (w2 != 0 || (p2.Y < p1.Y || (p2.Y == p1.Y && p2.X < p1.X)))
                            )
                        )
                        {
                            DrawCharacter(x, y, color);
                        }
                    }
                }
            }
        }

        public void DrawCircle(int x, int y, float radius, Color color, int sections = 32) => DrawCircle(new Vec2f(x,y), radius, color, sections);
        public void DrawCircle(Vec2f point, float radius, Color color, int sections = 32)
        {
            for (int i = 0; i < sections; i++)
            {
                float twoPi = (float)Math.PI * 2f;
                float firstAngle = twoPi / sections * i; 
                float secondAngle = twoPi / sections * (i + 1);

                Vec2f p1 = new(point.X + (float)Math.Cos(firstAngle) * radius, point.Y + (float)Math.Sin(firstAngle) * radius);
                Vec2f p2 = new(point.X + (float)Math.Cos(secondAngle) * radius, point.Y + (float)Math.Sin(secondAngle) * radius);
                
                DrawTriangleAdvanced(point, p1, p2, color, false);
            }
        }

        private static float Edge(float px, float py, float x1, float y1, float x2, float y2)
        {
            return (px - x1) * (y2 - y1) - (py - y1) * (x2 - x1);
        }
    }
}