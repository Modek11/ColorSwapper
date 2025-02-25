using ColorGame.Scripts.GameVisuals.Colors;
using ColorGame.Scripts.InteractableObjects.Collectables;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ColorGame.Scripts.UI.MainMenu
{
    public class ColorChangerUI : ColorChanger
    {
        public void SetupUIElements(ColorPalette colorPalette)
        {
            var targetScale = transform.GetComponentInParent<RectTransform>().rect.width;
            transform.localScale = Vector3.one * targetScale;

            for (var i = 0; i < ColorElementsList.Count; i++)
            {
                foreach (var element in ColorElementsList[i])
                {
                    if (element is ColorElementUI elementUi)
                    {
                        elementUi.TargetImage.color = colorPalette[i];
                    }
                }
            }
        }

        protected override async UniTaskVoid CheckForDestroy()
        {
            await UniTask.Yield();
        }
    }
}
