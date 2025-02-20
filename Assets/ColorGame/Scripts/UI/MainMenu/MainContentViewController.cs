using ColorGame.Scripts.GameHandlers;
using UnityEngine;

namespace ColorGame.Scripts.UI.MainMenu
{
    public class MainContentViewController : BaseContentView
    { 
        [Space]
        [SerializeField] private Sprite _avatarSprite;
        [SerializeField] private Sprite _trailSprite;

        public override void Init(PanelType panelType, object customObject = null)
        {
            AvatarSprite = _avatarSprite;
            TrailSprite = _trailSprite;
            ColorPalette = GameHandler.Instance.ColorsHandler.CurrentActiveColorPalette;
            
            base.Init(panelType, customObject);
        }
    }
}