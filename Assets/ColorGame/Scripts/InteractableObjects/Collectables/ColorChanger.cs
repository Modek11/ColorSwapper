namespace ColorGame.Scripts.InteractableObjects.Collectables
{
    public class ColorChanger : BaseObjectController
    {
        private void Awake()
        {
            base.SetupColors();
        }
        
        protected override void SetupColors()
        {
        }
    }
}