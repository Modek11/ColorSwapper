using System;
using ColorGame.Scripts.Colors.Globals;
using UnityEngine;

namespace ColorGame.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float jumpStrength;
        
        private Rigidbody2D _rigidbody;
        
        public event Action<GameObject> OnPlayerPickup;
        public event Action<GameObject> OnPlayerDie;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            InputSystemController.InitializeInputSystem();
            InputSystemController.OnJumpPerformed += PlayerJump;
        }

        private void PlayerJump()
        {
            var forceVector = new Vector2(0, jumpStrength);
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(forceVector, ForceMode2D.Impulse);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameTags.Collectable))
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
    }
}
