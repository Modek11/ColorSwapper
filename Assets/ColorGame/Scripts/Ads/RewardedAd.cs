using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace ColorGame.Scripts.Ads
{
    public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string ANDROID_AD_UNIT_ID = "Rewarded_Android";
        private const string IOS_AD_UNIT_ID = "Rewarded_iOS";
       
        [SerializeField] Button _showAdButton;
#if UNITY_EDITOR
        [SerializeField] Button _loadAdButton;
#endif
        
        string _adUnitId = null;
 
        void Awake()
        {   
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? IOS_AD_UNIT_ID
                : ANDROID_AD_UNIT_ID;

            _showAdButton.interactable = false;
#if UNITY_EDITOR
            _loadAdButton.onClick.AddListener(LoadAd);
#endif
        }
 
        public void LoadAd()
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }
 
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Ad Loaded: " + adUnitId);
            
            if (adUnitId.Equals(_adUnitId))
            {
                _showAdButton.onClick.AddListener(ShowAd);
                _showAdButton.interactable = true;
            }
        }
 
        public void ShowAd()
        {
            _showAdButton.interactable = false;
            RemoveListeners();
            Advertisement.Show(_adUnitId, this);
        }
 
        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Debug.Log("Unity Ads Rewarded Ad Completed");
                // Grant a reward.
            }
        }
 
        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.
        }
 
        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.
        }
 
        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { }
 
        void OnDestroy()
        {
            RemoveListeners();
        }

        private void RemoveListeners()
        {
            _showAdButton.onClick.RemoveAllListeners();
        }
    }
}