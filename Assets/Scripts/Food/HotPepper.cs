using System;
using Assets.Scripts.Food.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Food
{
    public class HotPepper : Food, ISecondAbility, IDot, IVelocityModifier
    {
        public override string Title
        {
            get { return "Hot Pepper"; }
        }

        public override string Text
        {
            get { return "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus malesuada nisl neque, at iaculis neque aliquet quis. Proin eu ligula tortor. Aenean congue eu nibh in ullamcorper. Vivamus nulla nibh, bibendum vitae tortor eget, semper tincidunt odio. Maecenas et fermentum tellus, eget dapibus nulla."; }
        }

        [SerializeField]
        private bool _secondSkillDestroyObject;

        [SerializeField]
        private int _dotDamage;

        [SerializeField]
        private int _dotTimer;

        [SerializeField]
        private float _velocityModifier;

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

        public float VelocityModifier
        {
            get { return _velocityModifier; }
            set { _velocityModifier = value; }
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
            transform.Rotate(new Vector3(0, 0, 4.5f));
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
