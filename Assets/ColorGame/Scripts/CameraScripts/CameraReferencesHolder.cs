using Cinemachine;
using ColorGame.Scripts.Globals.Patterns;
using UnityEngine;

namespace ColorGame.Scripts.CameraScripts
{
    public class CameraReferencesHolder : Singleton<CameraReferencesHolder>
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private CameraHeightLimiter cameraHeightLimiter;

        public Camera MainCamera => mainCamera;
        public CinemachineVirtualCamera VirtualCamera => virtualCamera;
        public CameraHeightLimiter CameraHeightLimiter => cameraHeightLimiter;
    }
}
