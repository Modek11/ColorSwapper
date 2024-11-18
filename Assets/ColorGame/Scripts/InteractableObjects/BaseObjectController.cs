using System;
using System.Collections.Generic;
using System.Threading;
using ColorGame.Scripts.Colors;
using ColorGame.Scripts.GameHandlers;
using ColorGame.Scripts.InteractableObjects.Obstacles;
using ColorGame.Scripts.Patterns;
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
        
        [SerializeField] private ObstacleParent obstacleParent;
        [SerializeField] protected bool randomizeRotation;
        [SerializeField] protected bool enableRotation;
        [SerializeField] protected bool invertRotationRandomly;
        [SerializeField] protected float rotationDuration;
        
        [SerializeField, HideInInspector] protected List<ColorElement> colorElementsAList = new();
        [SerializeField, HideInInspector] protected List<ColorElement> colorElementsBList = new();
        [SerializeField, HideInInspector] protected List<ColorElement> colorElementsCList = new();
        [SerializeField, HideInInspector] protected List<ColorElement> colorElementsDList = new();

        private List<List<ColorElement>> _colorElementsList;
        private CancellationTokenSource _token;

        protected virtual bool ShouldChangeOnGlobalColorChange { get; } = true;
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

        public float ObstacleHeight => obstacleParent.ObstacleHeight;
        
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
