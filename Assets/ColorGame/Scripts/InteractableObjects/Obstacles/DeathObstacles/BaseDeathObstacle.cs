using System;
using System.Threading;
using ColorGame.Scripts.CameraScripts;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace ColorGame.Scripts.InteractableObjects.Obstacles.DeathObstacles
{
    public abstract class BaseDeathObstacle : MonoBehaviour
    {
        private const float MOVE_CHECKING_INTERVAL = .3f;

        private CancellationTokenSource _token;
        private Transform _cameraTransform;

        protected abstract float YOffset { get; }
        
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
                await UniTask.Delay(TimeSpan.FromSeconds(MOVE_CHECKING_INTERVAL), cancellationToken: _token.Token);
            }
        }

        private void TryMoveObstacle()
        {
            if (_cameraTransform.position.y > transform.position.y)
            {
                transform.DOMoveY(_cameraTransform.position.y + YOffset, 0);
            }
        }
    }
}
