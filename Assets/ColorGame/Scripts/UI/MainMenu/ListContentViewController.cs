using ColorGame.Scripts.GameVisuals.Colors;
using UnityEngine;
using UnityEngine.UI;

namespace ColorGame.Scripts.UI.MainMenu
{
    public class ListContentViewController : BaseContentView
    {
        private Image _image;
        private Button _button;

        private object _customObject;

        public void Init(PanelType panelType, object customObject = null)
        {
            _customObject = customObject;
            base.Init(panelType);
            _button.onClick.AddListener(() => OverrideSave(panelType, customObject));
        }

        public override void Init(PanelType panelType)
        {
            Debug.LogError($"Used wrong Init! Try Another overload");
        }

        protected override void InitAvatars(PanelType panelType)
        {
            AvatarSprite = _customObject as Sprite;
            base.InitAvatars(panelType);
        }

        protected override void InitTrails(PanelType panelType)
        {
            TrailColor = (Color)_customObject;
            base.InitTrails(panelType);
        }

        protected override void InitColorPalettes(PanelType panelType)
        {
            ColorPalette = _customObject as ColorPalette;
            base.InitColorPalettes(panelType);
        }

        protected override void TryAssignReferences()
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }
            
            if (_button == null)
            {
                _button = GetComponent<Button>();
            }
            
            base.TryAssignReferences();
        }

        private void OverrideSave(PanelType panelType, object customObject)
        {
            if (customObject == null)
            {
                return;
            }
            
            switch (panelType)
            {
                case PanelType.Avatar:
                    PlayerStorageController.SaveAvatarSprite(customObject as Sprite);
                    break;
                case PanelType.Trails:
                    PlayerStorageController.SaveTrial((Color)customObject);
                    break;
                case PanelType.ColorPalette:
                    PlayerStorageController.SaveColorPalette(customObject as ColorPalette);
                    break;
            }
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}