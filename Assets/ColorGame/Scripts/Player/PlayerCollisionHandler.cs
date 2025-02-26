using ColorGame.Scripts.GameHandlers;
using ColorGame.Scripts.Globals;
using ColorGame.Scripts.InteractableObjects;
using ColorGame.Scripts.InteractableObjects.Obstacles;
using UnityEngine;

namespace ColorGame.Scripts.Player
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Vector3 playerSpawnPosition;

        private void Start()
        {
            playerController.OnPlayerPickup += OnPlayerPickup;
        }
        
        private void OnPlayerPickup(GameObject obj)
        {
            if (obj.CompareTag(GameTags.ColorChanger))
            {
                GameHandler.Instance.GameVisualsHandler.ChangeCurrentActiveColor();
            }

            if (obj.CompareTag(GameTags.Star))
            {
                GameHandler.Instance.ScoreHandler.StarCollected();
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
    }
}
