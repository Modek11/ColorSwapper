using System;

namespace ColorGame.Scripts.Player
{
    public static class InputSystemController
    {
        public static Action OnJumpPerformed;
        public static void InitializeInputSystem()
        {
            var inputSystem = new InputSystem();
            inputSystem.Gameplay.Enable();
            
            inputSystem.Gameplay.Jump.performed += ctx => { InvokeOnJump(); };
        }

        public static void InvokeOnJump()
        {
            OnJumpPerformed?.Invoke();
        }
    }
}
