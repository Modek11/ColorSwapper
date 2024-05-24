using System.Collections.Generic;
using ColorGame.Scripts.GameHandlers;
using DG.Tweening;
using UnityEngine;

namespace ColorGame.Scripts
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BaseObjectController : MonoBehaviour
    {
        [SerializeField, HideInInspector] private BoxCollider2D boxCollider2D;
        [SerializeField] protected bool enableRotation;
        [SerializeField] protected bool invertRotation;
        [SerializeField] protected float rotationDuration;
        [SerializeField] protected List<SpriteRenderer> colorAList;
        [SerializeField] protected List<SpriteRenderer> colorBList;
        [SerializeField] protected List<SpriteRenderer> colorCList;
        [SerializeField] protected List<SpriteRenderer> colorDList;

        private float _obstacleHeight;
        //TODO: star should be created here, prob as a list because some obstacles can have more than one
        
        public float ObstacleHeight => GetObstacleHeight();

        protected void Start()
        {
            SetupColors();

            if (enableRotation)
            {
                StartRotating();
            }
        }

        protected float GetObstacleHeight()
        {
            if (_obstacleHeight <= 0) 
            {
                boxCollider2D.enabled = true;
                _obstacleHeight = boxCollider2D.bounds.size.y;
                boxCollider2D.enabled = false;
            }

            return _obstacleHeight;
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
            var colorPalette = GameHandler.Instance.ColorsHandler.CurrentActiveColorPalette;
            
            SetupColor(colorAList, colorPalette.colorA);
            SetupColor(colorBList, colorPalette.colorB);
            SetupColor(colorCList, colorPalette.colorC);
            SetupColor(colorDList, colorPalette.colorD);
        }

        protected void SetupColor(List<SpriteRenderer> colorList, Color color)
        {
            foreach (var spriteRenderer in colorList)
            {
                spriteRenderer.color = color;
            }
        }

        private void OnValidate()
        {
            if (boxCollider2D == null)
            {
                boxCollider2D = GetComponent<BoxCollider2D>();
            }
        }
    }
}
