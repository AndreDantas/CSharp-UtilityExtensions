using System;

namespace UtilityExtensions.Core.Geometry
{
    public class Rectangle : Shape2D
    {
        public double Height;
        public double Width;
        public Angle Rotation;
        public Vector2 Center;
        public override double Area => Math.Abs(Width * Height);

        public override Line[] Lines => new Line[4]
        {
            new Line(TopLeft, TopRight),
            new Line(TopRight, BottomRight),
            new Line(BottomRight, BottomLeft),
            new Line(BottomLeft, TopLeft)
        };

        public override Vector2[] Points => new Vector2[4] {
            TopLeft, TopRight, BottomRight, BottomLeft
        };

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