using System;
using System.Collections.Generic;
using System.Threading;
using ColorGame.Scripts.Colors;
using ColorGame.Scripts.GameHandlers;
using ColorGame.Scripts.Patterns;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace ColorGame.Scripts.InteractableObjects
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BaseObjectController : MonoBehaviour
    {
        private const float DestroyCheckPeriod = 1.5f;
        private const float DestroyMinDistance = 10f;
        
        [SerializeField, HideInInspector] private BoxCollider2D boxCollider2D;
        [SerializeField] protected bool enableRotation;
        [SerializeField] protected bool invertRotation;
        [SerializeField] protected float rotationDuration;
        [SerializeField] protected List<ColorElement> colorElementsAList;
        [SerializeField] protected List<ColorElement> colorElementsBList;
        [SerializeField] protected List<ColorElement> colorElementsCList;
        [SerializeField] protected List<ColorElement> colorElementsDList;

        private List<List<ColorElement>> _colorElementsList;
        private float _obstacleHeight;
        private CancellationTokenSource _token;

        protected virtual bool ShouldChangeOnGlobalColorChange { get; set; } = true;
        private List<List<ColorElement>> ColorElementsList
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
        public float ObstacleHeight
        {
            get
            {
                if (_obstacleHeight <= 0) 
                {
                    boxCollider2D.enabled = true;
                    _obstacleHeight = boxCollider2D.bounds.size.y;
                    boxCollider2D.enabled = false;
                }

                return _obstacleHeight;
            }
        }
        
        //TODO: star should be created here, prob as a list because some obstacles can have more than one
        
        protected void Awake()
        {
            if (ShouldChangeOnGlobalColorChange && GameHandler.Instance != null)
            {
                GameHandler.Instance.ColorsHandler.OnGlobalColorChanged += ChangeActiveColliders;
            }
            
            _token = new CancellationTokenSource();
            CheckForDestroy().Forget();
        }
        
        protected virtual void Start()
        {
            SetupColors();

            if (enableRotation)
            {
                StartRotating();
            }
        }

        private void OnDestroy()
        {
            if (GameHandler.Instance != null)
            {
                GameHandler.Instance.ColorsHandler.OnGlobalColorChanged -= ChangeActiveColliders;
            }
            
            _token.Cancel();
            _token.Dispose();
        }

        private void StartRotating()
        {
            var rotation = new Vector3(0, 0, 180);
            rotation = invertRotation ? rotation * -1 : rotation;
            transform.DORotate(rotation, rotationDuration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

        private void SetupColors()
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

        private void ChangeActiveColliders(Color activeColor)
        {
            foreach (var sameColorObjects in ColorElementsList)
            {
                var shouldBeEnabled = Helper.IsDifferentColorRGB(sameColorObjects[0].SpriteRenderer.color, activeColor);
                foreach (var element in sameColorObjects)
                {
                    element.Collider2D.enabled = shouldBeEnabled;
                }
            }
        }

        private async UniTaskVoid CheckForDestroy()
        {
            while (true)
            {
                CheckDistanceBetweenPlayer();
                await UniTask.Delay(TimeSpan.FromSeconds(DestroyCheckPeriod), cancellationToken: _token.Token);
            }
        }

        private void CheckDistanceBetweenPlayer()
        {
            var position = transform.position;
            var playerPosition = GameHandler.Instance.PlayerController.transform.position;
            var distance = Vector3.Distance(position, playerPosition);

            if (playerPosition.y > position.y && distance > DestroyMinDistance)
            {
                Destroy(gameObject);
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
