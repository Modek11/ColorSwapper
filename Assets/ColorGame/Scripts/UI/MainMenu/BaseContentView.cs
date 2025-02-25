using ColorGame.Scripts.GameHandlers;
using ColorGame.Scripts.GameVisuals.Colors;
using UnityEngine;
using UnityEngine.UI;

namespace ColorGame.Scripts.UI.MainMenu
{
    public abstract class BaseContentView : BaseInitializableByPanelType
    {
        protected ColorChangerUI _colorChangerUI;
        protected Image _imageDisplay;
        
        public Sprite AvatarSprite { get; set; }
        public Color TrailColor { get; set; }
        public ColorPalette ColorPalette { get; set; }
        
        public override void Init(PanelType panelType)
        {
            TryAssignReferences();
            ChangeGameObjectActivity(panelType);
            base.Init(panelType);
        }

        protected override void InitAvatars(PanelType panelType)
        {
            _imageDisplay.sprite = AvatarSprite;
            _imageDisplay.color = GameHandler.Instance.GameVisualsHandler.BaseAvatarColor;
        }

        protected override void InitTrails(PanelType panelType)
        {
            _imageDisplay.sprite = GameHandler.Instance.GameVisualsHandler.TrailPreviewSprite;
            _imageDisplay.color = TrailColor;
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