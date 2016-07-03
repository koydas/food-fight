using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Canon : MonoBehaviour
    {
        public int Score;

        [SerializeField]
        private Text _scoreDisplay;
        [SerializeField]
        private GameObject _projectile;
        [SerializeField]
        private Vector2 _velocity;

        private Scrollbar _powerBar;
        private Castle _castle;

        // Use this for initialization
        void Start ()
        {
            _powerBar = FindObjectOfType<Scrollbar>();
            _castle = FindObjectOfType<Castle>();
        }
	
        // Update is called once per frame
        private void Update()
        {
            Score = (int) (100 - _castle.Score);
            _scoreDisplay.text = Score.ToString();

            if (Score <= 0)
            {
                SceneManager.LoadScene("Lose");
            }
            else if (Score >= 100)
            {
                SceneManager.LoadScene("Win");
            }
        }

        public void Fire()
        {
            if (_projectile != null && _powerBar != null)
            {
                var projectile = Instantiate(_projectile);

                projectile.transform.GetComponent<Rigidbody2D>().velocity = _velocity * _powerBar.value;
            }
            else
            {
                throw new Exception();
            }

        }
    }
}
