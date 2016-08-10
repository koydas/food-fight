using Assets.Scripts.Food.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Food
{
    public class Pomegranate : Food, ISecondAbility, IFragments
    {
        [SerializeField]
        private bool _secondSkillDestroyObject ;
        [SerializeField]
        private GameObject _fragment;
        [SerializeField]
        private int _nbOfFragments;

        public bool SecondSkillDestroyObject
        {
            get { return _secondSkillDestroyObject; }
            set { _secondSkillDestroyObject = value; }

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
            var yTotal = 15;
            var yDiff = yTotal / (float)NbOfFragments;
            var yStart = 10;

            for (int i = 0; i < NbOfFragments; i++)
            {
                var yVel = yStart - yDiff*i;
                var frag = Instantiate(Fragment, transform.position, Quaternion.identity) as GameObject;

                if (frag != null)
                {
                    if (FragmentToFollow == null)
                    {
                        FragmentToFollow = frag;
                    }

                    frag.GetComponent<Rigidbody2D>().velocity = new Vector2(10, yVel);
                    frag.GetComponent<PomegranateFragment>().IsLaunched = true;
                }
            }
        }

        
    }
}
