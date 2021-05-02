using System;

namespace UtilityExtensions.Extensions
{
    public static class MathExtensions
    {
        public static int SignZero(this double d)
        {
            if (d.CloseTo(0f))
                return 0;
            else
                return d > 0 ? 1 : -1;
        }

        public static double Clamp(this double value, double minValue, double maxValue)
        {
            return value.ClampMin(minValue).ClampMax(maxValue);
        }

        public static double ClampMax(this double value, double maxValue)
        {
            if (value > maxValue)
                value = maxValue;
            return value;
        }

        public static double ClampMin(this double value, double minValue)
        {
            if (value < minValue)
                value = minValue;
            return value;
        }

        public static int Clamp(this int value, int minValue, int maxValue)
        {
            return value.ClampMin(minValue).ClampMax(maxValue);
        }

        public static int ClampMax(this int value, int maxValue)
        {
            if (value > maxValue)
                value = maxValue;
            return value;
        }

        public static int ClampMin(this int value, int minValue)
        {
            if (value < minValue)
                value = minValue;
            return value;
        }

        public static double RevertPercentage(this double value, double percentageAdded)
        {
            return (value * 100) / (percentageAdded + 100);
        }

        /// <summary>
        /// Converts an Angle to Radians.
        /// </summary>
        /// <param name="angle"> </param>
        /// <returns> </returns>
        public static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static double Map(this double value, double OldMin, double OldMax, double NewMin, double NewMax)
        {
            double OldRange = (OldMax - OldMin);
            double NewRange = (NewMax - NewMin);
            double NewValue = (((value - OldMin) * NewRange) / OldRange) + NewMin;

            return (NewValue);
        }

        private delegate double RoundingFunction(double value);

        private enum RoundingDirection { Up, Down }

        private static double Round(double value, int precision, RoundingDirection roundingDirection)
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

        public static double RoundUp(this double value, int precision)
        {
            return Round(value, precision, RoundingDirection.Up);
        }

        public static double RoundDown(this double value, int precision)
        {
            return Round(value, precision, RoundingDirection.Down);
        }

        public static double Abs(this double d)
        {
            return d < 0 ? d * -1 : d;
        }

        public static bool CloseTo(this double d, double other, double threshold = 0.000001f)
        {
            return ((d - other).Abs() < threshold.Abs());
        }

        public static double ChangeTo(this double d, double target, double deltaTime, double speed = 30f)
        {
            if (d.CloseTo(target))
                return target;
            double changeValue = speed.Abs().ClampMin(target) * deltaTime;
            if (d > target)
            {
                if (d - changeValue < target)
                    return target;
                return d - changeValue;
            }
            else
            {
                if (d + changeValue > target)
                    return target;
                return d + changeValue;
            }
        }

        public static double Lerp(this double value, double to, double by)
        {
            return value * (1 - by) + to * by;
        }
    }
}