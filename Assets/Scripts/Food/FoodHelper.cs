using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Food
{
    public static class FoodHelper
    {
        public static GameObject CurrentSelectedFood()
        {
            var foodButtonHandler = Object.FindObjectOfType<FoodButtonHandler>();
            if (foodButtonHandler != null && foodButtonHandler.CurrentFoodActive != null)
            {
                return foodButtonHandler.CurrentFoodActive;
            }

            throw new UnityException("No Current food selected");
        }
    }
}
