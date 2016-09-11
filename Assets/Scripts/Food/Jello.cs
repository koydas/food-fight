using UnityEngine;

namespace Assets.Scripts.Food
{
    public class Jello : Food
    {

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
            get { return "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus malesuada nisl neque, at iaculis neque aliquet quis. Proin eu ligula tortor. Aenean congue eu nibh in ullamcorper. Vivamus nulla nibh, bibendum vitae tortor eget, semper tincidunt odio. Maecenas et fermentum tellus, eget dapibus nulla."; }
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
