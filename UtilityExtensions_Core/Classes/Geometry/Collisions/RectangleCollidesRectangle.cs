using CSharpUtilityExtensions.Interfaces;
using CSharpUtilityExtensions.Extensions;
using System;

namespace CSharpUtilityExtensions.Classes.Geometry.Collision
{
    public class RectangleCollidesRectangle : IShapesCollision<Rectangle, Rectangle>
    {
        public bool CheckCollision(Rectangle rect1, Rectangle rect2) => GeometryExtensions.IsShapes2DIntersecting(rect1, rect2);
    }
}