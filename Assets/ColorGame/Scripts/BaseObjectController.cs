using System.Collections.Generic;
using ColorGame.Scripts.Colors;
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
        [SerializeField] protected List<ColorElement> colorElementsAList;
        [SerializeField] protected List<ColorElement> colorElementsBList;
        [SerializeField] protected List<ColorElement> colorElementsCList;
        [SerializeField] protected List<ColorElement> colorElementsDList;

        protected List<List<ColorElement>> colorElementsList;

        private float _obstacleHeight;
        //TODO: star should be created here, prob as a list because some obstacles can have more than one
        
        public float ObstacleHeight => GetObstacleHeight();

        protected virtual void Awake()
        {
            colorElementsList = new List<List<ColorElement>>()
            {
                colorElementsAList,
                colorElementsBList,
                colorElementsCList,
                colorElementsDList
            };
        }

        protected virtual void Start()
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

            for (var i = 0; i < colorElementsList.Count; i++)
            {
                foreach (var element in colorElementsList[i])
                {
                    element.SpriteRenderer.color = colorPalette[i];
                }
            }
        }

        protected void OnValidate()
        {
            if (boxCollider2D == null)
            {
                boxCollider2D = GetComponent<BoxCollider2D>();
            }
        }
    }
}
