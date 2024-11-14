using System.Collections.Generic;
using ColorGame.Scripts.InteractableObjects;
using ColorGame.Scripts.InteractableObjects.Collectables;
using ColorGame.Scripts.InteractableObjects.Obstacles;
using UnityEngine;

namespace ColorGame.Scripts.GameHandlers
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private float spaceBetweenObjects;
        [SerializeField] private ColorChanger colorChanger;
        [SerializeField] private StarCollectable starCollectable;
        
        [Header("Obstacles")]
        [SerializeField] private BottomObstacle bottomObstacle;
        [SerializeField] private List<BaseObjectController> obstacles;

        private readonly Vector3 _firstObstaclePosition = new Vector3(0, -1.4f, 0); //half height of first object
        private Vector3 _nextObstacleStartingPosition;
        private float _previousObstacleHeight;
        
        private void Start()
        {
            SpawnBottomObstacle();
            
            _nextObstacleStartingPosition = _firstObstaclePosition;
            SpawnObstacle(obstacles[0]);
            
            for (var i = 0; i < 10; i++)
            {
                var randomIndex = Random.Range(0, obstacles.Count);
                SpawnObstacle(obstacles[randomIndex]);
            }
            
            GameHandler.Instance.ColorsHandler.ChangeCurrentActiveColor();
        }

        private void SpawnObstacle(BaseObjectController objectToSpawn)
        {
            var spawnedObject = Instantiate(objectToSpawn);
            var halfHeight = spawnedObject.ObstacleHeight / 2;
            _nextObstacleStartingPosition.y += halfHeight;
            spawnedObject.transform.position = _nextObstacleStartingPosition;
            
            var spawnedColor = Instantiate(colorChanger);
            var incrementStep = halfHeight + spaceBetweenObjects;
            _nextObstacleStartingPosition.y += incrementStep;
            spawnedColor.transform.position = _nextObstacleStartingPosition;

            _nextObstacleStartingPosition.y += spaceBetweenObjects;
            
            spawnedObject.transform.SetParent(transform);
            spawnedColor.transform.SetParent(transform);
        }

        private void SpawnBottomObstacle()
        {
            var spawnedObject = Instantiate(bottomObstacle, Vector3.down * 100, Quaternion.identity);
            spawnedObject.transform.SetParent(transform);
        }
    }
}
