using System;
using ColorGame.Scripts.Colors.Globals;
using UnityEngine;

namespace ColorGame.Scripts.Player
{
    public class PlayerEventsHandler : MonoBehaviour
    {
        public event Action OnPlayerPickup;
        public event Action OnPlayerDie;
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameTags.Collectable))
            {
                //TODO: check collectable, change colors / add start amount of stars
                Debug.Log("Collectable!");
                Destroy(other.gameObject);
                OnPlayerPickup?.Invoke();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(GameTags.Obstacle))
            {
                //TODO: end game
                Debug.Log("Obstacle! :(");
                OnPlayerDie?.Invoke();
            }
        }
    }
}
