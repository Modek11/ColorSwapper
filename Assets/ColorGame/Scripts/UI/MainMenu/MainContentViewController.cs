using ColorGame.Scripts.GameHandlers;
using UnityEngine;

namespace ColorGame.Scripts.UI.MainMenu
{
    public class MainContentViewController : BaseContentView
    {
        [SerializeField] private Sprite _avatarSprite;

        private void Awake()
        {
            PlayerStorageController.OnSaveUpdated += Init;
        }

        public override void Init(PanelType panelType)
        {
            AvatarSprite = GameHandler.Instance.PlayerStorageController.GetAvatar();
            TrailColor = GameHandler.Instance.PlayerStorageController.GetTrail();
            ColorPalette = GameHandler.Instance.PlayerStorageController.GetColorPalette();
            
            base.Init(panelType);
        }

        private void OnDestroy()
        {
            if (PlayerStorageController != null)
            {
                PlayerStorageController.OnSaveUpdated -= Init;
            }
        }
    }
}