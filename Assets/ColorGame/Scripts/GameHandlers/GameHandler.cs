using ColorGame.Scripts.Globals.Patterns;
using ColorGame.Scripts.Player;
using ColorGame.Scripts.PlayerStorage;
using UnityEngine;

namespace ColorGame.Scripts.GameHandlers
{
    public class GameHandler : Singleton<GameHandler>
    {
        [SerializeField] private GameVisualsHandler gameVisualsHandler;
        [SerializeField] private ObjectSpawner objectSpawner;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private CurrencyHandler currencyHandler;
        [SerializeField] private PlayerStorageController playerStorageController;

        public GameVisualsHandler GameVisualsHandler => gameVisualsHandler;
        public ObjectSpawner ObjectSpawner => objectSpawner;
        public PlayerController PlayerController => playerController;
        public CurrencyHandler CurrencyHandler => currencyHandler;
        public PlayerStorageController PlayerStorageController => playerStorageController;

        public void SetObjectSpawner(ObjectSpawner spawner)
        {
            objectSpawner = spawner;
        }

        public void SetPlayerController(PlayerController player)
        {
            playerController = player;
        }
    }
}
