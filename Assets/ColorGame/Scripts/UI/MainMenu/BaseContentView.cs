using ColorGame.Scripts.Colors;
using UnityEngine;
using UnityEngine.UI;

namespace ColorGame.Scripts.UI.MainMenu
{
    public abstract class BaseContentView : BaseInitializableByPanelType
    {
        protected ColorChangerUI _colorChangerUI;
        protected Image _imageDisplay;
        
        public Sprite AvatarSprite { get; set; }
        public Sprite TrailSprite { get; set; }
        public ColorPalette ColorPalette { get; set; }
        
        public override void Init(PanelType panelType, object customObject = null)
        {
            TryAssignReferences();
            ChangeGameObjectActivity(panelType);
            base.Init(panelType, customObject);
        }

        protected override void InitAvatars(PanelType panelType)
        {
            _imageDisplay.sprite = AvatarSprite;
        }

        protected override void InitTrails(PanelType panelType)
        {
            _imageDisplay.sprite = TrailSprite;
        }

        protected override void InitColorPalettes(PanelType panelType)
        {
            _colorChangerUI.SetupUIElements(ColorPalette);
        }

        protected virtual void TryAssignReferences()
        {
            if (_colorChangerUI == null)
            {
                _colorChangerUI = transform.GetChild(0).GetComponent<ColorChangerUI>();
            }
            
            if (_imageDisplay == null)
            {
                _imageDisplay = transform.GetChild(1).GetComponent<Image>();
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