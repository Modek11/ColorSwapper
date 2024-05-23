using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace ColorGame.Scripts.Obstacles
{
    public class BaseObstacleHandler : MonoBehaviour
    {
        [SerializeField] private float rotationDuration;
        [SerializeField] private List<SpriteRenderer> colorA;
        [SerializeField] private List<SpriteRenderer> colorB;
        [SerializeField] private List<SpriteRenderer> colorC;
        [SerializeField] private List<SpriteRenderer> colorD;

        protected void Start()
        {
            SetupColors();

            var rotation = new Vector3(0, 0, 180);
            transform.DORotate(rotation, rotationDuration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

        protected void SetupColors()
        {
            var colorPalette = GameHandler.Instance.CurrentActiveColorPalette;
            
            SetupColor(colorA, colorPalette.colorA);
            SetupColor(colorB, colorPalette.colorB);
            SetupColor(colorC, colorPalette.colorC);
            SetupColor(colorD, colorPalette.colorD);
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
