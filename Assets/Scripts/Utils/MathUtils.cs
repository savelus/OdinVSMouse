using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    internal static class MathUtils
    {
        /// <summary> Converts degree angle from 0 to 360 to derection vector. </summary>
        public static Vector2 AngleToDirection(float angleDeg) => new(Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad));

        /// <returns> Degree angle from 0 to 360. </returns>
        public static float DirectionToAngle(Vector2 direction) => (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 360) % 360;
    }
}
