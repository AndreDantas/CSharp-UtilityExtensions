
using System;

namespace CSharpUtilityExtensions
{
    public struct Range
    {

        private double _min;
        private double _max;
        private double _value;

        public double min { get => _min; set => _min = Math.Min(value, max); }
        public double max { get => _max; set => _max = Math.Max(value, min); }
        public double value { get => Math.Min(Math.Max(_value, min), max); set => this._value = Math.Min(Math.Max(value, min), max); }

        public Range(double min, double value, double max) : this()
        {
            this.min = min;
            this.max = max;
            this.value = value;
            
        }

        public Range(double min, double max) : this()
        {
            this.min = min;
            this.max = max;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return min + " --{" + value + "}-- " + max;
        }
    }
}