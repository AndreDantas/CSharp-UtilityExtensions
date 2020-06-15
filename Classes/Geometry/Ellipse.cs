using System;
using System.Collections.Generic;

namespace CSharpUtilityExtensions.Geometry.Classes
{

    public class Ellipse : Shape2D
    {

        private Vector2 _center;
        private double _width;
        private double _height;
        private Angle _rotation;

        public double height { get => _height; set => _height = value; }
        public double width { get => _width; set => _width = value; }
        public Angle rotation { get => _rotation; set => _rotation = value; }
        public Vector2 center { get => _center; set => _center = value; }

        public override double Area => Math.Abs((_width / 2f) * (_height / 2f) * Math.PI);

        public Ellipse()
        {

        }
        public Ellipse(double height, double width) : this()
        {
            this.height = height;
            this.width = width;
        }

        public Ellipse(double height, double width, Angle rotation) : this()
        {
            this.height = height;
            this.width = width;
            this.rotation = rotation;
        }

        public Ellipse(double height, double width, Angle rotation, Vector2 center) : this()
        {
            this.height = height;
            this.width = width;
            this.rotation = rotation;
            this.center = center;
        }


        public override int GetHashCode()
        {
            int hashCode = -2012222744;

            hashCode = hashCode * -1521134295 + height.GetHashCode();
            hashCode = hashCode * -1521134295 + width.GetHashCode();
            hashCode = hashCode * -1521134295 + rotation.GetHashCode();
            hashCode = hashCode * -1521134295 + center.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return obj is Ellipse ellipse &&
                   Area == ellipse.Area &&
                   EqualityComparer<Vector2>.Default.Equals(_center, ellipse._center) &&
                   EqualityComparer<Angle>.Default.Equals(_rotation, ellipse._rotation) &&
                   height == ellipse.height &&
                   width == ellipse.width;
        }
    }
}