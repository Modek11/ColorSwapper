using System;
using ColorGame.Scripts.Colors.Globals;
using ColorGame.Scripts.GameHandlers;
using UnityEngine;

namespace ColorGame.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer playerSpriteRenderer;
        [SerializeField] private float jumpStrength;
        
        public event Action<GameObject> OnPlayerPickup;
        public event Action<GameObject> OnPlayerDie;
        
        private void Awake()
        {
            GameHandler.Instance.ColorsHandler.OnGlobalColorChanged += OnGlobalColorChanged;
            InputSystemController.InitializeInputSystem();
            InputSystemController.OnJumpPerformed += PlayerJump;
        }

        private void OnGlobalColorChanged(Color color)
        {
            playerSpriteRenderer.color = color;
        }

        private void PlayerJump()
        {
            var forceVector = new Vector2(0, jumpStrength);
            rb.velocity = Vector2.zero;
            rb.AddForce(forceVector, ForceMode2D.Impulse);
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
