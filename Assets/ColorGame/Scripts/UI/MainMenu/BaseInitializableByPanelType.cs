using UnityEngine;

namespace ColorGame.Scripts.UI.MainMenu
{
    public abstract class BaseInitializableByPanelType : MonoBehaviour
    {
        public virtual void Init(PanelType panelType, object customObject = null)
        {
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