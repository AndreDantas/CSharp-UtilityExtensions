using System;

namespace UtilityExtensions.Core.Geometry
{
    public class Triangle : Shape2D
    {
        public Vector2 v1;
        public Vector2 v2;
        public Vector2 v3;

        public override double Area => Math.Abs((v1.x * (v2.y - v3.y) + v2.x * (v3.y - v1.y) + v3.x * (v1.y - v2.y)) / 2f);

        public override Line[] Lines => new Line[3]
        {
            new Line(v1, v2),
            new Line(v2, v3),
            new Line(v3, v1)
        };

        public override Vector2[] Points => new Vector2[3] { v1, v2, v3 };

        private Triangle()
        {
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