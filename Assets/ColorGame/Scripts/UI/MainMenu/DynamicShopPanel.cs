using UnityEngine;
using UnityEngine.UI;

namespace ColorGame.Scripts.UI.MainMenu
{
    
    public class DynamicShopPanel : MonoBehaviour
    {
        [SerializeField] private Text _titleText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private MainDisplayController _mainDisplayController;
        [SerializeField] private RectTransform _contentParent;
        [SerializeField] private GameObject _contentPrefab;

        public void Init(PanelType panelType)
        {
            _closeButton.onClick.AddListener(DisableView);
            _titleText.text = panelType.ToString();
            _mainDisplayController.Init(panelType);
        }

        private void DisableView()
        {
            _closeButton.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        }
    }

    public enum PanelType
    {
        Avatar = 0,
        Trails = 1,
        ColorPalette = 2,
    }
}
