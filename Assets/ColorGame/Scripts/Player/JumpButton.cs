using UnityEngine;
using UnityEngine.UI;

namespace ColorGame.Scripts.Player
{
    public class JumpButton : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(InputSystemController.InvokeOnJump);
        }
    }
}
