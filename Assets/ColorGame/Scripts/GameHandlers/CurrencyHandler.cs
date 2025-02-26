using ColorGame.Scripts.PlayerStorage;
using UnityEngine;

namespace ColorGame.Scripts.GameHandlers
{
    public class CurrencyHandler : MonoBehaviour
    {
        public int CurrentScore { get; set; }

        private PlayerStorageController Storage => GameHandler.Instance.PlayerStorageController;

        private void Start()
        {
            GameHandler.Instance.OnPlayerSpawned += Reset;
        }

        private void Reset()
        {
            CurrentScore = 0;
        }

        public void GetCalculatedScores(out int currentScore, out int highestScore)
        {
            highestScore = Storage.GetHighestScore();
            if (CurrentScore > highestScore)
            {
                Storage.SaveHighestScore(CurrentScore);
                currentScore = highestScore = CurrentScore;
            }
            else
            {
                currentScore = CurrentScore;
            }
        }

        public void StarCollected()
        {
            CurrentScore++;
        }

        private void OnDestroy()
        {
            if (GameHandler.Instance != null)
            {
                GameHandler.Instance.OnPlayerSpawned -= Reset;
            }
        }
    }
}
