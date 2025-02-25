using ColorGame.Scripts.Globals;
using ColorGame.Scripts.InteractableObjects;
using ColorGame.Scripts.InteractableObjects.Obstacles;
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
                GameHandler.Instance.GameVisualsHandler.ChangeCurrentActiveColor();
            }

            if (obj.CompareTag(GameTags.Star))
            {
                GameHandler.Instance.CurrencyHandler.StarCollected();
            }

            TryDestroyCollectedObject(obj);
        }

        private void TryDestroyCollectedObject(GameObject obj)
        {
            var obstacleParent = obj.GetComponent<ObstacleParent>();
            if (obstacleParent != null)
            {
                Destroy(obstacleParent.transform.parent.gameObject);
                return;
            }
            
            var baseController = obj.GetComponent<BaseObjectController>();
            if (baseController != null)
            {
                Destroy(baseController.gameObject);
                return;
            }
            
            Debug.LogError($"Collided with object and can't destroy it", obj);
        }

        private void OnPlayerDie(GameObject obj)
        {
            //TODO: end game
            Debug.Log("Obstacle! :(");
        }
    }
}
