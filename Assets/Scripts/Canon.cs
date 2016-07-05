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
        private Vector2 _velocityAjustment;
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
                
                //todo gérer l'angle
                var x = _minPower + (_powerBar.value*_velocityAjustment.x);
                var y = x * _angleBar.value;
                
                projectile.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(x, _velocityAjustment.y);
            }
            else
            {
                throw new Exception();
            }

        }


        /*
        
            todo try to adapt this code

            if (Input.GetMouseButton(0))
                {
                    //get where user is tapping
                    Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    location.z = 0;
                    //we will let the user pull the bird up to a maximum distance
                    if (Vector3.Distance(location, SlingshotMiddleVector) > 1.5f)
                    {
                        //basic vector maths :)
                        var maxPosition = (location - SlingshotMiddleVector).normalized * 1.5f + SlingshotMiddleVector;
                        BirdToThrow.transform.position = maxPosition;
                    }
                    else
                    {
                        BirdToThrow.transform.position = location;
                    }
                    float distance = Vector3.Distance(SlingshotMiddleVector, BirdToThrow.transform.position);
                    //display projected trajectory based on the distance
                    DisplayTrajectoryLineRenderer2(distance);
                }
                else//user has removed the tap 
                {
                    SetTrajectoryLineRenderesActive(false);
                    //throw the bird!!!
                    TimeSinceThrown = Time.time;
                    float distance = Vector3.Distance(SlingshotMiddleVector, BirdToThrow.transform.position);
                    if (distance > 1)
                    {
                        SetSlingshotLineRenderersActive(false);
                        slingshotState = SlingshotState.BirdFlying;
                        ThrowBird(distance);
                    }
                    else//not pulled long enough, so reinitiate it
                    {
                        //distance/10 was found with trial and error :)
                        //animate the bird to the wait position
                        BirdToThrow.transform.positionTo(distance / 10, //duration
                            BirdWaitPosition.transform.position). //final position
                            setOnCompleteHandler((x) =>
                        {
                            x.complete();
                            x.destroy();
                            InitializeBird();
                        });

            
            */


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
