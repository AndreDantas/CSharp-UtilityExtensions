using CSharpUtilityExtensions.Extensions;
using System;

namespace CSharpUtilityExtensions.Classes.Geometry
{
    public struct Line
    {
        private Vector2 _start;
        public Vector2 start { get => _start; private set => _start = value; }

        private Vector2 _end;
        public Vector2 end { get => _end; private set => _end = value; }

        /// <summary>
        /// Returns a point that belong to the line, where t = 0 returns the start point and t = 1
        /// returns the end point
        /// </summary>
        /// <param name="by"> </param>
        /// <returns> </returns>
        public Vector2 GetPoint(Float01 by) => GeometryExtensions.Lerp(start, end, by);

        public Line(Vector2 start, Vector2 end) : this()
        {
            this.start = start;
            this.end = end;
        }

        public static Line[] FormLinesWithPoints(Vector2[] points)
        {
            if (points == null || points.Length < 2)
                return null;

            Line[] lines = new Line[points.Length - 1];

            for (int i = 0; i < points.Length - 1; i++)
            {
                lines[i] = new Line(points[i], points[i + 1]);
            }

            return lines;
        }
    }
}