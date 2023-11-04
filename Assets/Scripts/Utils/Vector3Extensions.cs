using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class Vector3Extensions
    {
        public static Vector3 With(this Vector3 self, float? x = null, float? y = null, float? z = null) =>
            new Vector3(x ?? self.x, y ?? self.y, z ?? self.z);
        public static Vector3 WithX(this Vector3 self, float x) => new Vector3(x, self.y, self.z);
        public static Vector3 WithY(this Vector3 self, float y) => new Vector3(self.x, y, self.z);
        public static Vector3 WithZ(this Vector3 self, float z) => new Vector3(self.x, self.y, z);

        public static Vector3 WithX(this Vector3 self, Func<float, float> xChangeFunc) => new Vector3(xChangeFunc(self.x), self.y, self.z);
        public static Vector3 WithY(this Vector3 self, Func<float, float> yChangeFunc) => new Vector3(self.x, yChangeFunc(self.y), self.z);
        public static Vector3 WithZ(this Vector3 self, Func<float, float> zChangeFunc) => new Vector3(self.x, self.y, zChangeFunc(self.z));

        public static Vector3 Add(this Vector3 self, float? x = null, float? y = null, float? z = null) =>
            new Vector3(self.x + x ?? 0, self.y + y ?? 0, self.z + z ?? 0);
        public static Vector3 AddX(this Vector3 self, float x) => new Vector3(self.x + x, self.y, self.z);
        public static Vector3 AddY(this Vector3 self, float y) => new Vector3(self.x, self.y + y, self.z);
        public static Vector3 AddZ(this Vector3 self, float z) => new Vector3(self.x, self.y, self.z + z);

        public static Vector2 AsVector2(this Vector3 self) => new Vector2(self.x, self.y);
    }
}
