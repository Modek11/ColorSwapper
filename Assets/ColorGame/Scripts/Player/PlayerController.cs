using System;
using ColorGame.Scripts.GameHandlers;
using ColorGame.Scripts.Globals;
using UnityEngine;

namespace ColorGame.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private const float MAX_SWIPE_LENGTH = 1000f;
        private const float MIN_SWIPE_LENGTH = 10f;
        
        [SerializeField] private Rigidbody2D playerRigidbody;
        [SerializeField] private SwipeDetector _swipeDetector;
        [SerializeField] private SpriteRenderer playerSpriteRenderer;
        [SerializeField] private TrailRenderer playerTrailRenderer;
        [SerializeField] private float jumpStrength;
        [SerializeField] private Vector2 jumpForceMultiplier;
        
        public event Action<GameObject> OnPlayerPickup;
        public event Action<GameObject> OnPlayerDie;

        private void Awake()
        {
            GameHandler.Instance.InvokeOnPlayerSpawned(this);
            GameHandler.Instance.GameVisualsHandler.OnGlobalColorChanged += OnGlobalGameVisualChanged;
            _swipeDetector.OnSwipeUpdated += OnSwipeUpdated;
            SetupPlayerVisuals();
        }

        private void OnGlobalGameVisualChanged(Color color)
        {
            playerSpriteRenderer.color = color;
        }

        private void SetupPlayerVisuals()
        {
            OnGlobalGameVisualChanged(GameHandler.Instance.GameVisualsHandler.CurrentActiveColor);
            playerSpriteRenderer.sprite = GameHandler.Instance.PlayerStorageController.GetAvatar();
            playerTrailRenderer.startColor = GameHandler.Instance.PlayerStorageController.GetStartTrailColor();
            playerTrailRenderer.endColor = GameHandler.Instance.PlayerStorageController.GetEndTrailColor();
        }
        
        private void OnSwipeUpdated(SwipeData swipeData)
        {
            if (swipeData.SwipeStrength < MIN_SWIPE_LENGTH)
            {
                return;
            }

            var strength = swipeData.SwipeStrength > MAX_SWIPE_LENGTH
                ? jumpStrength
                : jumpStrength * (swipeData.SwipeStrength / MAX_SWIPE_LENGTH);

            var forceVector = jumpForceMultiplier * swipeData.NormalizedDirection * strength;
            
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(forceVector, ForceMode2D.Impulse);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameTags.ColorChanger) || other.CompareTag(GameTags.Star))
            {
                OnPlayerPickup?.Invoke(other.gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(GameTags.Obstacle))
            {
                OnPlayerDie?.Invoke(other.gameObject);
            }
        }

        private void OnDestroy()
        {
            if (GameHandler.Instance != null)
            {
                GameHandler.Instance.GameVisualsHandler.OnGlobalColorChanged -= OnGlobalGameVisualChanged;
            }
        }
    }
}
