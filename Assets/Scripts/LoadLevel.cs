using UnityEngine;

namespace Assets.Scripts
{
    public class LoadLevel : MonoBehaviour
    {
        [SerializeField]
        private float _cameraOverViewSize = 16;
        [SerializeField]
        private float _cameraZoomSpeed = 0.05f;
        [SerializeField]
        private float _timeBeforeCameraZoomIn = 3;
        

        private float _originalCameraSize;
        private float _cameraMouvementPerFrame;
        private GameObject _bg;
        private float _originalCameraPosition;

        // Use this for initialization
        void Start ()
        {
            _originalCameraSize = Camera.main.orthographicSize;
            _originalCameraPosition = Camera.main.transform.position.x;
            _bg = GameObject.Find("bg");

            Camera.main.orthographicSize = _cameraOverViewSize;

            Camera.main.transform.position = new Vector3(_bg.transform.position.x, 0, 0);
            

            float diffSize = _cameraOverViewSize - _originalCameraSize;
            int nbOfFramesRequired = (int)(diffSize/_cameraZoomSpeed);

            _cameraMouvementPerFrame = (Camera.main.transform.position.x - _originalCameraPosition)/ nbOfFramesRequired;
        }
	
        // Update is called once per frame
        void Update ()
        {
            if (Time.time >= _timeBeforeCameraZoomIn && Camera.main.orthographicSize > _originalCameraSize)
            {
                Camera.main.orthographicSize -= _cameraZoomSpeed;
            }
            
            if (Time.time >= _timeBeforeCameraZoomIn && Camera.main.transform.position.x > _originalCameraPosition)
            {
               Camera.main.transform.position += Vector3.left*_cameraMouvementPerFrame;
            }
        }
    }
}
