using Assets.Scripts.Food.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Food
{
    public abstract class Food : MonoBehaviour
    {
        public abstract string Title { get; }
        public abstract string Text { get; }
        public abstract Sprite Image { get; }
        
        public AudioClip SplashSound;
        public AudioClip BounceSound;
        
        public Vector3 OriginalAngle;

        public int Damage = 10;

        public abstract EnumFood EnumFood { get; set; }

        [HideInInspector]
        public bool IsLaunched = false;

        [SerializeField]
        [Range(0,9)]
        private int _nbOfLeaps;
        [SerializeField]
        private float _leapPower = 2.5f;

        public bool RotationAllowed = true;

        // Update is called once per frame
        public void Update () {
			if (!LoadLevel.IsLoaded  || PauseManager.IsPaused) return;

            if (RotationAllowed)
            {
                Rotation();
            }
        }

        protected virtual void Rotation()
        {
            transform.Rotate(new Vector3(0, 0, 25));
        }

        public virtual void OnCollisionEnter2D(Collision2D coll)
        {            
            if (IsLaunched && coll.gameObject.tag != Constant.PlatformLimiter && coll.gameObject.tag != Constant.Fragment)
            {
                float delay = 0;
                //bounce on the floor
                if (coll.gameObject.tag == Constant.Floor)
                {
                    RotationAllowed = false;
                    if (_nbOfLeaps > 0)
                    {
                        //bounce sound
                        var audioSource = GetComponent<AudioSource>();
                        if (audioSource != null)
                        {
                            audioSource.clip = BounceSound;
                            audioSource.volume = VolumeManager.GetSfxVolume();
                            audioSource.Play();
                        }

                        GetComponent<Rigidbody2D>().velocity = new Vector2(_leapPower, _leapPower);
                        _leapPower *= .5f;
                        _nbOfLeaps--;
                    }
                    else
                    {
                        //splash sound
                        if (coll.gameObject.tag != Constant.Bouncy)
                        {
							var audioSource = GetComponent<AudioSource> ();
							if (audioSource != null) {
                                audioSource.clip = SplashSound;
                                audioSource.volume = VolumeManager.GetSfxVolume();
                                audioSource.Play();
							}

                            delay = audioSource != null && audioSource.clip != null ? audioSource.clip.length : 0;

                            Destroy(gameObject, delay);
                        }

                        Destroy(gameObject, delay);
                    }

                    return;
                }
                else if (coll.gameObject.tag == Constant.SousChef)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
