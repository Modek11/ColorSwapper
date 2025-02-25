using System.Collections.Generic;
using ColorGame.Scripts.GameHandlers;
using UnityEngine;

namespace ColorGame.Scripts.UI.MainMenu
{
    public class BuyableElementsListsController : BaseInitializableByPanelType
    {
        [SerializeField] private ListContentViewController _listContentViewPrefab;
        [SerializeField] private RectTransform _parentTransform;

        private readonly List<ListContentViewController> _currentActiveElements = new();

        protected override void InitAvatars(PanelType panelType)
        {
            var avatars = GameHandler.Instance.GameVisualsHandler.AvailableAvatars;
            foreach (var avatar in avatars)
            {
                InitViewInternal(panelType, avatar);
            }
        }

        protected override void InitTrails(PanelType panelType)
        {
            var trails = GameHandler.Instance.GameVisualsHandler.AvailableTrails;
            foreach (var trail in trails)
            {
                InitViewInternal(panelType, trail);
            }
        }

        protected override void InitColorPalettes(PanelType panelType)
        { 
            var palettes = GameHandler.Instance.GameVisualsHandler.AvailableColorPalettes;
            foreach (var palette in palettes)
            {
                InitViewInternal(panelType, palette);
            }
        }

        private void InitViewInternal(PanelType panelType, object customObject = null)
        {
            var view = Instantiate(_listContentViewPrefab, _parentTransform, false);
            view.Init(panelType, customObject);
            _currentActiveElements.Add(view);
        }

        private void OnDisable()
        {
            foreach (var element in _currentActiveElements)
            {
                Destroy(element?.gameObject);
            }
            
            _currentActiveElements.Clear();
        }
    }
}
