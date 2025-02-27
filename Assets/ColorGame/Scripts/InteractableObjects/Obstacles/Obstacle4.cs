using DG.Tweening;
using UnityEngine;

namespace ColorGame.Scripts.InteractableObjects.Obstacles
{
    public class Obstacle4 : BaseObjectController
    {
        private float _startValue = 1.5f;
        private float _endValue = -2.5f;

        private Transform ColorA => obstacleParent.ColorA.transform.GetChild(0);
        private Transform ColorB => obstacleParent.ColorB.transform.GetChild(0);
        private Transform ColorC => obstacleParent.ColorC.transform.GetChild(0);
        private Transform ColorD => obstacleParent.ColorD.transform.GetChild(0);
        
        protected override void StartMovement()
        {
            SetMovement(ColorA);
            SetMovement(ColorB);
            SetMovement(ColorC);
            SetMovement(ColorD);
        }

        protected override void RandomizeMovement()
        {
        }

        private void Update()
        {
            TryTeleportColor(ColorA);
            TryTeleportColor(ColorB);
            TryTeleportColor(ColorC);
            TryTeleportColor(ColorD);
        }

        private void TryTeleportColor(Transform colorObject)
        {
            if (CheckColor(colorObject))
            {
                TeleportColor(colorObject);
            }
        }

        private bool CheckColor(Transform colorObject)
        {
            return colorObject.localPosition.x <= _endValue;
        }

        private void TeleportColor(Transform colorObject)
        {
            colorObject.DOKill();
            colorObject.localPosition = new Vector3(_startValue, colorObject.localPosition.y, colorObject.localPosition.z);
            SetMovement(colorObject);
        }

        private void SetMovement(Transform colorObject)
        {
            colorObject.DOLocalMoveX(colorObject.localPosition.x - 1, movementDuration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear)
                .SetLink(obstacleParent.gameObject, LinkBehaviour.KillOnDestroy);
        }
    }
}