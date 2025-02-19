using UnityEngine;
using UnityEngine.UI;

namespace ColorGame.Scripts.UI.MainMenu
{
    public class MainDisplayController : MonoBehaviour
    {
        [SerializeField] private ColorChangerUI _colorChangerUI;
        [SerializeField] private Image _imageDisplay;
        
        [Space]
        [SerializeField] private Sprite _avatarSprite;
        [SerializeField] private Sprite _trailSprite;

        public void Init(PanelType panelType)
        {
            ChangeGameObjectActivity(panelType);
            
            if (panelType == PanelType.Avatar)
            {
                _imageDisplay.sprite = _avatarSprite;
            }
            else if (panelType == PanelType.Trails)
            {
                _imageDisplay.sprite = _trailSprite;
            }
            else if (panelType == PanelType.ColorPalette)
            {
                _colorChangerUI.SetupColors();
            }
        }

        private void ChangeGameObjectActivity(PanelType panelType)
        {
            var isColorPalette = panelType == PanelType.ColorPalette;
            _colorChangerUI.gameObject.SetActive(isColorPalette);
            _imageDisplay.gameObject.SetActive(!isColorPalette);
        }
    }
}