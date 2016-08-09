using Assets.Scripts.Food.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Food
{
    public class Pomegranate : Food, ISecondAbility
    {
        [SerializeField]
        private bool _secondSkillDestroyObject ;
        public bool SecondSkillDestroyObject
        {
            get { return _secondSkillDestroyObject; }
            set { _secondSkillDestroyObject = value; }

        }

        public GameObject Fragment;
        
        public int NbOfFragments;
        
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
            for (int i = 0; i < NbOfFragments; i++)
            {
                var frag = Instantiate(Fragment, transform.position, Quaternion.identity) as GameObject;

                if (frag != null)
                {
                    var xx = (int) Mathf.Round(Random.Range(0f, 1f));

                    var isPositive = xx != 0;

                    var randX = Random.Range(5f, 7f);
                    var randY = Random.Range(5f, 7f);

                    var velX = isPositive ? randX : -randX;
                    var velY = isPositive ? randY : -randY;

                    frag.GetComponent<Rigidbody2D>().velocity = new Vector2(velX, velY);
                    frag.GetComponent<PomegranateFragment>().IsLaunched = true;
                }
            }
        }
    }
}
