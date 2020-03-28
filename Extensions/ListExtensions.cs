using System;
using System.Collections.Generic;

namespace UtilityExtensions.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Removes items from the list where the condition applies.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="condition"></param>
        /// <typeparam name="T"></typeparam>
        public static void RemoveWhere<T>(this List<T> l, Predicate<T> condition)
        {
            if (l == null || condition == null)
                return;
            
            for (int i = l.Count - 1; i >= 0; i--)
            {

                if (condition(l[i]))
                    l.RemoveAt(i);
            }
        }

        /// <summary>
        /// Returns a new List of the given Type after using the map function on each element.
        /// </summary>
        /// <param name="oldList"></param>
        /// <param name="map"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public static List<T2> Map<T1, T2>(this List<T1> oldList, Func<T1, T2> map)
        {
            if (oldList == null || map == null)
                return null;

            List<T2> newList = new List<T2>();

            for (int i = 0; i < oldList.Count; i++)
            {
                newList.Add(map(oldList[i]));
            }

            return newList;
        }

        public static void TryAddRange<T>(this List<T> l, List<T> other)
        {
            if (l == null)
                return;
            if (other != null)
                l.AddRange(other);
        }


        /// <summary>
        /// Checks if the list is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this List<T> list)
        {
            return list?.Count == 0;
        }

        /// <summary>
        /// Checks if the index is valid.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool ValidIndex<T>(this List<T> list, int index)
        {
            return (index >= 0 && index < list.Count);
        }

    }
}