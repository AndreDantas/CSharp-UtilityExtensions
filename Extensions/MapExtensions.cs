using System;
using System.Reflection;

namespace CSharpUtilityExtensions.Extensions
{
    public static class MapExtensions
    {

        public static Map toMap(this object obj)
        {
            PropertyInfo[] properties;
            properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            Map map = new Map();

            foreach (var prop in properties)
            {
                map[prop.Name] = prop.GetValue(obj, null);
            }

            return map;
        }

        public static T fromMap<T>(this Map map) where T : class, new()
        {
            try
            {
                var obj = new T();
                var objType = obj.GetType();

                foreach (var item in map)
                {
                    objType.GetProperty(item.Key).SetValue(obj, item.Value, null);
                }

                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}