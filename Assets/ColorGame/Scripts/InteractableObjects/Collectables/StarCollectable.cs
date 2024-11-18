namespace ColorGame.Scripts.InteractableObjects.Collectables
{
    public class StarCollectable : BaseObjectController
    {
        protected override bool ShouldChangeOnGlobalColorChange => false;

        protected override void OnValidate()
        {
        }
    }
}