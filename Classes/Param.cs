using System;
using System.Collections.Generic;

namespace CSharpUtilityExtensions.Classes
{
    public class Param<T>
    {
        public Param(T value)
        {
            this.dirty = false;
            this.value = value;
        }
        private bool dirty;
        private T value;
        public T Value
        {
            get => value;

            set
            {
                if (!EqualityComparer<T>.Default.Equals(this.value, value))
                    this.Dirty = true;
                this.value = value;
            }
        }
        public bool Dirty
        {
            get => dirty ? !(dirty = false) : dirty;

            private set => dirty = value;
        }

        public static implicit operator Param<T>(T d) => new Param<T>(d);
        public static implicit operator T(Param<T> d) => d.value;


        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is Param<T> param &&
                   EqualityComparer<T>.Default.Equals(Value, param.Value);
        }

        public override int GetHashCode()
        {
            int hashCode = 189844002;
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(Value);
            return hashCode;
        }
    }
}