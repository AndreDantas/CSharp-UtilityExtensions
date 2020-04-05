
using System;

namespace CSharpUtilityExtensions.Geometry.Classes
{
    public class Rectangle : Shape2D
    {

        private double width;
        private double height;
        private Angle rotation;
        private Vector2 center;

        public override double Area => Math.Abs(width * height);
        public double Height { get => height; set => height = value; }
        public double Width { get => width; set => width = value; }
        public Angle Rotation { get => rotation; set => rotation = value; }
        public Vector2 Center { get => center; set => center = value; }

        private Vector2 topLeftRaw { get => new Vector2(Center.x - Width / 2f, Center.y + Height / 2f); }
        public Vector2 TopLeft { get => Vector2.RotateVector(topLeftRaw, Center, Rotation); }

        private Vector2 topRightRaw { get => new Vector2(Center.x + Width / 2f, Center.y + Height / 2f); }
        public Vector2 TopRight { get => Vector2.RotateVector(topRightRaw, Center, Rotation); }

        private Vector2 bottomLeftRaw { get => new Vector2(Center.x - Width / 2f, Center.y - Height / 2f); }
        public Vector2 BottomLeft { get => Vector2.RotateVector(bottomLeftRaw, Center, Rotation); }

        private Vector2 bottomRightRaw { get => new Vector2(Center.x + Width / 2f, Center.y - Height / 2f); }
        public Vector2 BottomRight { get => Vector2.RotateVector(bottomRightRaw, Center, Rotation); }

        public Rectangle()
        {
            
        }

        public Rectangle(double height, double width, Angle rotation, Vector2 center) : this()
        {
            Height = height;
            Width = width;
            Rotation = rotation;
            Center = center;
        }

        public Rectangle(double height, double width, Angle rotation) : this(height, width)
        {
            Rotation = rotation;
        }

        public Rectangle(double height, double width) : this()
        {
            Height = height;
            Width = width;
        }


    }
}