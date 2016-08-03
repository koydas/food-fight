using Assets.Scripts.Food.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Food
{
    public class Pomegranate : Food, ISecondAbility
    {
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
            for (int i = 0; i <= NbOfFragments; i++)
            {
                var frag = Instantiate(Fragment, transform.position, Quaternion.identity) as GameObject;
                frag.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1);
            }
        }
    }
}
