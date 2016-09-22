using Assets.Scripts.Food.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class SousChef : MonoBehaviour
    {
        [HideInInspector]
        public bool IsSlowed;

        [SerializeField]
        private float WalkSpeed = 1f;

        [SerializeField]
        public int MaxHealth = 100;

        
        public float CurrentHealth;

        [HideInInspector]
        public float DotDamagePerTick;

        [HideInInspector]
        public int DotNbOfTicks;

        [SerializeField]
        private EnumDirection _currentDirection;

        void Start ()
        {
            if (_currentDirection != EnumDirection.Left && _currentDirection != EnumDirection.Right)
            {
                _currentDirection = transform.localScale.x < 0 ? EnumDirection.Right : EnumDirection.Left;
            }

            CurrentHealth = MaxHealth;
        }
	
        void Update ()
        {
            DotDamage();
            Walk();
            IsDead();
            
        }

        private void DotDamage()
        {
            if (DotNbOfTicks > 0)
            {
                CurrentHealth -= DotDamagePerTick;
                DotNbOfTicks--;
            }
        }

        private void IsDead()
        {
            if (CurrentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void Walk()
        {
            var direction = _currentDirection == EnumDirection.Right ? WalkSpeed : -WalkSpeed;
            
            GetComponent<Rigidbody2D>().velocity = Vector2.right * direction * (IsSlowed ? .5f : 1f);
        }

        public void OnCollisionEnter2D(Collision2D coll)
        {
            var food = coll.gameObject.GetComponent<Food.Food>();
            if (food)
            {
                var fps = 1.0f/Time.deltaTime;

                CurrentHealth -= food.Damage;

                if (food is IDot)
                {
                    var foodDot = food as IDot;

                    DotNbOfTicks = foodDot.DotTimer * (int)fps;
                    DotDamagePerTick = foodDot.DotDamage / ((float)foodDot.DotTimer * fps);
                }
            }
        }

        private void ChangeDirection()
        {
            var currentScale = transform.localScale;
            transform.localScale = new Vector3(currentScale.x * -1, currentScale.y, currentScale.z);
            _currentDirection = _currentDirection == EnumDirection.Right ? EnumDirection.Left : EnumDirection.Right;
        }

        public void OnTriggerStay2D(Collider2D coll)
        {
            if (coll.gameObject.tag.Equals(Constant.PlatformLimiter))
            {
                ChangeDirection();
            }

            var platformSettings = coll.GetComponent<PlatformSettings>();

            if ((coll.gameObject.tag.Equals(Constant.PlatformDropper) || coll.gameObject.tag.Equals(Constant.PlatformLifter)) && (platformSettings.DirectionLimited == EnumDirection.All || platformSettings.DirectionLimited == _currentDirection))
            {
                transform.position = new Vector3(transform.position.x, platformSettings.LifterOrDropperTarget.transform.position.y, transform.position.z);

                if (platformSettings.ChangeDirection)
                {
                    ChangeDirection();
                }
            }
        }
    }
}
