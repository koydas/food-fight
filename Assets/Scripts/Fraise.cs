using UnityEngine;

namespace Assets.Scripts
{
    public class Fraise : MonoBehaviour
    {
        [SerializeField]
        private float _timeBeforeDestroyed;
        private float _startTime;

        // Use this for initialization
        void Start ()
        {
            _startTime = Time.time;
        }
	    
        // Update is called once per frame
        void Update () {

            transform.Rotate(new Vector3(0,0,25));

            if (Time.time - _startTime >= (_timeBeforeDestroyed -2f))
            {
                GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            }

            if (Time.time - _startTime >= _timeBeforeDestroyed)
            {
                Destroy(gameObject);
            }
        }
    }
}
