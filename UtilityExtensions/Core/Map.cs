using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UtilityExtensions.Extensions;

namespace UtilityExtensions.Core
{
    public sealed class Map : Dictionary<string, object>
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Map otherMap)
            {
                if (this.Keys.Count != otherMap.Keys.Count)
                    return false;

                foreach (var item in this)
                {
                    if (!otherMap.ContainsKey(item.Key))
                        return false;

                    if (!CompareObjects(item.Value, otherMap[item.Key]))
                        return false;
                }
            }
            else return false;

            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = 340188902;
            hashCode = hashCode * -1521134295 + EqualityComparer<KeyCollection>.Default.GetHashCode(Keys);
            hashCode = hashCode * -1521134295 + EqualityComparer<ValueCollection>.Default.GetHashCode(Values);
            return hashCode;
        }

        public static bool operator ==(Map left, Map right)
        {
            return EqualityComparer<Map>.Default.Equals(left, right);
        }

        public static bool operator !=(Map left, Map right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            StringBuilder sr = new StringBuilder();
            sr.Append("{");
            int keyCount = Keys.Count;
            using (var cc = new CultureChanger(CultureInfo.InvariantCulture))
            {
                foreach (var item in this)
                {
                    keyCount--;

                    sr.Append($"\"{item.Key}\":");
                    sr.Append(ConvertObjectToString(item.Value));

                    if (keyCount > 0)
                        sr.Append(",");
                }
            }
            sr.Append("}");
            return sr.ToString();
        }

        private class MapObjectComparer : IEqualityComparer<object>
        {
            public new bool Equals(object x, object y)
            {
                return CompareObjects(x, y);
            }

            public int GetHashCode(object obj)
            {
                return obj?.GetHashCode() ?? 0;
            }
        }

        private static bool CompareObjects(object a, object b)
        {
            if (a == null && b == null)
                return true;

            if (a?.GetType() != b?.GetType())
                return false;

            if (a.GetType().IsIEnumerable() && a is Map == false)
                return CompareIEnumerable(a as IEnumerable, b as IEnumerable);
            else
                return a.Equals(b);
        }

        private static bool CompareIEnumerable(IEnumerable a, IEnumerable b)
        {
            return ListExtensions.EqualsSequenceIgnoreOrder(a, b, new MapObjectComparer());
        }

        private static string ConvertObjectToString(object obj)
        {
            StringBuilder sr = new StringBuilder();

            if (obj == null)
                sr.Append("null");
            else if (obj is string)
                sr.Append($"\"{obj}\"");
            else if (obj.GetType().IsIEnumerable() && obj is Map == false)
                sr.Append(ConvertIEnumerableToString(obj as IEnumerable));
            else
                sr.Append(obj.ToString());

            return sr.ToString();
        }

        private static string ConvertIEnumerableToString(IEnumerable obj)
        {
            if (obj == null)
                return "null";

            StringBuilder sr = new StringBuilder();
            sr.Append("[");

            foreach (var item in obj)
            {
                sr.Append(ConvertObjectToString(item));
                sr.Append(",");
            };
            if (sr.Length > 1)
                sr.Length--;
            sr.Append("]");

            return sr.ToString();
        }
    }
}