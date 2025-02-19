using ColorGame.Scripts.Colors;
using UnityEngine;
using UnityEngine.UI;

namespace ColorGame.Scripts.UI.MainMenu
{
    public class ColorElementUI : ColorElement
    {
        [SerializeField] private Image targetImage;

        public Image TargetImage => targetImage;
    }
}