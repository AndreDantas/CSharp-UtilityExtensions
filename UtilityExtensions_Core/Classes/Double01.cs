using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using CSharpUtilityExtensions.Classes;

namespace UtilityExtensions_Core.Classes
{
    public struct Double01
    {
        private double value;

        public double Value { get => value; set => this.value = Math.Max(Math.Min(value, 1f), 0f); }

        public Double01(double value)
        {
            this.value = Math.Max(Math.Min(value, 1f), 0f);
        }

        public static implicit operator double(Double01 f) => f.Value;

        public static implicit operator Double01(double f) => new Double01(f);

        public static implicit operator Float01(Double01 f) => new Float01((float)(f.Value));

        public static implicit operator Double01(Float01 f) => new Double01(f.Value);

        public static Double01 operator +(Double01 d1, Double01 d2) => new Double01(d1.Value + d2.Value);

        public static Double01 operator +(Double01 d1, double d2) => new Double01(d1.Value + d2);

        public static double operator +(double d1, Double01 d2) => d1 + d2.Value;

        public static Double01 operator -(Double01 d1, Double01 d2) => new Double01(d1.Value - d2.Value);

        public static Double01 operator -(Double01 d1, double d2) => new Double01(d1.Value - d2);

        public static double operator -(double d1, Double01 d2) => d1 - d2.Value;

        /// <summary>
        /// Increases the value by 0.1.
        /// </summary>
        public static Double01 operator ++(Double01 d1) => new Double01(d1.Value += 0.1f);

        /// <summary>
        /// Decreases the value by 0.1.
        /// </summary>
        public static Double01 operator --(Double01 d1) => new Double01(d1.Value -= 0.1f);

        public static Double01 operator *(Double01 d1, Double01 d2) => new Double01(d1.Value * d2.Value);

        public static Double01 operator /(Double01 d1, Double01 d2) => new Double01(d1.Value / d2.Value);

        public static bool operator ==(Double01 d1, Double01 d2) => EqualityComparer<Double01>.Default.Equals(d1, d2);

        public static bool operator !=(Double01 d1, Double01 d2) => !(d1 == d2);

        public static bool operator >(Double01 d1, Double01 d2) => d1.Value > d2.Value;

        public static bool operator <(Double01 d1, Double01 d2) => d1.Value < d2.Value;

        public static bool operator >=(Double01 d1, Double01 d2) => d1.Value >= d2.Value;

        public static bool operator <=(Double01 d1, Double01 d2) => d1.Value <= d2.Value;

        public static bool operator true(Double01 d1) => d1.Value <= 1 && d1.Value > 0;

        public static bool operator false(Double01 d1) => d1.Value == 0;

        public override bool Equals(object obj)
        {
            var @double = (Double01)obj;
            return @double != null &&
                   Value == @double.Value;
        }

        public override int GetHashCode()
        {
            return -1937169414 + Value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}