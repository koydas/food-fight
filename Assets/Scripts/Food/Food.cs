using UnityEngine;

namespace Assets.Scripts.Food
{
    public class Food : MonoBehaviour
    {
        public bool IsLaunched = false;

        [SerializeField]
        private float _timeBeforeDestroyed;
        [SerializeField]
        private bool _rotationAllowed = true;
        
        private LoadLevel _loadLevel;

        // Use this for initialization
        void Start ()
        {
            _loadLevel = FindObjectOfType<LoadLevel>();
        }
	    
        // Update is called once per frame
        void Update () {
            if (!_loadLevel.IsLoaded) return;

            if (_rotationAllowed)
            {
                transform.Rotate(new Vector3(0, 0, 25));
            }
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (IsLaunched)
            {
                _rotationAllowed = false;
                Destroy(gameObject);
            }
        }
    }
}
