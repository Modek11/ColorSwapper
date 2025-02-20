using ColorGame.Scripts.Colors;
using UnityEngine;
using UnityEngine.UI;

namespace ColorGame.Scripts.UI.MainMenu
{
    public class ListContentViewController : BaseContentView
    {
        private Image _image;
        private Button _button;

        private object _customObject;

        public override void Init(PanelType panelType, object customObject = null)
        {
            //TODO [mt]: handle clicking on button
            
            _customObject = customObject;
            base.Init(panelType, customObject);
        }
        
        protected override void InitAvatars(PanelType panelType)
        {
            AvatarSprite = _customObject as Sprite;
            base.InitAvatars(panelType);
        }

        protected override void InitTrails(PanelType panelType)
        {
            TrailSprite = _customObject as Sprite;
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
    }
}