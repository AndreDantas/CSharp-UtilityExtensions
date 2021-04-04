using System;
using CSharpUtilityExtensions.Geometry.Classes;

namespace CSharpUtilityExtensions.Extensions
{
    public static class GeometryExtensions
    {

        /// <summary>
        /// Returns the distance between two points.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static double Distance(Vector2 start, Vector2 end)
        {
            double deltaX = (end.x - start.x) * (end.x - start.x);
            double deltaY = (end.y - start.y) * (end.y - start.y);

            return Math.Sqrt(deltaX + deltaY);
        }

        public static double Distance(this Line l)
        {
            return Distance(l.start, l.end);
        }
    }
}