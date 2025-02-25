using System.Collections.Generic;
using UnityEngine;

namespace ColorGame.Scripts.GameVisuals.Colors
{
    [CreateAssetMenu(fileName = "ColorPalette", menuName = "ScriptableObjects/ColorPalettes")]
    public class ColorPalettesHolder : ScriptableObject
    {
        public List<ColorPalette> colorPalettes = new List<ColorPalette>();

        private void OnValidate()
        {
            foreach (var singleColorPalette in colorPalettes)
            {
                singleColorPalette.colorA.a = 255;
                singleColorPalette.colorB.a = 255;
                singleColorPalette.colorC.a = 255;
                singleColorPalette.colorD.a = 255;
            }
        }
    }
}
