using UnityEngine;
using UnityEngine.UI;

namespace ColorGame.Scripts.UI.MainMenu
{
    public class MainMenuHandler : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _avatarButton;
        [SerializeField] private Button _trailButton;
        [SerializeField] private Button _gameColorsButton;

        [SerializeField] private DynamicShopPanel _dynamicShopPanel;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(() => Debug.Log($"Implement changing scene to gameplay!"));
            _avatarButton.onClick.AddListener(() => OpenDynamicPanel(PanelType.Avatar));
            _trailButton.onClick.AddListener(() => OpenDynamicPanel(PanelType.Trails));
            _gameColorsButton.onClick.AddListener(() => OpenDynamicPanel(PanelType.ColorPalette));
        }

        private void OpenDynamicPanel(PanelType panelType)
        {
            _dynamicShopPanel.gameObject.SetActive(true);
            _dynamicShopPanel.Init(panelType);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveAllListeners();
            _avatarButton.onClick.RemoveAllListeners();
            _trailButton.onClick.RemoveAllListeners();
            _gameColorsButton.onClick.RemoveAllListeners();
        }
    }
}