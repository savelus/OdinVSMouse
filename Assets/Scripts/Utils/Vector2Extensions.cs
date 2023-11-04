using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class Vector2Extensions
    {
        public static Vector2 With(this Vector2 self, float? x = null, float? y = null, float? z = null) =>
            new Vector2(x ?? self.x, y ?? self.y);
        public static Vector2 WithX(this Vector2 self, float x) => new Vector2(x, self.y);
        public static Vector2 WithY(this Vector2 self, float y) => new Vector2(self.x, y);

        public static Vector2 Add(this Vector2 self, float? x = null, float? y = null, float? z = null) =>
            new Vector2(self.x + x ?? 0, self.y + y ?? 0);
        public static Vector2 AddX(this Vector2 self, float x) => new Vector2(self.x + x, self.y);
        public static Vector2 AddY(this Vector2 self, float y) => new Vector2(self.x, self.y + y);
    }
}
