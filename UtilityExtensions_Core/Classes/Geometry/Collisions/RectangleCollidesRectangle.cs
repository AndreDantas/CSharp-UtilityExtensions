using CSharpUtilityExtensions.Interfaces;
using CSharpUtilityExtensions.Extensions;
using System;

namespace CSharpUtilityExtensions.Classes.Geometry.Collision
{
    public class RectangleCollidesRectangle : IShapesCollision<Rectangle, Rectangle>
    {
        public bool CheckCollision(Rectangle rec1, Rectangle rec2)
        {
            if (GeometryExtensions.Distance(rec1.Center, rec2.Center) > (Math.Max(rec1.Width, rec1.Height) / 2d + Math.Max(rec2.Width, rec2.Height) / 2d))
                return false;
            return true;
        }
    }
}