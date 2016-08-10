using System;
using Assets.Scripts.Food;
using Assets.Scripts.Food.Interfaces;
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

        private Vector3 _originalCameraPosition;

        public EnumCanonState CanonState;

        private EnumDirection _powerScrollBarDirection = EnumDirection.Up;

        private float _timeBeforeReturnToCanon = 1.5f;
        private float? _timeStart;
        
        public void Awake()
        {
            _originalCameraPosition = UnityEngine.Camera.main.transform.position;
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

            Projectile = FoodHelper.CurrentSelectedFood();

            PowerBarUpAndDown();
            FollowProjectile();
        }
        
        protected override void SetAngle()
        {
            SetAngle(_angleBar.value);
        }

        private void PowerBarUpAndDown()
        {
            if (CanonState != EnumCanonState.PowerbarMoving) return;

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

            switch (CanonState)
            {
                case EnumCanonState.Idle:
                    CanonState = EnumCanonState.PowerbarMoving;
                    break;

                case EnumCanonState.PowerbarMoving:
                    if (Projectile != null && _powerBar != null && CurrentProjectile == null)
                    {
                        Fire(_powerBar.value, false);
                        CanonState = EnumCanonState.Launched;
                    }
                    break;

                case EnumCanonState.Launched:
                    UseSecondAbility();
                    CanonState = EnumCanonState.FollowFragment;
                    break;
            }
        }

        private void UseSecondAbility()
        {
            if (CurrentProjectile == null)
            {
                return;
            }

            var projectile = CurrentProjectile.GetComponent<ISecondAbility>();
            if (projectile != null)
            {
                projectile.UseSecondAbility();
                if (projectile.SecondSkillDestroyObject)
                {
                    Destroy(CurrentProjectile);

                    var fragments = CurrentProjectile.GetComponent<IFragments>();
                    if (fragments != null)
                    {
                        CurrentProjectile = fragments.FragmentToFollow;
                    }
                }
            }
        }
        
        private void FollowProjectile()
        {
            if (CurrentProjectile != null)
            {
                var followPosX = CurrentProjectile.transform.position.x - _followDistance;
                
                if (followPosX >= _originalCameraPosition.x && followPosX <= _maxCameraFollow)
                {
                    UnityEngine.Camera.main.transform.position = Vector2.right * followPosX;
                }
            }
            else if (CanonState == EnumCanonState.FollowFragment || CanonState == EnumCanonState.Launched || CanonState == EnumCanonState.ReturnToCanon)
            {
                if (!_timeStart.HasValue)
                {
                    _timeStart = Time.time;
                }

                if (Time.time > _timeStart.Value + _timeBeforeReturnToCanon)
                {
                    CanonState = EnumCanonState.ReturnToCanon;
                    ReturnToCanon();
                }
            }
        }

        private void ReturnToCanon()
        {
            float ajustmentBuffer = -.2f;

            if (UnityEngine.Camera.main.transform.position.x > _originalCameraPosition.x - ajustmentBuffer)
            {
                UnityEngine.Camera.main.transform.position += Vector3.left * .25f;
            }
            else if (UnityEngine.Camera.main.transform.position.x < _originalCameraPosition.x + ajustmentBuffer)
            {
                UnityEngine.Camera.main.transform.position -= Vector3.left * .25f;
            }
            else
            {
                _timeStart = null;
                CanonState = EnumCanonState.Idle;
            }
        }

    }
}