using System;

namespace UtilityExtensions.Classes
{
    public class Vector2
    {
        double _x;

        public double x { get => _x; private set => _x = value; }
        double _y;

        public double y { get => _y; private set => _y = value; }

        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2() : this(0, 0)
        {

        }

        public static Vector2 zero => new Vector2();
        public static Vector2 up => new Vector2(0, 1);
        public static Vector2 down => new Vector2(0, -1);
        public static Vector2 left => new Vector2(-1, 0);
        public static Vector2 right => new Vector2(1, 0);

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }


        public override string ToString()
        {
            return string.Format("({0},{1})", x, y);
        }

        public override int GetHashCode()
        {
            int hashCode = -1359342342;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }

        // <summary>
        /// Rotates one Vector2 around another
        /// </summary>
        /// <param name="pointToRotate">The point to rotate.</param>
        /// <param name="centerPoint">The center point of rotation.</param>
        /// <param name="angleInDegrees">The rotation angle in degrees.</param>
        /// <returns>Rotated point</returns>
        public static Vector2 RotateVector(Vector2 pointToRotate, Vector2 centerPoint, double angleInDegrees)
        {

            double angleInRadians = Math.Abs(angleInDegrees) * (Math.PI / 180);

            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);

            double originX = pointToRotate.x - centerPoint.x;
            double originY = pointToRotate.y - centerPoint.y;

            double newX = 0, newY = 0;
            if (angleInDegrees >= 0)
            {
                newX = originX * cosTheta - originY * sinTheta;
                newY = originX * sinTheta + originY * cosTheta;
            }
            else
            {
                newX = originX * cosTheta + originY * sinTheta;
                newY = -originX * sinTheta + originY * cosTheta;
            }


            return new Vector2(newX + centerPoint.x, newY + centerPoint.y);
        }
    }
}