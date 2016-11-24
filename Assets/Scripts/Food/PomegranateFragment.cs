using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Food
{
    public class PomegranateFragment : FoodFragment
    {
        public void Start()
        {
            int randomInt = Random.RandomRange(0, SplashSounds.Count);
            SplashSound = SplashSounds[randomInt];
        }

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

        public List<AudioClip> SplashSounds;

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
