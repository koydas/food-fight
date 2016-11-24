using Assets.Scripts.Food.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Food
{
    public class Chicken : Food, ISecondAbility, IVelocityModifier
    {
        private int _secondSkillNbOfRepetitions = 0;
        private bool _secondSkillRepeatable = true;

        public override string Title
        {
            get { return "Chicken"; }
        }

        public override string Text
        {
            get { return "Even roasted this chicken seems a bit alive, click the screen mid-flight to have it flap its wings!"; }
        }
        
        public override Sprite Image
        {
            get { return GetComponent<SpriteRenderer>().sprite; }
        }

        public override EnumFood EnumFood
        {
            get
            {
                return EnumFood.Chicken;
            }
            set { }
        }

        protected override void Rotation()
        {
            transform.Rotate(new Vector3(0, 0, -.8f));
        }

        public bool SecondSkillDestroyObject
        {
            get { return false; }
            set { }
        }

        public bool SecondSkillRepeatable
        {
            get
            {
                return _secondSkillRepeatable;
                
            }
            set { _secondSkillRepeatable = value; }
        }


        public void UseSecondAbility()
        {
            if (_secondSkillNbOfRepetitions >= 3)
            {
                SecondSkillRepeatable = false;
                return;
            }

            var currentVelocity = GetComponent<Rigidbody2D>().velocity;
            
            GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocity.x, 5);
            transform.localEulerAngles = OriginalAngle;
            _secondSkillNbOfRepetitions++;
        }

        public float VelocityModifier
        {
            get { return -.3f; }
            set { }
        }
    }
}
