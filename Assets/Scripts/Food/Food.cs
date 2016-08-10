using UnityEngine;

namespace Assets.Scripts.Food
{
    public abstract class Food : MonoBehaviour
    {
        public int Damage = 10;

        public abstract EnumFood EnumFood { get; set; }

        [HideInInspector]
        public bool IsLaunched = false;

        [SerializeField]
        [Range(0,9)]
        private int _nbOfLeaps;
        [SerializeField]
        private float _leapPower = 2.5f;

        [SerializeField]
        protected bool RotationAllowed = true;
        
        private LoadLevel _loadLevel;

        // Use this for initialization
        void Start ()
        {
            _loadLevel = FindObjectOfType<LoadLevel>();
        }
	    
        // Update is called once per frame
        void Update () {
            if (!_loadLevel.IsLoaded) return;

            if (RotationAllowed)
            {
                Rotation();
            }
        }

        protected virtual void Rotation()
        {
            transform.Rotate(new Vector3(0, 0, 25));
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (IsLaunched && coll.gameObject.tag != Constant.PlatformLimiter && !coll.gameObject.GetComponent<Food>())
            {
                //bounce on the floor
                if (coll.gameObject.tag == Constant.Floor)
                {
                    RotationAllowed = false;
                    if (_nbOfLeaps > 0)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(_leapPower, _leapPower);
                        _leapPower *= .5f;
                        _nbOfLeaps--;
                    }
                    else
                    {
                        Destroy(gameObject);
                    }

                    return;
                }

                RotationAllowed = false;
                Destroy(gameObject);
            }
        }
    }
}
