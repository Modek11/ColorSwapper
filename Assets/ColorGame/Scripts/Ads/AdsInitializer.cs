using UnityEngine;
using UnityEngine.Advertisements;

namespace ColorGame.Scripts.Ads
{
    public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        private const string ANDROID_GAME_ID = "5798725";
        private const string IOS_GAME_ID = "5798724";
        
        [SerializeField] bool _testMode = true;
        private string _gameId;
 
        void Awake()
        {
            InitializeAds();
        }
 
        public void InitializeAds()
        {
#if UNITY_IOS
            _gameId = IOS_GAME_ID;
#elif UNITY_ANDROID
            _gameId = ANDROID_GAME_ID;
#elif UNITY_EDITOR
            _gameId = ANDROID_GAME_ID; //Only for testing the functionality in the Editor
#endif
            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _testMode, this);
            }
        }
        
        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
        }
 
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}