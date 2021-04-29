using System;
using System.Collections.Generic;
using UtilityExtensions.Extensions;

namespace UtilityExtensions.Core.Geometry
{
    public class Ellipse : Shape2D
    {
        public double Height;
        public double Width;
        public Angle Rotation;
        public Vector2 Center;

        public override double Area => Math.Abs((Width / 2f) * (Height / 2f) * Math.PI);
        public override Line[] Lines => null;

        public override Vector2[] Points => null;

        public Ellipse()
        {
        }

        public Ellipse(double height, double width) : this()
        {
            this.Height = height;
            this.Width = width;
        }

        public Ellipse(double height, double width, Angle rotation) : this()
        {
            this.Height = height;
            this.Width = width;
            this.Rotation = rotation;
        }

        public Ellipse(double height, double width, Angle rotation, Vector2 center) : this()
        {
            this.Height = height;
            this.Width = width;
            this.Rotation = rotation;
            this.Center = center;
        }

        public override int GetHashCode()
        {
            int hashCode = -2012222744;

            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Rotation.GetHashCode();
            hashCode = hashCode * -1521134295 + Center.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return obj is Ellipse ellipse &&
                   EqualityComparer<Vector2>.Default.Equals(Center, ellipse.Center) &&
                   EqualityComparer<Angle>.Default.Equals(Rotation, ellipse.Rotation) &&
                   Height.CloseTo(ellipse.Height) &&
                   Width.CloseTo(ellipse.Width);
        }

        private Vector2 getPointFromAngle(Angle angle)
        {
            double ePX = (Center.x + Width) + (int)(Width * Math.Cos(angle.Rad));
            double ePY = (Center.y + Height) + (int)(Height * -Math.Sin(angle.Rad));

            return Vector2.RotateVector(new Vector2(ePX, ePY), Center, Rotation);
        }
    }
}