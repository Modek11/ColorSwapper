using UnityEngine;
using UnityEngine.UI;

namespace ColorGame.Scripts.UI.MainMenu
{
    
    public class DynamicShopPanel : MonoBehaviour
    {
        [SerializeField] private Text _titleText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private MainContentViewController mainContentViewController;
        [SerializeField] private BuyableElementsListsController _buyableElementsListsController;
        
        public PanelType ActivePanelType { get; private set; }

        public void Init(PanelType panelType)
        {
            ActivePanelType = panelType;
            _closeButton.onClick.AddListener(DisableView);
            _titleText.text = panelType.ToString();
            mainContentViewController.Init(panelType);
            _buyableElementsListsController.Init(panelType);
        }

        private void DisableView()
        {
            ActivePanelType = PanelType.Undefined;
            _closeButton.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        }
    }

    public enum PanelType
    {
        Undefined = -1,
        Avatar = 0,
        Trails = 1,
        ColorPalette = 2,
    }
}
