using UnityEngine.UI;

namespace Source.Extensions
{
    public static class TextExtensions
    {
        public static void SetFade(this Text text, float fade)
        {
            var color = text.color;

            color.a = fade;
            text.color = color;
        }
    }
}
