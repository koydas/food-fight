using UnityEngine;

namespace Assets.Scripts.Food
{
    public class PomegranateFragment : FoodFragment
    {
        public override string Title
        {
            get { return null; }
        }

        public override string Text
        {
            get { return null; }
        }

        public override Sprite Image
        {
            get { return null; }
        }

        public override EnumFood EnumFood
        {
            get
            {
                return EnumFood.Pomegranate;
            }
            set { }
        }

        
    }
}
