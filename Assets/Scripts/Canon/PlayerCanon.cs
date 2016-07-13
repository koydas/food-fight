using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Canon
{
    public class PlayerCanon : Canon
    {
        [Header("Camera")]
        [SerializeField]
        private float _followDistance = 3;
        [SerializeField]
        private float _maxCameraFollow = 30;

        private Scrollbar _powerBar;
        private Scrollbar _angleBar;

        private bool _cameraInPlace;
        private Vector3 _originalCameraPosition;

        public void Awake()
        {
            _originalCameraPosition = Camera.main.transform.position;
        }

        public override void Start()
        {
            base.Start();

            var scrollBars = FindObjectsOfType<Scrollbar>();

            foreach (var scrollBar in scrollBars)
            {
                switch (scrollBar.name)
                {
                    //todo mettre dans un enum
                    case "Power":
                        _powerBar = scrollBar;
                        break;
                    case "Angle":
                        _angleBar = scrollBar;
                        break;
                    default:
                        throw new Exception("Unknown ScrollBar");
                }
            }
        }

        public override void Update()
        {
            if (!LoadLevel.IsLoaded) return;

            base.Update();
            
            FollowProjectile();
        }
        
        protected override void SetAngle()
        {
            SetAngle(_angleBar.value);
        }
        
        public override void Fire()
        {
            if (!LoadLevel.IsLoaded) return;

            if (Projectile != null && _powerBar != null && CurrentProjectile == null && _cameraInPlace)
            {
                Fire(_powerBar.value, false);
                _cameraInPlace = false;
            }
        }

        private void FollowProjectile()
        {
            if (CurrentProjectile != null)
            {
                var followPosX = CurrentProjectile.transform.position.x - _followDistance;
                
                if (followPosX >= _originalCameraPosition.x && followPosX <= _maxCameraFollow)
                {
                    Camera.main.transform.position = Vector2.right * followPosX;
                }
            }
            else
            {
                ReturnToCanon();
            }
        }

        private void ReturnToCanon()
        {
            if (Camera.main.transform.position.x > _originalCameraPosition.x)
            {
                Camera.main.transform.position += Vector3.left * .25f;
            }
            else
            {
                _cameraInPlace = true;
            }
        }
    }
}