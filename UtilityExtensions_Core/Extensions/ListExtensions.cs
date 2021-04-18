using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpUtilityExtensions.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Removes items from this list where the condition applies.
        /// </summary>
        /// <returns> The number of elements removed </returns>
        /// <param name="l"> </param>
        /// <param name="condition"> </param>
        /// <typeparam name="T"> </typeparam>
        public static int RemoveWhere<T>(this List<T> l, Predicate<T> condition)
        {
            if (l == null || condition == null)
                return 0;

            int count = 0;

            for (int i = l.Count - 1; i >= 0; i--)
            {
                if (condition(l[i]))
                {
                    l.RemoveAt(i);
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Removes null elements in this list
        /// </summary>
        /// <returns> The number of elements removed </returns>
        /// <typeparam name="T"> </typeparam>
        /// <param name="l"> </param>
        public static int RemoveNull<T>(this List<T> l)
        {
            if (l == null || l.Count == 0)
                return 0;

            int count = 0;

            for (int i = l.Count - 1; i >= 0; i--)
            {
                if (l[i] == null)
                {
                    l.RemoveAt(i);
                    count++;
                }
            }

            return count;
        }

        public static T Search<T>(this List<T> l, Func<T, bool> searchFunction)
        {
            if (l == null || l.Count == 0)
                return default;

            for (int i = 0; i < l.Count; i++)
            {
                if (searchFunction(l[i]))
                    return l[i];
            }

            return default;
        }

        /// <summary>
        /// Returns a new List of the given Type after using the map function on each element.
        /// </summary>
        /// <remarks>
        /// Usage: (Converting a list of int to string) stringList = intList.Map((e) = e.ToString());
        /// </remarks>
        /// <param name="oldList"> </param>
        /// <param name="map"> </param>
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <returns> </returns>
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
        /// <typeparam name="T"> </typeparam>
        /// <param name="list"> </param>
        /// <returns> </returns>
        public static bool IsEmpty<T>(this List<T> list)
        {
            return list?.Count == 0;
        }

        /// <summary>
        /// Checks if the index is valid.
        /// </summary>
        /// <param name="list"> </param>
        /// <param name="index"> </param>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        public static bool ValidIndex<T>(this List<T> list, int index)
        {
            return (index >= 0 && index < list.Count);
        }

        /// <summary>
        /// Checks if the <paramref name="list2" /> items are contained in <paramref name="list1"
        /// />, ignoring order.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="list1"> </param>
        /// <param name="list2"> </param>
        /// <param name="comparer"> </param>
        /// <returns> </returns>
        public static bool ContainsSequenceIgnoreOrder(this IEnumerable list1, IEnumerable list2, IEqualityComparer<object> comparer = null)
        {
            if (list1 is ICollection ilist1 && list2 is ICollection ilist2 && (ilist1.Count == 0 || ilist2.Count == 0 || ilist2.Count > ilist1.Count))
                return false;

            if (comparer == null)
                comparer = EqualityComparer<object>.Default;

            var itemCounts = new Dictionary<object, int>(comparer);
            foreach (var s in list1)
            {
                if (itemCounts.ContainsKey(s))
                {
                    itemCounts[s]++;
                }
                else
                {
                    itemCounts.Add(s, 1);
                }
            }
            foreach (var s in list2)
            {
                if (itemCounts.ContainsKey(s))
                {
                    itemCounts[s]--;
                }
                else
                {
                    return false;
                }
            }
            return itemCounts.Values.All(c => c >= 0);
        }

        #region String

        /// <summary>
        /// Adds the string to this list if it's not empty or null
        /// </summary>
        /// <param name="l"> </param>
        /// <param name="s"> </param>
        public static void AddIfNotEmpty(this List<string> l, string s)
        {
            if (!string.IsNullOrEmpty(s))
                l.Add(s);
        }

        #endregion String
    }
}