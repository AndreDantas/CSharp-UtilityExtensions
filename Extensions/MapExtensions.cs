using System.Reflection;
using UtilityExtensions.Classes;

namespace UtilityExtensions.Extensions
{
    public static class MapExtensions
    {

        public static Map toMap(this object obj)
        {
            PropertyInfo[] properties;
            properties = obj.GetType().GetProperties();

            Map map = new Map();
            foreach (var prop in properties)
            {
                map[prop.Name] = prop.GetValue(obj);
            }

            return map;
        }
    }
}