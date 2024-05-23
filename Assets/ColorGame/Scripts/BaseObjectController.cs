using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace ColorGame.Scripts
{
    public class BaseObjectController : MonoBehaviour
    {
        [SerializeField] protected bool enableRotation;
        [SerializeField] protected bool invertRotation;
        [SerializeField] protected float rotationDuration;
        [SerializeField] protected List<SpriteRenderer> colorA;
        [SerializeField] protected List<SpriteRenderer> colorB;
        [SerializeField] protected List<SpriteRenderer> colorC;
        [SerializeField] protected List<SpriteRenderer> colorD;

        protected void Start()
        {
            SetupColors();

            if (enableRotation)
            {
                StartRotating();
            }
        }

        protected void StartRotating()
        {
            var rotation = new Vector3(0, 0, 180);
            rotation = invertRotation ? rotation * -1 : rotation;
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
