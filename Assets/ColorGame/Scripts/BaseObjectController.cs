using System.Collections.Generic;
using ColorGame.Scripts.GameHandlers;
using DG.Tweening;
using UnityEngine;

namespace ColorGame.Scripts
{
    public class BaseObjectController : MonoBehaviour
    {
        [SerializeField] protected bool enableRotation;
        [SerializeField] protected bool invertRotation;
        [SerializeField] protected float rotationDuration;
        [SerializeField] protected List<SpriteRenderer> colorAList;
        [SerializeField] protected List<SpriteRenderer> colorBList;
        [SerializeField] protected List<SpriteRenderer> colorCList;
        [SerializeField] protected List<SpriteRenderer> colorDList;

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
            
            SetupColor(colorAList, colorPalette.colorA);
            SetupColor(colorBList, colorPalette.colorB);
            SetupColor(colorCList, colorPalette.colorC);
            SetupColor(colorDList, colorPalette.colorD);
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
