using System;
using UtilityExtensions.Extensions;
namespace UtilityExtensions.Classes
{
    public struct Angle
    {
        private double value;

        public Angle(Angle other) : this()
        {
            Value = other.value;
        }

        public Angle(double value) : this()
        {
            Value = value;
        }

        public double Value
        {
            get => ClampAngle(value); set => this.value = ClampAngle(value);
        }
        public double Rad
        {
            get => value * (Math.PI / 180);
        }


        public bool InsideRange(Angle start, Angle end)
        {
            if (start.Value < end.Value)
                return start.Value <= Value && Value <= end.Value;
            return start.Value <= Value || Value <= end.Value;

        }

        public static implicit operator Angle(double d)
        {
            return new Angle(d);
        }

        public static implicit operator double(Angle d)
        {
            return d.value;
        }
        public override string ToString()
        {
            return Value + "°";
        }

        public static double ClampAngle(double angle)
        {
            if (angle > 360)
            {

                while (angle > 360)
                    angle -= 360;

            }
            if (angle < 0)
            {

                while (angle < 0)
                    angle += 360;

            }

            return angle;
        }
    }
}