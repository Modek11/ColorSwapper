using ColorGame.Scripts.GameVisuals.Colors;
using UnityEngine;

namespace ColorGame.Scripts.PlayerStorage
{
    [CreateAssetMenu(fileName = "PlayerStorageSave", menuName = "ScriptableObjects/PlayerStorageSave")]
    public class PlayerStorageSave : ScriptableObject
    {
        public int Points;
        public Sprite PlayerAvatar;
        public Color PlayerTrial;
        public ColorPalette ColorPalette;
    }
}
