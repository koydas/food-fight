using Assets.Scripts.Food.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Food
{
    public class Pomegranate : Food, ISecondAbility, IFragments
    {
        public override string Title
        {
            get { return "Pomegranate"; }
        }

        public override string Text
        {
            get { return "This red fruit reveals its insides with an explosion of flavor when you click the screen!"; }
        }

        public override Sprite Image
        {
            get { return GetComponent<SpriteRenderer>().sprite; }
        }

        public AudioClip ExplosionSound;

        [SerializeField]
        private bool _secondSkillDestroyObject ;
        [SerializeField]
        private GameObject _fragment;
        [SerializeField]
        private int _nbOfFragments;

        public EnumDirection Direction;

        public bool SecondSkillDestroyObject
        {
            get { return _secondSkillDestroyObject; }
            set { _secondSkillDestroyObject = value; }

        }

        public bool SecondSkillRepeatable
        {
            get { return false; }
            set { }
        }

        public GameObject Fragment
        {
            get { return _fragment; }
            set { _fragment = value; }
        }

        public GameObject FragmentToFollow { get; set; }

        public int NbOfFragments
        {
            get { return _nbOfFragments; }
            set { _nbOfFragments = value; }
        }

        public override EnumFood EnumFood
        {
            get
            {
                return EnumFood.Pomegranate;
            }
            set { }
        }


        public void UseSecondAbility()
        {
            // Explosion sound
            var audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.clip = ExplosionSound;
                audioSource.volume = VolumeManager.GetSfxVolume();
                audioSource.Play();
            }

            var yTotal = 10;
            var yDiff = yTotal / (float)NbOfFragments;
            var yStart = 10;

            for (int i = 0; i < NbOfFragments; i++)
            {
                float yVel = yStart - yDiff*i;
                var frag = Instantiate(Fragment, transform.position, Quaternion.identity) as GameObject;

                frag.layer = gameObject.layer;

                if (frag != null)
                {
                    if (FragmentToFollow == null)
                    {
                        FragmentToFollow = frag;
                    }

                    float xVel = 10;

                    switch (Direction)
                    {
                            case EnumDirection.Left:
                            xVel *= -1;
                            break;
                        case EnumDirection.Up:
                            xVel = yVel;
                            yVel = 10;
                            break;
                        case EnumDirection.Down:
                            xVel = yVel;
                            yVel = -10;
                            break;
                    }
                    
                    frag.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
                    frag.GetComponent<PomegranateFragment>().IsLaunched = true;
                }
            }
        }

        public override void OnCollisionEnter2D(Collision2D coll)
        {
            base.OnCollisionEnter2D(coll);

            if (coll.gameObject.tag == Constant.Bouncy)
            {
                Direction = Direction == EnumDirection.Right ? EnumDirection.Left : EnumDirection.Right;
            }

            if (coll.gameObject.tag == Constant.Sticky)
            {
                if (coll.contacts[0].normal.x == -1)
                {
                    Direction = EnumDirection.Right;
                }
                else if (coll.contacts[0].normal.x == 1)
                {
                    Direction = EnumDirection.Left;
                }
                else if (coll.contacts[0].normal.y == -1)
                {
                    Direction = EnumDirection.Up;
                }
                else if (coll.contacts[0].normal.y == 1)
                {
                    Direction = EnumDirection.Down;
                }
            }
        }
    }
}
