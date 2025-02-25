using ColorGame.Scripts.GameHandlers;
using ColorGame.Scripts.PlayerStorage;
using UnityEngine;

namespace ColorGame.Scripts.UI.MainMenu
{
    public abstract class BaseInitializableByPanelType : MonoBehaviour
    {
        protected PlayerStorageController PlayerStorageController => GameHandler.Instance != null ? GameHandler.Instance.PlayerStorageController : null;
        
        protected PanelType CurrentPanelType { get; set; }
        
        public virtual void Init(PanelType panelType)
        {
            CurrentPanelType = panelType;
            switch (panelType)
            {
                case PanelType.Avatar:
                    InitAvatars(panelType);
                    break;
                case PanelType.Trails:
                    InitTrails(panelType);
                    break;
                case PanelType.ColorPalette:
                    InitColorPalettes(panelType);
                    break;
            }
        }

        protected abstract void InitAvatars(PanelType panelType);
        protected abstract void InitTrails(PanelType panelType);
        protected abstract void InitColorPalettes(PanelType panelType);
    }
}