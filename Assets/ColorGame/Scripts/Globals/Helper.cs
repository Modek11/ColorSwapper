using UnityEngine;

namespace ColorGame.Scripts.Patterns
{
    public static class Helper
    {
        public static bool IsSameColorRGB(Color a, Color b)
        {
            return Mathf.Approximately(a.r, b.r) && Mathf.Approximately(a.g, b.g) && Mathf.Approximately(a.b, b.b);
        }

        public static bool IsDifferentColorRGB(Color a, Color b)
        {
            return !IsSameColorRGB(a, b);
        }
    }
}
