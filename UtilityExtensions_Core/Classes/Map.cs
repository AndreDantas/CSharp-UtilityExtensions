using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using UtilityExtensions_Core.Classes;

namespace CSharpUtilityExtensions
{
    public class Map : Dictionary<string, object>
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
                    if (!otherMap.ContainsKey(item.Key) ||
                        item.Value?.GetType() != otherMap[item.Key]?.GetType())
                        return false;

                    if (item.Value is Map itemMap)
                    {
                        if (itemMap != (Map)otherMap[item.Key])
                            return false;
                    }
                    else if (!item.Value.Equals(otherMap[item.Key]))
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
            using (var cc = new CultureChanger())
            {
                cc.ChangeCulture(CultureInfo.InvariantCulture);

                foreach (var item in this)
                {
                    keyCount--;

                    sr.Append($"\"{item.Key}\":");
                    if (item.Value == null)
                        sr.Append("null");
                    else if (item.Value is string)
                        sr.Append($"\"{item.Value}\"");
                    else
                        sr.Append(item.Value.ToString());

                    if (keyCount > 0)
                        sr.Append(",");
                }
            }
            sr.Append("}");
            return sr.ToString();
        }
    }
}