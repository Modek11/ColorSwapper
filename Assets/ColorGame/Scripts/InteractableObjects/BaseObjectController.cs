using System.Collections.Generic;
using ColorGame.Scripts.Colors;
using ColorGame.Scripts.GameHandlers;
using DG.Tweening;
using UnityEngine;

namespace ColorGame.Scripts.InteractableObjects
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

        private List<List<ColorElement>> _colorElementsList;

        protected List<List<ColorElement>> ColorElementsList
        {
            get
            {
                if (_colorElementsList == null || _colorElementsList.Count <= 0)
                {
                    _colorElementsList = new List<List<ColorElement>>()
                    {
                        colorElementsAList,
                        colorElementsBList,
                        colorElementsCList,
                        colorElementsDList
                    };
                }

                return _colorElementsList;
            }
        }

        private float _obstacleHeight;
        //TODO: star should be created here, prob as a list because some obstacles can have more than one
        
        public float ObstacleHeight => GetObstacleHeight();

        protected virtual void Start()
        {
            SetupColors();

            if (enableRotation)
            {
                StartRotating();
            }
        }

        private float GetObstacleHeight()
        {
            if (_obstacleHeight <= 0) 
            {
                boxCollider2D.enabled = true;
                _obstacleHeight = boxCollider2D.bounds.size.y;
                boxCollider2D.enabled = false;
            }

            return _obstacleHeight;
        }

        private void StartRotating()
        {
            var rotation = new Vector3(0, 0, 180);
            rotation = invertRotation ? rotation * -1 : rotation;
            transform.DORotate(rotation, rotationDuration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

        protected virtual void SetupColors()
        {
            var colorPalette = GameHandler.Instance.ColorsHandler.CurrentActiveColorPalette;

            for (var i = 0; i < ColorElementsList.Count; i++)
            {
                foreach (var element in ColorElementsList[i])
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
