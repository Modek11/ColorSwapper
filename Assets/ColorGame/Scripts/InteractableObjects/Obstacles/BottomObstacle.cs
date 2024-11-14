using System;
using System.Threading;
using ColorGame.Scripts.CameraScripts;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace ColorGame.Scripts.InteractableObjects.Obstacles
{
    public class BottomObstacle : MonoBehaviour
    {
        [SerializeField] private float distanceBelowCameraCenter;
        [SerializeField] private float movingCheckInterval;

        private Transform _cameraTransform;
        private CancellationTokenSource _token;

        
        private void Awake()
        {
            _token = new CancellationTokenSource();
            
            _cameraTransform = CameraReferencesHolder.Instance.VirtualCamera.transform;
            ObstacleMoveLoop().Forget();
        }

        private void OnDestroy()
        {
            _token.Cancel();
            _token.Dispose();
        }

        private async UniTaskVoid ObstacleMoveLoop()
        {
            while (true)
            {
                TryMoveObstacle();
                await UniTask.Delay(TimeSpan.FromSeconds(movingCheckInterval), cancellationToken: _token.Token);
            }
        }

        private void TryMoveObstacle()
        {
            if (_cameraTransform.position.y > transform.position.y)
            {
                transform.DOMoveY(_cameraTransform.position.y - distanceBelowCameraCenter, 0);
            }
        }
    }
}
