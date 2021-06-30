using UnityEngine;

namespace Game.Gameplay
{
    public class CameraRotationSetter : MonoBehaviour
    {
        [SerializeField] Vector3 targetRotation;

        private CameraController cameraController;
        
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Init(CameraController cameraController)
        {
            this.cameraController = cameraController;
            gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            cameraController.SetTargetRotation(targetRotation);
        }
    }
}
