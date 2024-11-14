using ColorGame.Scripts.CameraScripts;
using UnityEngine;

namespace ColorGame.Scripts.InteractableObjects.Obstacles
{
    public class BottomObstacle : MonoBehaviour
    {
        [SerializeField] private float distanceBelowCameraCenter;

        private Transform _cameraHeightLimiterTransform;

        private void Awake()
        {
            _cameraHeightLimiterTransform = CameraReferencesHolder.Instance.CameraHeightLimiter.transform;
        }

        private void FixedUpdate()
        {
            if (_cameraHeightLimiterTransform.position.y > transform.position.y)
            {
                transform.position = _cameraHeightLimiterTransform.position + Vector3.down * distanceBelowCameraCenter;
            }
        }
    }
}
