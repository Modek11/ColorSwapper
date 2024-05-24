using ColorGame.Scripts.Patterns;
using UnityEngine;

namespace ColorGame.Scripts.GameHandlers
{
    public class GameHandler : Singleton<GameHandler>
    {
        [SerializeField] private ColorsHandler colorsHandler;
        [SerializeField] private PlayerCollisionHandler playerCollisionHandler;
        [SerializeField] private ObjectSpawner objectSpawner;

        public ColorsHandler ColorsHandler => colorsHandler;
        public PlayerCollisionHandler PlayerCollisionHandler => playerCollisionHandler;
        public ObjectSpawner ObjectSpawner => objectSpawner;

    }
}
