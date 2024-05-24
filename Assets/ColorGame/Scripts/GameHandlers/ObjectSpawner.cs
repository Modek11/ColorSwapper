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
        private Vector3 _lastObstaclePosition;
        private float _previousObstacleHeight;
        private int _firstObstacleIndex = 0;
        
        private void Start()
        {
            _lastObstaclePosition = _firstObstaclePosition;
            SpawnObstacle(obstacles[_firstObstacleIndex]);
            
            for (var i = 0; i < 10; i++)
            {
                var randomIndex = Random.Range(0, obstacles.Count);
                SpawnObstacle(obstacles[randomIndex]);
            }
        }

        private void SpawnObstacle(BaseObjectController objectToSpawn)
        {
            var spawnedObject = Instantiate(objectToSpawn);
            var spawnedColor = Instantiate(colorChanger);

            var halfHeight = spawnedObject.ObstacleHeight / 2;
            var spawnPoint = new Vector3(0, _lastObstaclePosition.y + halfHeight,0);
            spawnedObject.transform.position = spawnPoint;

            var incrementStep = halfHeight + spaceBetweenObjects;
            spawnPoint += new Vector3(0, incrementStep, 0);
            spawnedColor.transform.position = spawnPoint;

            _lastObstaclePosition = spawnPoint + new Vector3(0, spaceBetweenObjects, 0);


        }
    }
}
