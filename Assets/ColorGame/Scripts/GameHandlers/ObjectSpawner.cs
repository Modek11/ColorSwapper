using System.Collections.Generic;
using ColorGame.Scripts.InteractableObjects;
using ColorGame.Scripts.InteractableObjects.Collectables;
using ColorGame.Scripts.InteractableObjects.Obstacles.DeathObstacles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ColorGame.Scripts.GameHandlers
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private float spaceBetweenObjects;
        [SerializeField] private ColorChanger colorChanger;
        [SerializeField] private StarCollectable starCollectable;
        
        [Header("Obstacles")]
        [SerializeField] private BottomDeathObstacle bottomDeathObstacle;
        [SerializeField] private SideDeathObstacle sideDeathObstacle;
        [SerializeField] private List<BaseObjectController> obstacles;

        private readonly Vector3 _firstObstaclePosition = new Vector3(0, -1.4f, 0); //half height of first object
        private Vector3 _nextObstacleStartingPosition;
        private float _previousObstacleHeight;

        private void Awake()
        {
            GameHandler.Instance.SetObjectSpawner(this);
            if (GameHandler.Instance.PlayerController != null)
            {
                SpawnObstacles();
            }
            else
            {
                GameHandler.Instance.OnPlayerSpawned += SpawnObstacles;
            }
        }

        private void SpawnObstacles()
        {
            GameHandler.Instance.OnPlayerSpawned -= SpawnObstacles;
            
            SpawnDeathObstacles();
            
            _nextObstacleStartingPosition = _firstObstaclePosition;
            SpawnObstacle(obstacles[0]);
            
            for (var i = 0; i < 10; i++)
            {
                var randomIndex = Random.Range(0, obstacles.Count);
                SpawnObstacle(obstacles[randomIndex]);
            }
            
            GameHandler.Instance.GameVisualsHandler.ChangeCurrentActiveColor();
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

        private void SpawnDeathObstacles()
        {
            var bottom = Instantiate(bottomDeathObstacle, transform, true);
            bottom.transform.position = GetDeathObstaclePosition();

            var left = Instantiate(sideDeathObstacle, transform, true);
            left.transform.position = GetDeathObstaclePosition(left.SideOffset);

            var right = Instantiate(sideDeathObstacle, transform, true);
            right.transform.position = GetDeathObstaclePosition(-left.SideOffset);
        }

        private Vector3 GetDeathObstaclePosition(float sideOffset = 0f)
        {
            return new Vector3(sideOffset, -100, 0);
        }
    }
}
