using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraScript : MonoBehaviour
    {
        [Header("Zoom")]
        [SerializeField]
        private float _cameraOverViewSize = 16;
        [SerializeField]
        private float _cameraZoomSpeed = 0.05f;
        [SerializeField]
        private float _timeBeforeCameraZoomIn = 3;

        [Header("Move")]

        [SerializeField]
        private float _maxDistance = 30f;
        [SerializeField]
        private float Speed = 0.01f;

        private Vector2? _mousePosWhenFirstClick = null;
        
        private float _originalCameraSize;
        private float _cameraMouvementPerFrame;
        private GameObject _bg;
        private float _originalCameraPosition;

        void Start()
        {
            _originalCameraSize = UnityEngine.Camera.main.orthographicSize;
            _originalCameraPosition = UnityEngine.Camera.main.transform.position.x;
            _bg = GameObject.Find("bg");

            UnityEngine.Camera.main.orthographicSize = _cameraOverViewSize;

            UnityEngine.Camera.main.transform.position = new Vector3(_bg.transform.position.x, 0, 0);


            float diffSize = _cameraOverViewSize - _originalCameraSize;
            int nbOfFramesRequired = (int)(diffSize / _cameraZoomSpeed);

            _cameraMouvementPerFrame = (UnityEngine.Camera.main.transform.position.x - _originalCameraPosition) / nbOfFramesRequired;

            LoadLevel.IsLoaded = false;
        }

        // Update is called once per frame
        void Update ()
        {
			if (!LoadLevel.IsLoaded)
            {
                Zoom();
                return;
            }
            
            Move();
        }

        private void Zoom()
        {
			if (Time.time >= _timeBeforeCameraZoomIn)
			{
			    bool isSizeGood = false;
			    bool isPositionGood = false;

                if (UnityEngine.Camera.main.orthographicSize > _originalCameraSize)
                {
                    UnityEngine.Camera.main.orthographicSize -= _cameraZoomSpeed;
                }
                else
                {
                    isSizeGood = true;
                }

                if (UnityEngine.Camera.main.transform.position.x > _originalCameraPosition)
                {
                    UnityEngine.Camera.main.transform.position += Vector3.left * _cameraMouvementPerFrame;
                }
                else
                {
                    isPositionGood = true;
                }

			    if (isSizeGood && isPositionGood)
			    {
			        LoadLevel.IsLoaded = true;
			    }
			}
        }

        private void Move()
        {
            if (Input.mousePosition.x < 100)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!_mousePosWhenFirstClick.HasValue)
                {
                    _mousePosWhenFirstClick = Input.mousePosition;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _mousePosWhenFirstClick = null;
            }

            if (_mousePosWhenFirstClick != null)
            {
                var x = _mousePosWhenFirstClick.Value.x - Input.mousePosition.x;

                if (UnityEngine.Camera.main.transform.position.x >= _originalCameraPosition - 1 && UnityEngine.Camera.main.transform.position.x < _maxDistance)
                {
                    UnityEngine.Camera.main.transform.position += new Vector3(x * Speed, 0, 0);
                }
            }
        }

    }
}
