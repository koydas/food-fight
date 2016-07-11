using System;
using Assets.Scripts.Food;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
        [SerializeField]
        private float _maximumAngle = 70;
        [SerializeField]
        private float _followDistance = 3;
        [SerializeField]
        private float _minFollowPos = 3;
        [SerializeField]
        private bool _ennemy;
        [SerializeField]
        [Range(1,4)]
        private int _difficulty;
        
        private Scrollbar _powerBar;
        private Scrollbar _angleBar;
        private Castle _castle;
        private float _originalAngle;
        private Transform _canonBody;
        private GameObject _currentProjectile;
        private Vector3 _originalCameraPosition;
        private LoadLevel _loadLevel ;
        private bool _cameraInPlace;
        private bool _angleSet;
       
        // Use this for initialization
        void Start ()
        {
            _loadLevel = FindObjectOfType<LoadLevel>();
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
        void Update()
        {
            if (!_loadLevel.IsLoaded) return;

            SetAngle();
            ScoreDisplay();
            WinOrLoseScreen();
            FollowProjectile();
            EnnemyShots();
        }

        private void EnnemyShots()
        {
            if (_ennemy)
            {
                Fire();
            }
        }

        private void FollowProjectile()
        {
            if (!_ennemy)
            {

                if (_currentProjectile != null)
                {
                    var followPosX = _currentProjectile.transform.position.x - _followDistance;

                    if (followPosX >= _minFollowPos && followPosX <= 30)
                    {
                        Camera.main.transform.position = Vector2.right*followPosX;
                    }
                }
                else
                {
                    ReturnToCanon();
                }
            }
        }

        private void ReturnToCanon()
        {
            if (Camera.main.transform.position.x > _originalCameraPosition.x)
            {
                Camera.main.transform.position += Vector3.left*.25f;
            }
            else
            {
                _cameraInPlace = true;
            }
        }

        private void SetAngle()
        {
            if (_angleSet) return;
            
            float rotateValue = _ennemy ? Random.Range(0, -_maximumAngle) : _originalAngle + _angleBar.value * (-_maximumAngle);
            _angleSet = true;
            _canonBody.eulerAngles = new Vector3(0,0,rotateValue);
        }

        public void Fire()
        {
            if (!_loadLevel.IsLoaded) return;

            if (_projectile != null && _currentProjectile == null && _ennemy && _angleSet)
            {
                _currentProjectile = Instantiate(_projectile, _canonBody.position, Quaternion.identity) as GameObject;

                if (_currentProjectile != null)
                {
                    _currentProjectile.transform.eulerAngles = new Vector3(0, 0, _canonBody.eulerAngles.z + 7);
                    _currentProjectile.transform.GetComponent<Rigidbody2D>().freezeRotation = true;

                    //todo rendre la puissance plus précise
                    var puss = _currentProjectile.transform.right*Random.Range(0.3f, -0.8f)*_velocityAjustment;
                    _currentProjectile.GetComponent<Rigidbody2D>().velocity = _currentProjectile.transform.right * Random.Range(0, -1f) * _velocityAjustment;

                    //todo rendre plus generique
                    _currentProjectile.GetComponent<Fraise>().IsLaunched = true;

                    _angleSet = false;
                }

                return;
            }

            if (_projectile != null && _powerBar != null && _currentProjectile == null && _cameraInPlace)
            {
                _currentProjectile = Instantiate(_projectile, _canonBody.position, Quaternion.identity) as GameObject;
                
                if (_currentProjectile != null)
                {
                    _currentProjectile.transform.eulerAngles = new Vector3(0, 0, -_canonBody.eulerAngles.z + 7);
                    _currentProjectile.transform.GetComponent<Rigidbody2D>().freezeRotation = true;

                    _currentProjectile.GetComponent<Rigidbody2D>().velocity = _currentProjectile.transform.right * _powerBar.value * _velocityAjustment;

                    //todo rendre plus generique
                    _currentProjectile.GetComponent<Fraise>().IsLaunched = true;
                    _cameraInPlace = false;
                }
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
