using ColorGame.Scripts.Patterns;
using UnityEngine;

namespace ColorGame.Scripts.GameHandlers
{
    public class GameHandler : Singleton<GameHandler>
    {
        [SerializeField] private ColorsHandler colorsHandler;
        [SerializeField] private ObjectSpawner objectSpawner;

        public ColorsHandler ColorsHandler => colorsHandler;
        public ObjectSpawner ObjectSpawner => objectSpawner;
    }
}
