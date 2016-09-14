using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public static class FoodSelector
    {
        public static int LevelLoaded = 0;
        public static GameObject[] SelectedFoods = new GameObject[6];

        public static bool HaveSelectedFood()
        {
            return SelectedFoods.All(x => x == null);
        }

        public static void DestroySelectedFood()
        {
            SelectedFoods = new GameObject[6];
        }
    }
}
