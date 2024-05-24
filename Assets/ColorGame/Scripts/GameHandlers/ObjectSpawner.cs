using System.Collections.Generic;
using UnityEngine;

namespace ColorGame.Scripts.GameHandlers
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private float spaceBetweenObjects;
        [SerializeField] private List<BaseObjectController> obstacles;
        [SerializeField] private BaseObjectController colorChanger;

        private readonly Vector3 _firstObstaclePosition = new Vector3(0, -1.4f, 0); //half height of first object
        private Vector3 _nextObstacleStartingPosition;
        private float _previousObstacleHeight;
        
        private void Start()
        {
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
    }
}
