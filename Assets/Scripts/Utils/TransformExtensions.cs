using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SharedUtils.Extensions
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
