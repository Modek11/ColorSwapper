using UnityEngine;

namespace ColorGame.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float jumpStrength;
        
        private Rigidbody2D _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            InputSystemController.InitializeInputSystem();
            InputSystemController.OnJumpPerformed += PlayerJump;
        }

        private void PlayerJump()
        {
            var forceVector = new Vector2(0, jumpStrength);
            _rigidbody.AddForce(forceVector, ForceMode2D.Impulse);
        }
    }
}
