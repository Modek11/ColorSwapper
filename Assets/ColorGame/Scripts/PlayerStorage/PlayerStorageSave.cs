using ColorGame.Scripts.GameVisuals.Colors;
using UnityEngine;

namespace ColorGame.Scripts.PlayerStorage
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "PlayerStorageSave", menuName = "ScriptableObjects/PlayerStorageSave")]
    public class PlayerStorageSave : ScriptableObject
    {
        public int HighestScore;
        public Sprite PlayerAvatar;
        public Color PlayerTrial;
        public ColorPalette ColorPalette;
    }
}
