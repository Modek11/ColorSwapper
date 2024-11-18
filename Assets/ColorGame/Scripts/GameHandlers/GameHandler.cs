using ColorGame.Scripts.Globals.Patterns;
using ColorGame.Scripts.Player;
using UnityEngine;

namespace ColorGame.Scripts.GameHandlers
{
    public class GameHandler : Singleton<GameHandler>
    {
        [SerializeField] private ColorsHandler colorsHandler;
        [SerializeField] private ObjectSpawner objectSpawner;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private CurrencyHandler currencyHandler;

        public ColorsHandler ColorsHandler => colorsHandler;
        public ObjectSpawner ObjectSpawner => objectSpawner;
        public PlayerController PlayerController => playerController;
        public CurrencyHandler CurrencyHandler => currencyHandler;
    }
}
