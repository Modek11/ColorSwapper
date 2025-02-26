using System;
using System.Collections.Generic;
using ColorGame.Scripts.GameVisuals.Avatars;
using ColorGame.Scripts.GameVisuals.Colors;
using ColorGame.Scripts.GameVisuals.Trails;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ColorGame.Scripts.GameHandlers
{
    public class GameVisualsHandler : MonoBehaviour
    {
        [SerializeField] private AvatarsHolder avatarsHolder;
        [SerializeField] private TrailsHolder trailsHolder;
        [SerializeField] private ColorPalettesHolder colorPalettesHolder;

        private int _colorIndex;
        private int[] _allowedIndexes = new int[3];

        public Color BaseAvatarColor => avatarsHolder.BaseAvatarColor;
        public List<Sprite> AvailableAvatars => avatarsHolder.avatars;
        public Sprite TrailPreviewSprite => trailsHolder.SpritePreview;
        public List<Color> AvailableTrails => trailsHolder.trails;
        public ColorPalette CurrentActiveColorPalette => GameHandler.Instance.PlayerStorageController.GetColorPalette();
        public Color CurrentActiveColor { get; private set; }
        public List<ColorPalette> AvailableColorPalettes => colorPalettesHolder.colorPalettes;
        
        public event Action<Color> OnGlobalColorChanged;

        private void Start()
        {
            GameHandler.Instance.OnPlayerSpawned += ChangeCurrentActiveColor;
        }

        public void ChangeCurrentActiveColor()
        {
            var randomIndex = Random.Range(0, CurrentActiveColorPalette.Count);
            _colorIndex = randomIndex != _colorIndex ? randomIndex :
                (_colorIndex + 1) % CurrentActiveColorPalette.Count;
            
            CurrentActiveColor = CurrentActiveColorPalette[_colorIndex];
            OnGlobalColorChanged?.Invoke(CurrentActiveColor);
        }
    }
}