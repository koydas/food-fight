using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class FoodButtonHandler : MonoBehaviour
    {
        public GameObject CurrentFoodActive;

        public void Start()
        {
            CurrentFoodActive = GameObject.Find("Food Button 1").GetComponent<FoodButton>().Food;
        }

        public void Update()
        {
            var currentActive = GameObject.FindObjectsOfType<FoodButton>().Single(x => x.IsActive);
            CurrentFoodActive = currentActive.GetComponent<FoodButton>().Food;
        }

        public void OnClick(GameObject clickedButton)
        {
            if (clickedButton.GetComponent<FoodButton>().Food)
            {
                var foodButtons = GameObject.FindGameObjectsWithTag(Constant.FoodButton);

                foreach (var foodButton in foodButtons)
                {
                    foodButton.GetComponent<FoodButton>().IsActive = false;
                    foodButton.GetComponent<Image>().color = new Color32(255, 255, 225, 75);
                }

                clickedButton.GetComponent<FoodButton>().IsActive = true;
                clickedButton.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
            }
        }
    }
}
