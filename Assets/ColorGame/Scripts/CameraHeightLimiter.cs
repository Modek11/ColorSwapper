using ColorGame.Scripts.Player;
using UnityEngine;

namespace ColorGame.Scripts
{
    public class CameraHeightLimiter : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;

        private Transform _playerTransform;

        private void Awake()
        {
            _playerTransform = playerController.transform;
        }

        private void LateUpdate()
        {
            if (_playerTransform.position.y > transform.position.y)
            {
                transform.position = _playerTransform.position;
            }
        }
    }
}
