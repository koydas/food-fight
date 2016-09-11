using UnityEngine;

namespace Assets.Scripts.Food
{
    public class Jello : Food
    {

        public override Sprite Image
        {
            get { return GetComponent<SpriteRenderer>().sprite; }
        }

        public void Start()
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

        public override string Title
        {
            get { return "Jello"; }
        }

        public override string Text
        {
            get { return "Trap a person in jello to prevent them from moving or protect your own with a bouncy shell!"; }
        }

        public override EnumFood EnumFood
        {
            get
            {
                return EnumFood.Jello;
            }
            set { }
        }
    }
}
