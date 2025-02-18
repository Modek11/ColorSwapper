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
        [SerializeField] private float jumpStrength;
        [SerializeField] private Vector2 jumpForceMultiplier;
        
        public event Action<GameObject> OnPlayerPickup;
        public event Action<GameObject> OnPlayerDie;
        
        private void Awake()
        {
            GameHandler.Instance.ColorsHandler.OnGlobalColorChanged += OnGlobalColorChanged;
            _swipeDetector.OnSwipeUpdated += OnSwipeUpdated;
        }

        private void OnGlobalColorChanged(Color color)
        {
            playerSpriteRenderer.color = color;
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
                GameHandler.Instance.ColorsHandler.OnGlobalColorChanged -= OnGlobalColorChanged;
            }
        }
    }
}
