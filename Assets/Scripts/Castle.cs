using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Castle : MonoBehaviour
    {
        public float Score = 50;

        [SerializeField]
        private Text _scoreDisplay;

        private float _lastSecond = 0f;

        void Start()
        {
            _scoreDisplay.text = Score.ToString();
        }

        void Update()
        {
            if (!LoadLevel.IsLoaded) return;

            if (Time.time >= _lastSecond + 1)
            {
                _lastSecond = Time.time;
                Score += 1;
                _scoreDisplay.text = Score.ToString();
            }
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            Destroy(coll.gameObject);
            Score -= 5;
        }
    }
}
