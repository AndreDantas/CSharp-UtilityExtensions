

using System;

namespace CSharpUtilityExtensions.Extensions
{
    public static class MathExtensions
    {
        public static double ReversePercentage(double finalValue, double percentAdded)
        {
            return (finalValue * 100) / (percentAdded + 100);
        }

        /// <summary>
        /// Converts an Angle to Radians.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static double Map(double OldMin, double OldMax, double NewMin, double NewMax, double OldValue)
        {

            double OldRange = (OldMax - OldMin);
            double NewRange = (NewMax - NewMin);
            double NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

            return (NewValue);
        }

        private delegate double RoundingFunction(double value);

        private enum RoundingDirection { Up, Down }

        private static double Round(double value, int precision,
                    RoundingDirection roundingDirection)
        {
            RoundingFunction roundingFunction;
            if (roundingDirection == RoundingDirection.Up)
                roundingFunction = Math.Ceiling;
            else
                roundingFunction = Math.Floor;
            value *= Math.Pow(10, precision);
            value = roundingFunction(value);
            return value * Math.Pow(10, -1 * precision);
        }
        public static double RoundUp(double value, int precision)
        {
            return Round(value, precision, RoundingDirection.Up);
        }

        public static double RoundDown(double value, int precision)
        {
            return Round(value, precision, RoundingDirection.Down);
        }

    }
}