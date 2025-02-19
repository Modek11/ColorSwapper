using ColorGame.Scripts.Colors;
using ColorGame.Scripts.InteractableObjects.Collectables;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ColorGame.Scripts.UI.MainMenu
{
    public class ColorChangerUI : ColorChanger
    {
        private void SetupUIElements(ColorPalette colorPalette)
        {
            var targetScale = transform.GetComponentInParent<RectTransform>().rect.width;
            transform.localScale = Vector3.one * targetScale;

            foreach (var list in ColorElementsList)
            {
                foreach (var element in list)
                {
                    if (element is ColorElementUI elementUi)
                    {
                        elementUi.TargetImage.sprite = elementUi.SpriteRenderer.sprite;
                        elementUi.TargetImage.color = elementUi.SpriteRenderer.color;
                    }
                }
            }
        }

        protected override async UniTaskVoid CheckForDestroy()
        {
        }
    }
}
