using ColorGame.Scripts.Colors.Globals;
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
            if (obj.CompareTag(GameTags.ColorChanger))
            {
                GameHandler.Instance.ColorsHandler.ChangeCurrentActiveColor();
            }

            if (obj.CompareTag(GameTags.Star))
            {
                //TODO: star implementation
            }
            
            Destroy(obj);
        }

        private void OnPlayerDie(GameObject obj)
        {
            //TODO: end game
            Debug.Log("Obstacle! :(");
        }
    }
}
