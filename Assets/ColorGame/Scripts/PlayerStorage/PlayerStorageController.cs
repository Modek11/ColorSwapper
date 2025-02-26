using System;
using ColorGame.Scripts.GameVisuals.Colors;
using ColorGame.Scripts.UI.MainMenu;
using UnityEngine;

namespace ColorGame.Scripts.PlayerStorage
{
    public class PlayerStorageController : MonoBehaviour
    {
        [SerializeField] private PlayerStorageSave _playerStorageSave;

        public event Action<PanelType> OnSaveUpdated;

        public int GetHighestScore()
        {
            return _playerStorageSave.HighestScore;
        }

        public void SaveHighestScore(int score)
        {
            _playerStorageSave.HighestScore = score;
        }

        public Sprite GetAvatar()
        {
            return _playerStorageSave.PlayerAvatar;
        }

        public void SaveAvatarSprite(Sprite avatar)
        {
            _playerStorageSave.PlayerAvatar = avatar;
            OnSaveUpdated?.Invoke(PanelType.Avatar);
        }

        public Color GetTrail()
        {
            return _playerStorageSave.PlayerTrial;
        }

        public Color GetStartTrailColor()
        {
            var endColor = _playerStorageSave.PlayerTrial;
            endColor.a = 1;
            return endColor;
        }

        public Color GetEndTrailColor()
        {
            var endColor = _playerStorageSave.PlayerTrial;
            endColor.a = 0;
            return endColor;
        }

        public void SaveTrial(Color trial)
        {
            _playerStorageSave.PlayerTrial = trial;
            OnSaveUpdated?.Invoke(PanelType.Trails);
        }

        public ColorPalette GetColorPalette()
        {
            return _playerStorageSave.ColorPalette;
        }

        public void SaveColorPalette(ColorPalette colorPalette)
        {
            _playerStorageSave.ColorPalette = colorPalette;
            OnSaveUpdated?.Invoke(PanelType.ColorPalette);
        }
    }
}