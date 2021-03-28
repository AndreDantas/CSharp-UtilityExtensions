using System;

namespace CSharpUtilityExtensions.Geometry.Classes
{
    public class Triangle : Shape2D
    {
        private Vector2 _v3;
        private Vector2 _v1;
        private Vector2 _v2;

        public Vector2 v1 { get => _v1; set => _v1 = value; }
        public Vector2 v2 { get => _v2; set => _v2 = value; }
        public Vector2 v3 { get => _v3; set => _v3 = value; }

        public override double Area => Math.Abs((v1.x * (v2.y - v3.y) + v2.x * (v3.y - v1.y) + v3.x * (v1.y - v2.y)) / 2f);

        public override Line[] Lines => new Line[3]
        {
            new Line(v1, v2),
            new Line(v2, v3),
            new Line(v3, v1)
        };

        public Triangle()
        {
        }

        public Triangle(Vector2 v1) : this()
        {
            this.v1 = v1;
        }

        public Triangle(Vector2 v1, Vector2 v2) : this()
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public Triangle(Vector2 v1, Vector2 v2, Vector2 v3) : this()
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }

        public override int GetHashCode()
        {
            int hashCode = 600343409;

            hashCode = hashCode * -1521134295 + v1.GetHashCode();
            hashCode = hashCode * -1521134295 + v2.GetHashCode();
            hashCode = hashCode * -1521134295 + v3.GetHashCode();

            return hashCode;
        }
    }
}