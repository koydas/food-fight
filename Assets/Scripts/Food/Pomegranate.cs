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
            var yTotal = 10;
            var yDiff = yTotal / (float)NbOfFragments;
            var yStart = 10;

            for (int i = 0; i < NbOfFragments; i++)
            {
                var yVel = yStart - yDiff*i;
                var frag = Instantiate(Fragment, transform.position, Quaternion.identity) as GameObject;

                frag.layer = gameObject.layer;

                if (frag != null)
                {
                    if (FragmentToFollow == null)
                    {
                        FragmentToFollow = frag;
                    }

                    var xVel = 10;
                    if (Direction == EnumDirection.Left)
                    {
                        xVel *= -1;
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
        }
    }
}
