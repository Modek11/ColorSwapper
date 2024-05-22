using System.Collections.Generic;
using UnityEngine;

namespace ColorGame.Scripts.Colors
{
    [CreateAssetMenu(fileName = "ColorPalette", menuName = "ScriptableObjects/ColorPalettes")]
    public class ColorPalettesHolder : ScriptableObject
    {
        public List<ColorPalette> colorPalettes = new List<ColorPalette>();

        private void OnValidate()
        {
            foreach (var singleColorPalette in colorPalettes)
            {
                singleColorPalette.color1.a = 255;
                singleColorPalette.color2.a = 255;
                singleColorPalette.color3.a = 255;
                singleColorPalette.color4.a = 255;
            }
        }
    }
}
