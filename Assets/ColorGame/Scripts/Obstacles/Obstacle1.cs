using ColorGame.Scripts.GameHandlers;
using ColorGame.Scripts.Patterns;
using UnityEngine;

namespace ColorGame.Scripts.Obstacles
{
    public class Obstacle1 : BaseObjectController
    {
        //TODO: Change the name of this obstacle
        
        protected override void Awake()
        {
            base.Awake();
            
            GameHandler.Instance.ColorsHandler.OnGlobalColorChanged += color => ChangeActiveColliders(color);
        }
        
        protected void ChangeActiveColliders(Color activeColor)
        {
            for (var i = 0; i < colorElementsList.Count; i++)
            {
                var shouldBeEnabled = Helper.IsDifferentColorRGB(colorElementsList[i][0].SpriteRenderer.color, activeColor);
                foreach (var element in colorElementsList[i])
                {
                    element.Collider2D.enabled = shouldBeEnabled;
                }
            }
        }
    }
    
}
