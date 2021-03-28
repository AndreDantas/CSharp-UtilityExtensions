using System.Collections.Generic;

namespace CSharpUtilityExtensions.Extensions
{
    public static class GenericExtensions
    {
        public static bool In<T>(this T obj, params T[] collection)
        {
            if (collection == null)
                return false;

            for (int i = 0; i < collection.Length; i++)
            {
                if (EqualityComparer<T>.Default.Equals(obj, collection[i]))
                    return true;
            }
            return false;
        }
    }
}
