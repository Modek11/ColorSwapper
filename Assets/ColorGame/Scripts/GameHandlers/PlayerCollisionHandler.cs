using ColorGame.Scripts.Player;
using UnityEngine;

namespace ColorGame.Scripts.GameHandlers
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Vector3 playerSpawnPosition;

        private void Start()
        {
            playerController.OnPlayerPickup += OnPlayerPickup;
            playerController.OnPlayerDie += OnPlayerDie;
        }
        
        private void OnPlayerPickup(GameObject obj)
        {
            //TODO: check collectable, change colors / add start amount of stars
            Debug.Log("Collectable!");
            Destroy(obj);
        }

        private void OnPlayerDie(GameObject obj)
        {
            //TODO: end game
            Debug.Log("Obstacle! :(");
        }
    }
}
