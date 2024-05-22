using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ColorGame.Scripts
{
    public class ButtonDebugger : MonoBehaviour
    {
        [SerializeField] private string textToDisplay;
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => Debug.Log(textToDisplay));
        }
    }
}
