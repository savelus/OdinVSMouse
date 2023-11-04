using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utils.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> YieldForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var item in enumeration)
            {
                action(item);
                yield return item;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var item in enumeration)
            {
                action(item);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T, int> action)
        {
            int i = 0;
            foreach (var item in enumeration)
            {
                action(item, i);
                i++;
            }
        }

        public static void For<T>(this IEnumerable<T> enumeration, Action<int> action)
        {
            for (int i = 0; i < enumeration.Count(); i++)
            {
                action(i);
            }
        }
    }
}
