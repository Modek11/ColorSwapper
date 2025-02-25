using UnityEngine;

namespace ColorGame.Scripts.GameHandlers
{
    public class CurrencyHandler : MonoBehaviour
    {
        private int _currentPoints;

        public int CurrentPoints
        {
            get => _currentPoints;

            private set
            {
                _currentPoints = value;
                //TODO [mt]: in the future this should send some kind of Action to update ex. UI and save
            }
        }

        public void StarCollected()
        {
            //TODO [mt]: probably we can remove those start collecting 
            Debug.Log($"Star collected");
            CurrentPoints++;
        }
    }
}
