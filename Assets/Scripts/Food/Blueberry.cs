using UnityEngine;

namespace Assets.Scripts.Food
{
    public class Blueberry : Food
    {

        public override string Title
        {
            get { return "Blueberry"; }
        }

        public override string Text
        {
            get { return "This delicious blue fruit bounces a few times but try to aim directly for the sous-chefs!"; }
        }
        
        public override Sprite Image
        {
            get { return GetComponent<SpriteRenderer>().sprite; }
        }

        public override EnumFood EnumFood
        {
            get
            {
                return EnumFood.Blueberry;
            }
            set { }
        }
    }
}
