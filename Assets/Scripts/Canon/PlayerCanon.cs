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
            float rangeAngle = MinimumAngle - MaximumAngle;
            float rotateValue = MinimumAngle + (rangeAngle * _angleBar.value);
            
            CanonBody.eulerAngles = new Vector3(0, 0, rotateValue);
        }

        //todo revoir pour mettre ça dans la classe de base
        public override void Fire()
        {
            if (!LoadLevel.IsLoaded) return;

            if (Projectile != null && _powerBar != null && CurrentProjectile == null && _cameraInPlace)
            {
                CurrentProjectile = Instantiate(Projectile, new Vector3(CanonBody.position.x, CanonBody.position.y, Projectile.transform.position.z), Quaternion.identity) as GameObject;

                if (CurrentProjectile != null)
                {
                    CurrentProjectile.transform.eulerAngles = new Vector3(0, 0, -CanonBody.eulerAngles.z + 7);
                    CurrentProjectile.transform.GetComponent<Rigidbody2D>().freezeRotation = true;

                    float rangePower = MaximumPower - MinimumPower;
                    float powerVelocity = MinimumPower + (rangePower * _powerBar.value);

                    CurrentProjectile.GetComponent<Rigidbody2D>().velocity = CurrentProjectile.transform.right * powerVelocity;

                    //todo rendre plus generique
                    CurrentProjectile.GetComponent<Food.Food>().IsLaunched = true;
                    _cameraInPlace = false;
                }
            }
        }

        private void FollowProjectile()
        {
            if (CurrentProjectile != null)
            {
                var followPosX = CurrentProjectile.transform.position.x - _followDistance;

                //todo possible bogue de la position originale
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