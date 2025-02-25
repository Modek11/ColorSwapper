using System.Collections.Generic;
using UnityEngine;

namespace ColorGame.Scripts.GameVisuals.Trails
{
    [CreateAssetMenu(fileName = "TrailsHolder", menuName = "ScriptableObjects/TrailsHolder")]
    public class TrailsHolder : ScriptableObject
    {
        public Sprite SpritePreview;
        public List<Color> trails = new List<Color>();
    }
}
