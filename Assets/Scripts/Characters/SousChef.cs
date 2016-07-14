using Assets.Scripts.Canon;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class SousChef : MonoBehaviour
    {
        [SerializeField]
        private float WalkSpeed = 1f;

        private EnumDirection _currentDirection;

        void Start ()
        {
            _currentDirection = transform.localScale.x < 0 ? EnumDirection.Right : EnumDirection.Left;
        }
	
        void Update ()
        {
            Walk();

        }

        private void Walk()
        {
            var direction = _currentDirection == EnumDirection.Right ? WalkSpeed : -WalkSpeed;
            
            GetComponent<Rigidbody2D>().velocity = Vector2.right * direction;
        }

        public void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.GetComponent<Food.Food>())
            {
                Destroy(gameObject);
            }
        }

        public void OnTriggerEnter2D(Collider2D coll)
        {
            print(coll.gameObject.tag);

            if (coll.gameObject.tag.Equals(Constant.PlatformLimiter))
            {
                var currentScale = transform.localScale;
                transform.localScale = new Vector3(currentScale.x * -1, currentScale.y, currentScale.z);
                _currentDirection = _currentDirection == EnumDirection.Right ? EnumDirection.Left : EnumDirection.Right;
            }
        }
    }
}
