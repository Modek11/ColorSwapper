using System.Collections.Generic;
using ColorGame.Scripts.GameHandlers;
using UnityEngine;

namespace ColorGame.Scripts.UI.MainMenu
{
    public class BuyableElementsListsController : BaseInitializableByPanelType
    {
        [SerializeField] private ListContentViewController _listContentViewPrefab;
        [SerializeField] private RectTransform _parentTransform;
        
        [Space]
        [SerializeField] private Sprite _avatarSprite; // TODO [mt]: to remove
        [SerializeField] private Sprite _trailSprite; // TODO [mt]: to remove

        private readonly List<ListContentViewController> _currentActiveElements = new();

        protected override void InitAvatars(PanelType panelType)
        {
            for (var i = 0; i < 5; i++)
            {
                InitViewInternal(panelType, _avatarSprite);
            }
        }

        protected override void InitTrails(PanelType panelType)
        {
            for (var i = 0; i < 5; i++)
            {
                InitViewInternal(panelType, _trailSprite);
            }
        }

        protected override void InitColorPalettes(PanelType panelType)
        { 
            var palettes = GameHandler.Instance.ColorsHandler.AvailableColorPalettes;
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
