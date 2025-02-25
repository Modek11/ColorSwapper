using System.Collections.Generic;
using UnityEngine;

namespace ColorGame.Scripts.GameVisuals.Avatars
{
    [CreateAssetMenu(fileName = "AvatarsHolder", menuName = "ScriptableObjects/AvatarsHolder")]
    public class AvatarsHolder : ScriptableObject
    {
        public Color BaseAvatarColor;
        public List<Sprite> avatars = new List<Sprite>();
    }
}