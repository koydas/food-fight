using System;
using Assets.Scripts.Food.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Food
{
    public class HotPepper : Food, ISecondAbility, IDot
    {
        [SerializeField]
        private bool _secondSkillDestroyObject;

        [SerializeField]
        private int _dotDamage;

        [SerializeField]
        private int _dotTimer;
        
        public int DotDamage
        {
            get { return _dotDamage; }
            set { _dotDamage = value; }
        }

        public int DotTimer
        {
            get { return _dotTimer; }
            set { _dotTimer = value; }
        }


        public bool SecondSkillDestroyObject
        {
            get { return _secondSkillDestroyObject; }
            set { _secondSkillDestroyObject = value; }
        }

        public override EnumFood EnumFood
        {
            get
            {
                return EnumFood.HotPepper;
            }
            set { }
        }

        protected override void Rotation()
        {
            transform.Rotate(new Vector3(0, 0, 7));
        }
        
        public void UseSecondAbility()
        {
            var tip = transform.GetChild(0);
            var leaves = transform.GetChild(1);

            var diffPos = tip.position - leaves.position;

            RotationAllowed = false;
            GetComponent<Rigidbody2D>().velocity = diffPos * 30;
        }

    }
}
