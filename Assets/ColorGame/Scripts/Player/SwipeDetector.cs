using System;
using UnityEngine;

namespace ColorGame.Scripts.Player
{
    public class SwipeDetector : MonoBehaviour
    {
        private Vector2 _startTouchPosition;
        private Vector2 _endTouchPosition;
        private bool _swipeInProgress = false;

        public event Action<SwipeData> OnSwipeUpdated;
        
    
        private void Update()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    _startTouchPosition = touch.position;
                    _swipeInProgress = true;
                }
                else if (touch.phase == TouchPhase.Ended && _swipeInProgress)
                {
                    _endTouchPosition = touch.position;
                    _swipeInProgress = false;
                    ProcessSwipe();
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                _startTouchPosition = Input.mousePosition;
                _swipeInProgress = true;
            }
            else if (Input.GetMouseButtonUp(0) && _swipeInProgress)
            {
                _endTouchPosition = Input.mousePosition;
                _swipeInProgress = false;
                ProcessSwipe();
            }
        }

        private void ProcessSwipe()
        {
            var swipeVector = _endTouchPosition - _startTouchPosition;
            var swipeLength = swipeVector.magnitude;
            var swipeDirection = swipeVector.normalized;

            var data = new SwipeData(swipeLength, swipeDirection);
            OnSwipeUpdated?.Invoke(data);
        }
    }

    public struct SwipeData
    {
        public float SwipeStrength;
        public Vector2 NormalizedDirection;

        public SwipeData(float swipeStrength, Vector2 normalizedDirection)
        {
            SwipeStrength = swipeStrength;
            NormalizedDirection = normalizedDirection;
        }
    }
}