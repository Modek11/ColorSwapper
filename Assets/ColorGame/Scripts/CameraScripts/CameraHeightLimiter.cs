using ColorGame.Scripts.Player;
using UnityEngine;

namespace ColorGame.Scripts.CameraScripts
{
    public class CameraHeightLimiter : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private float offset;

        private Transform _playerTransform;
        private Vector3 _targetPosition;

        private void Awake()
        {
            _playerTransform = playerController.transform;
        }

        private void FixedUpdate()
        {
            if (_playerTransform.position.y > transform.position.y)
            {
                OverrideTargetPositionY(_playerTransform.transform.position.y);
            }
            else
            {
                OverrideTargetPositionY(transform.position.y + offset);
            }
        }

        private void LateUpdate()
        {
            transform.position = _targetPosition;
        }

        private void OverrideTargetPositionY(float y)
        {
            var x = transform.position.x;
            var z = transform.position.z;
            _targetPosition = new Vector3(x, y, z);
        }
    }
}
