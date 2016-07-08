using UnityEngine;

namespace Assets.Scripts
{
    public class Fraise : MonoBehaviour
    {
        public bool IsLaunched = false;

        [SerializeField]
        private float _timeBeforeDestroyed;
        [SerializeField]
        private bool _isRotate = true;
        
        private float _startTime;

        // Use this for initialization
        void Start ()
        {
            _startTime = Time.time;
        }
	    
        // Update is called once per frame
        void Update () {

            if (_isRotate)
            {
                transform.Rotate(new Vector3(0, 0, 25));
            }

            if (Time.time - _startTime >= (_timeBeforeDestroyed -2f))
            {
                GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            }

            if (Time.time - _startTime >= _timeBeforeDestroyed)
            {
                Destroy(gameObject);
            }
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (IsLaunched)
            {
                _isRotate = false;
            }
        }
    }
}
