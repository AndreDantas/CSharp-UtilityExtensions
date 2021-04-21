using System;
using UtilityExtensions.Extensions;

namespace UtilityExtensions.Core
{
    public struct Range
    {
        private double _min;
        private double _max;
        private double _value;

        public double Min { get => _min; set => _min = Math.Min(value, Max); }
        public double Max { get => _max; set => _max = Math.Max(value, Min); }
        public double Value { get => _value; set => _value = MathExtensions.Clamp(value, Min, Max); }

        public Range(double value, double min, double max)
        {
            _min = _max = _value = 0;
            Value = value;
            Min = min;
            Max = max;
        }

        public override string ToString()
        {
            return Min + " --{" + Value + "}-- " + Max;
        }

        public override bool Equals(object obj)
        {
            return obj is Range range &&
                   Min.CloseTo(range.Min) &&
                   Max.CloseTo(range.Max) &&
                   Value.CloseTo(range.Value);
        }

        public override int GetHashCode()
        {
            int hashCode = -944750606;
            hashCode = hashCode * -1521134295 + Min.GetHashCode();
            hashCode = hashCode * -1521134295 + Max.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Range left, Range right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Range left, Range right)
        {
            return !(left == right);
        }
    }
}