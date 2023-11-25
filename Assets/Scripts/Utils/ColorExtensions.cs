using System;
using UnityEngine;

namespace Utils
{
    public static class ColorExtensions
    {
        public static Color With(this Color self, float? r = null, float? g = null, float? b = null, float? a = null) =>
            new Color(r ?? self.r, g ?? self.g, b ?? self.b, a ?? self.a);

        public static Color WithRgb(this Color self, float? r = null, float? g = null, float? b = null) =>
            new Color(r ?? self.r, g ?? self.g, b ?? self.b);

        public static Color WithR(this Color self, float r) => new Color(r, self.g, self.b);
        public static Color WithG(this Color self, float g) => new Color(self.r, g, self.b);
        public static Color WithB(this Color self, float b) => new Color(self.r, self.g, b);

        public static Color WithR(this Color self, Func<float, float> rChangeFunc) =>
            new Color(rChangeFunc(self.r), self.g, self.b);

        public static Color WithG(this Color self, Func<float, float> gChangeFunc) =>
            new Color(self.r, gChangeFunc(self.g), self.b);

        public static Color WithB(this Color self, Func<float, float> bChangeFunc) =>
            new Color(self.r, self.g, bChangeFunc(self.b));

        public static Color Add(this Color self, float? r = null, float? g = null, float? b = null, float? a = null) =>
            new Color(self.r + r ?? 0, self.g + g ?? 0, self.b + b ?? 0, self.a + a ?? 0);

        public static Color AddRgb(this Color self, float? r = null, float? g = null, float? b = null) =>
            new Color(self.r + r ?? 0, self.g + g ?? 0, self.b + b ?? 0);

        public static Color AddR(this Color self, float r) => new Color(self.r + r, self.g, self.b);
        public static Color AddG(this Color self, float g) => new Color(self.r, self.g + g, self.b);
        public static Color AddB(this Color self, float b) => new Color(self.r, self.g, self.b + b);

        public static Color WithAlpha(this Color color, float alpha) => new Color(color.r, color.g, color.b, alpha);
    }
}