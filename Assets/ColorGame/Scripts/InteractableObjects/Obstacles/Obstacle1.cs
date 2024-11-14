using ColorGame.Scripts.GameHandlers;
using ColorGame.Scripts.Patterns;
using UnityEngine;

namespace ColorGame.Scripts.InteractableObjects.Obstacles
{
    public class Obstacle1 : BaseObjectController
    {
        //TODO: Change the name of this obstacle
        
        protected void Awake()
        {
            GameHandler.Instance.ColorsHandler.OnGlobalColorChanged += ChangeActiveColliders;
        }
        
        private void ChangeActiveColliders(Color activeColor)
        {
            foreach (var sameColorObjects in ColorElementsList)
            {
                var shouldBeEnabled = Helper.IsDifferentColorRGB(sameColorObjects[0].SpriteRenderer.color, activeColor);
                foreach (var element in sameColorObjects)
                {
                    element.Collider2D.enabled = shouldBeEnabled;
                }
            }
        }
    }
    
}
