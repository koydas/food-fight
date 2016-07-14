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

        private bool _fireButtonPushedOnce;
        private EnumDirection _powerScrollBarDirection = EnumDirection.Up;

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

            PowerBarUpAndDown();
            FollowProjectile();
        }
        
        protected override void SetAngle()
        {
            SetAngle(_angleBar.value);
        }

        private void PowerBarUpAndDown()
        {
            if (!_fireButtonPushedOnce) return;

            var speed = .025f;

            if (_powerScrollBarDirection == EnumDirection.Up)
            {
                _powerBar.value += speed;
            }
            else if (_powerScrollBarDirection == EnumDirection.Down)
            {
                _powerBar.value -= speed;
            }

            if (_powerBar.value == 0)
            {
                _powerScrollBarDirection = EnumDirection.Up;
            }
            else if (_powerBar.value == 1)
            {
                _powerScrollBarDirection = EnumDirection.Down;

            }
        }

        public override void Fire()
        {
            if (!LoadLevel.IsLoaded) return;

            if (!_fireButtonPushedOnce)
            {

                _fireButtonPushedOnce = true;
                return;
            }

            if (Projectile != null && _powerBar != null && CurrentProjectile == null && _cameraInPlace)
            {
                Fire(_powerBar.value, false);
                _cameraInPlace = false;
                ResetPowerBar();
            }
        }

        private void ResetPowerBar()
        {
            _fireButtonPushedOnce = false;
            _powerBar.value = 0;
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