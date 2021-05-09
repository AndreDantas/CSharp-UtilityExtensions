﻿using System;
using System.Collections.Generic;
using System.Text;
using UtilityExtensions.Extensions;

namespace UtilityExtensions.Core
{
    public struct Numeric
    {
        private double value;

        private Numeric(double value)
        {
            this.value = value;
        }

        public Numeric WithPrecision(int precision, MidpointRounding rounding = MidpointRounding.ToEven)
        {
            return new Numeric(Math.Round(value, Math.Abs(precision), rounding));
        }

        public long Long => (long)value;
        public int Int => (int)value;
        public double Double => value;
        public float Float => (float)value;
        public decimal Decimal => (decimal)value;

        public static implicit operator long(Numeric n) => n.Long;

        public static implicit operator Numeric(long n) => new Numeric(n);

        public static implicit operator int(Numeric n) => n.Int;

        public static implicit operator Numeric(int n) => new Numeric(n);

        public static implicit operator double(Numeric n) => n.Double;

        public static implicit operator Numeric(double n) => new Numeric(n);

        public static implicit operator float(Numeric n) => n.Float;

        public static implicit operator Numeric(float n) => new Numeric(n);

        public static implicit operator decimal(Numeric n) => n.Decimal;

        public static implicit operator Numeric(decimal n) => new Numeric((double)n);

        public static Numeric operator +(Numeric n1, Numeric n2) => n1.value + n2.value;

        public static Numeric operator +(Numeric n1, long n2) => n1.value + n2;

        public static Numeric operator +(Numeric n1, int n2) => n1.value + n2;

        public static Numeric operator +(Numeric n1, double n2) => n1.value + n2;

        public static Numeric operator +(Numeric n1, float n2) => n1.value + n2;

        public static Numeric operator +(Numeric n1, decimal n2) => n1.value + (double)n2;

        public static Numeric operator -(Numeric n1, Numeric n2) => n1.value - n2.value;

        public static Numeric operator -(Numeric n1, long n2) => n1.value - n2;

        public static Numeric operator -(Numeric n1, int n2) => n1.value - n2;

        public static Numeric operator -(Numeric n1, double n2) => n1.value - n2;

        public static Numeric operator -(Numeric n1, float n2) => n1.value - n2;

        public static Numeric operator -(Numeric n1, decimal n2) => n1.value - (double)n2;

        public static Numeric operator *(Numeric n1, Numeric n2) => n1.value * n2.value;

        public static Numeric operator *(Numeric n1, long n2) => n1.value * n2;

        public static Numeric operator *(Numeric n1, int n2) => n1.value * n2;

        public static Numeric operator *(Numeric n1, double n2) => n1.value * n2;

        public static Numeric operator *(Numeric n1, float n2) => n1.value * n2;

        public static Numeric operator *(Numeric n1, decimal n2) => n1.value * (double)n2;

        public static Numeric operator /(Numeric n1, Numeric n2) => n1.value / n2.value;

        public static Numeric operator /(Numeric n1, long n2) => n1.value / n2;

        public static Numeric operator /(Numeric n1, int n2) => n1.value / n2;

        public static Numeric operator /(Numeric n1, double n2) => n1.value / n2;

        public static Numeric operator /(Numeric n1, float n2) => n1.value / n2;

        public static Numeric operator /(Numeric n1, decimal n2) => n1.value / (double)n2;

        public static bool operator ==(Numeric left, Numeric right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Numeric left, Numeric right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is Numeric numeric &&
                   value.CloseTo(numeric.value);
        }

        public override int GetHashCode()
        {
            return 1835847388 + value.GetHashCode();
        }
    }
}