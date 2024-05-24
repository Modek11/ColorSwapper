using ColorGame.Scripts.Colors;
using UnityEngine;

namespace ColorGame.Scripts.GameHandlers
{
    public class ColorsHandler : MonoBehaviour
    {
        [SerializeField] private ColorPalettesHolder colorPalettesHolder;

        public ColorPalette CurrentActiveColorPalette { get; private set; }
        public Color CurrentActiveColor { get; private set; }

        private void Awake()
        {
            var randomNumber = Random.Range(0, colorPalettesHolder.colorPalettes.Count);
            CurrentActiveColorPalette = colorPalettesHolder.colorPalettes[randomNumber];

            var randomColorIndex = Random.Range(0, CurrentActiveColorPalette.Count);
            CurrentActiveColor = CurrentActiveColorPalette[randomColorIndex];
        }
    }
}
