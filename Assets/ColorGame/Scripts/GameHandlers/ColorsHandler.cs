using System;
using ColorGame.Scripts.Colors;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ColorGame.Scripts.GameHandlers
{
    public class ColorsHandler : MonoBehaviour
    {
        [SerializeField] private ColorPalettesHolder colorPalettesHolder;

        private int _colorIndex;
        private int[] _allowedIndexes = new int[3];

        public ColorPalette CurrentActiveColorPalette { get; private set; }
        public Color CurrentActiveColor { get; private set; }
        
        public event Action<Color> OnGlobalColorChanged;


        private void Awake()
        {
            //TODO: check color palette from save file
            ChangeCurrentActiveColorPalette();
        }

        private void ChangeCurrentActiveColorPalette()
        {
            var randomIndex = Random.Range(0, colorPalettesHolder.colorPalettes.Count);
            CurrentActiveColorPalette = colorPalettesHolder.colorPalettes[randomIndex];
        }

        public void ChangeCurrentActiveColor()
        {
            var randomIndex = Random.Range(0, CurrentActiveColorPalette.Count);
            Debug.Log($"{randomIndex} ||| {_colorIndex}");
            _colorIndex = randomIndex != _colorIndex ? randomIndex :
                (_colorIndex + 1) % CurrentActiveColorPalette.Count;
            
            CurrentActiveColor = CurrentActiveColorPalette[_colorIndex];
            OnGlobalColorChanged?.Invoke(CurrentActiveColor);
        }
    }
}
