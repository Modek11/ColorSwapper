using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace ColorGame.Scripts.Obstacles
{
    public class BaseObstacleHandler : MonoBehaviour
    {
        [SerializeField] private float rotationDuration;
        [SerializeField] private List<SpriteRenderer> color1;
        [SerializeField] private List<SpriteRenderer> color2;
        [SerializeField] private List<SpriteRenderer> color3;
        [SerializeField] private List<SpriteRenderer> color4;

        protected void Start()
        {
            SetupColors();

            var rotation = new Vector3(0, 0, 360);
            transform.DORotate(rotation, rotationDuration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

        protected void SetupColors()
        {
            var colorPalette = GameHandler.Instance.CurrentActiveColorPalette;
            
            SetupColor(color1, colorPalette.color1);
            SetupColor(color2, colorPalette.color2);
            SetupColor(color3, colorPalette.color3);
            SetupColor(color4, colorPalette.color4);
        }

        private void SetupColor(List<SpriteRenderer> colorList, Color color)
        {
            foreach (var spriteRenderer in colorList)
            {
                spriteRenderer.color = color;
            }
        }
    }
}
