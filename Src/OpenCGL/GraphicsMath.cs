using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCGL
{
	class GraphicsMath
	{
		public static float Edge(Vec2f point, Vec2f vec1, Vec2f vec2)
		{
			return (point.X - vec1.X) * (vec2.Y - vec1.Y) - (point.Y - vec1.Y) * (vec2.X - vec1.X); 
		}

		public static float Edge(float px, float py, float x1, float y1, float x2, float y2)
		{
			return (px - x1) * (y2 - y1) - (py - y1) * (x2 - x1);
		}
	}
}
