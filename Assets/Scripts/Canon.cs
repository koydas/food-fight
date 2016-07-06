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
        private float _velocityAjustment;
        [SerializeField]
        private float _minPower = 3.6f;

        private Scrollbar _powerBar;
        private Scrollbar _angleBar;
        private Castle _castle;
        private float _originalAngle;
        private Transform _canonBody;
        // Use this for initialization
        void Start ()
        {
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

            _canonBody = transform.FindChild("Canon-body");
            _originalAngle = _canonBody.eulerAngles.z;

            _castle = FindObjectOfType<Castle>();
        }
	
        // Update is called once per frame
        private void Update()
        {
            SetAngle();
            ScoreDisplay();
            WinOrLoseScreen();
        }

        private void SetAngle()
        {
            float rotateValue = _originalAngle + _angleBar.value * (-90);
            _canonBody.eulerAngles = new Vector3(0,0,rotateValue);
        }

        public void Fire()
        {
            if (_projectile != null && _powerBar != null)
            {
                var projectile = Instantiate(_projectile);
                
                projectile.transform.eulerAngles = new Vector3(0, 0, -_canonBody.eulerAngles.z + 7);
                projectile.transform.GetComponent<Rigidbody2D>().freezeRotation = true;
                
                projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.right * _powerBar.value * _velocityAjustment;
            }
            else
            {
                throw new Exception();
            }

        }
        
        private void ScoreDisplay()
        {
            Score = (int)(100 - _castle.Score);
            _scoreDisplay.text = Score.ToString();
        }

        private void WinOrLoseScreen()
        {
            if (Score <= 0)
            {
                SceneManager.LoadScene("Lose");
            }
            else if (Score >= 100)
            {
                SceneManager.LoadScene("Win");
            }
        }
    }
}
