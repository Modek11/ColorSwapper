using System;
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
        [SerializeField] private CurrencyHandler scoreHandler;
        [SerializeField] private PlayerStorageController playerStorageController;
        [SerializeField] private GameOverController gameOverController;

        public event Action OnPlayerSpawned;

        public GameVisualsHandler GameVisualsHandler => gameVisualsHandler;
        public ObjectSpawner ObjectSpawner => objectSpawner;
        public PlayerController PlayerController => playerController;
        public CurrencyHandler ScoreHandler => scoreHandler;
        public PlayerStorageController PlayerStorageController => playerStorageController;
        public GameOverController GameOverController => gameOverController;

        public void SetObjectSpawner(ObjectSpawner spawner)
        {
            objectSpawner = spawner;
        }

        public void InvokeOnPlayerSpawned(PlayerController player)
        {
            playerController = player;
            OnPlayerSpawned?.Invoke();
        }
    }
}
