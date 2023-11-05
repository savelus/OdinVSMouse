using System;
using UnityEngine;

namespace Utils
{
    public static class TransformExtensions
    {
        public static void ForEachChield(this Transform transform, Action<Transform> action)
        {
            foreach (Transform chield in transform)
            {
                action(chield);
            }
        }

        public static void ForEachChieldReqursive(this Transform transform, Action<Transform> action)
        {
            foreach (Transform chield in transform)
            {
                action(chield);
                ForEachChieldReqursive(chield, action);
            }
        }
    }
}
