using System.ComponentModel;
using System.Reflection;
using UtilityExtensions.Core;

namespace UtilityExtensions.Extensions
{
    public static class MapExtensions
    {
        public static Map ToMap(this object obj)
        {
            if (obj == null)
                return null;

            var properties = TypeDescriptor.GetProperties(obj);

            Map map = new Map();

            foreach (PropertyDescriptor prop in properties)
            {
                if (prop.PropertyType.IsSimpleType() || prop.PropertyType.IsIEnumerable())
                    map[prop.Name] = prop.GetValue(obj);
                else
                    map[prop.Name] = prop.GetValue(obj).ToMap();
            }

            return map;
        }

        public static T FromMap<T>(this Map map) where T : class, new()
        {
            var obj = new T();
            var objType = obj.GetType();

            foreach (var item in map)
            {
                var prop = objType.GetProperty(item.Key);

                if (prop == null)
                    continue;

                if (item.Value is Map)
                {
                    var mi = typeof(MapExtensions).GetMethod("FromMap");
                    var miRef = mi.MakeGenericMethod(prop.PropertyType);
                    prop.SetValue(obj, miRef.Invoke(null, new object[1] { item.Value }), null);
                }
                else
                {
                    prop.SetValue(obj, item.Value.ConvertTo(prop.PropertyType), null);
                }
            }

            return obj;
        }
    }
}