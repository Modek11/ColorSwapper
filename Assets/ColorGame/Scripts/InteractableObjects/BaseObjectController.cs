using System;
using System.Collections.Generic;
using System.Threading;
using ColorGame.Scripts.GameHandlers;
using ColorGame.Scripts.GameVisuals.Colors;
using ColorGame.Scripts.Globals;
using ColorGame.Scripts.InteractableObjects.Obstacles;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace ColorGame.Scripts.InteractableObjects
{
    public class BaseObjectController : MonoBehaviour
    {
        private const float DestroyCheckPeriod = 1.5f;
        private const float DestroyMinDistance = 10f;
        private const float RotateRight = -360;
        private const float RotateLeft = 360;
        
        [SerializeField] protected ObstacleParent obstacleParent;
        
        [Space]
        [SerializeField] private bool randomizeRotation; // start rotation
        [SerializeField] private bool enableRotation;
        [SerializeField] private bool invertRotationRandomly;
        [SerializeField] private float rotationDuration;
        
        [Space]
        [SerializeField] protected bool randomizeMovement = false; // start movement pos
        [SerializeField] protected bool enableMovement = false;
        [SerializeField] protected bool invertMovementRandomly = false;
        [SerializeField] protected float movementDuration = 5f;
        
        [SerializeField, HideInInspector] private List<ColorElement> colorElementsAList = new();
        [SerializeField, HideInInspector] private List<ColorElement> colorElementsBList = new();
        [SerializeField, HideInInspector] private List<ColorElement> colorElementsCList = new();
        [SerializeField, HideInInspector] private List<ColorElement> colorElementsDList = new();

        private List<List<ColorElement>> _colorElementsList;
        private CancellationTokenSource _token;

        protected virtual bool ShouldChangeOnGlobalColorChange { get; } = true;
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

        public float ObstacleHeight => obstacleParent.ObstacleHeight;
        
        protected void Awake()
        {
            if (ShouldChangeOnGlobalColorChange && GameHandler.Instance != null)
            {
                ChangeActiveColliders(GameHandler.Instance.GameVisualsHandler.CurrentActiveColor);
                GameHandler.Instance.GameVisualsHandler.OnGlobalColorChanged += ChangeActiveColliders;
            }
            
            _token = new CancellationTokenSource();
            CheckForDestroy().Forget();
        }
        
        protected void Start()
        {
            SetupColors();
            
            if (randomizeRotation)
            {
                RandomizeRotation();
            }

            if (enableRotation)
            {
                StartRotating();
            }

            if (enableMovement)
            {
                StartMovement();
            }

            if (randomizeMovement)
            {
                RandomizeMovement();
            }
        }

        private void OnDestroy()
        {
            if (GameHandler.Instance != null)
            {
                GameHandler.Instance.GameVisualsHandler.OnGlobalColorChanged -= ChangeActiveColliders;
            }
            
            _token.Cancel();
            _token.Dispose();
        }

        private void StartRotating()
        {
            var rotationEuler = invertRotationRandomly && UnityEngine.Random.Range(0f, 1f) > 0.5f ? RotateLeft : RotateRight;
            var rotation = new Vector3(0, 0, rotationEuler);
            obstacleParent.transform.DOLocalRotate(rotation, rotationDuration, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear)
                .SetLink(obstacleParent.gameObject, LinkBehaviour.KillOnDestroy);
        }

        private void RandomizeRotation()
        {
            var random = UnityEngine.Random.Range(0, 360);
            obstacleParent.transform.localEulerAngles = new Vector3(0, 0, random);
        }

        protected virtual void StartMovement()
        {
            Debug.LogError($"Movement not implemented!");
        }

        protected virtual void RandomizeMovement()
        {
            Debug.LogError($"Movement not implemented!");
        }

        private void SetupColors()
        {
            var colorPalette = GameHandler.Instance.GameVisualsHandler.CurrentActiveColorPalette;

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

        protected virtual async UniTaskVoid CheckForDestroy()
        {
            while (true)
            {
                CheckDistanceBetweenPlayer();
                await UniTask.Delay(TimeSpan.FromSeconds(DestroyCheckPeriod), cancellationToken: _token.Token);
            }
        }

        private void CheckDistanceBetweenPlayer()
        {
            if (GameHandler.Instance.PlayerController == null)
            {
                return;
            }
            
            var position = transform.position;
            var playerPosition = GameHandler.Instance.PlayerController.transform.position;
            var distance = Vector3.Distance(position, playerPosition);

            if (playerPosition.y > position.y && distance > DestroyMinDistance)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnValidate()
        {
            if (obstacleParent == null)
            {
                obstacleParent = GetComponentInChildren<ObstacleParent>();
            }

            FillColorElementsLists();
        }

        private void FillColorElementsLists()
        {
            colorElementsAList.Clear();
            colorElementsBList.Clear();
            colorElementsCList.Clear();
            colorElementsDList.Clear();

            foreach (var element in obstacleParent.ColorA.GetComponentsInChildren<ColorElement>())
            {
                colorElementsAList.Add(element);
            }

            foreach (var element in obstacleParent.ColorB.GetComponentsInChildren<ColorElement>())
            {
                colorElementsBList.Add(element);
            }

            foreach (var element in obstacleParent.ColorC.GetComponentsInChildren<ColorElement>())
            {
                colorElementsCList.Add(element);
            }

            foreach (var element in obstacleParent.ColorD.GetComponentsInChildren<ColorElement>())
            {
                colorElementsDList.Add(element);
            }
        }
    }
}
