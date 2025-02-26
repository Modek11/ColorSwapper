using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ColorGame.Scripts.GameHandlers
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private Transform screenTransform;
        [SerializeField] private Text currentScoreText;
        [SerializeField] private Text highestScoreText;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button backToMenuButton;

        private GameHandler GameHandler => GameHandler.Instance;
        
        private void Awake()
        {
            screenTransform.gameObject.SetActive(false);
            playAgainButton.onClick.AddListener(() => SceneManager.LoadScene(1));
            backToMenuButton.onClick.AddListener(() => SceneManager.LoadScene(0));
            
            if (GameHandler.PlayerController == null)
            {
                GameHandler.OnPlayerSpawned += ListenToPlayerDieEvent;
            }
            else
            {
                GameHandler.PlayerController.OnPlayerDie += ShowGameOverScreen;
            }
        }

        private void OnDestroy()
        {
            if (GameHandler != null)
            {
                GameHandler.OnPlayerSpawned -= ListenToPlayerDieEvent;
                
                if (GameHandler.PlayerController != null)
                {
                    GameHandler.PlayerController.OnPlayerDie -= ShowGameOverScreen;
                }
            }
            
            playAgainButton.onClick.RemoveAllListeners();
            backToMenuButton.onClick.RemoveAllListeners();
        }

        private void ListenToPlayerDieEvent()
        {
            GameHandler.OnPlayerSpawned -= ListenToPlayerDieEvent;
            GameHandler.PlayerController.OnPlayerDie += ShowGameOverScreen;
        }

        private void ShowGameOverScreen(GameObject _)
        {
            Destroy(GameHandler.PlayerController.gameObject);
            
            AssignScoreValues();
            
            var endPosition = screenTransform.position;
            var startPosition = endPosition + (1000 * Vector3.down);
            screenTransform.position = startPosition;
            screenTransform.gameObject.SetActive(true);

            screenTransform.DOMove(endPosition, 1).SetEase(Ease.Flash).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        private void AssignScoreValues()
        {
            GameHandler.Instance.ScoreHandler.GetCalculatedScores(out var currentScore, out var highestScore);
            currentScoreText.text = currentScore.ToString();
            highestScoreText.text = highestScore.ToString();
        }
    }
}
