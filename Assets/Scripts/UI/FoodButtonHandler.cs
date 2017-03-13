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
            int nbOfButtons = transform.parent.childCount;

            var selectedFoods = FoodSelector.SelectedFoods;
            
            for (int i = 1; i < nbOfButtons; i++)
            {
                var foodButton = transform.parent.GetChild(i);
                var selectedFood = selectedFoods[i-1];

                foodButton.GetComponent<FoodButton>().IsActive = false;

                if (selectedFood != null)
                {
                    foodButton.GetComponent<FoodButton>().Food = Instantiate(selectedFood);

                    var pos = foodButton.GetComponent<FoodButton>().Food.gameObject.transform.position;
                    foodButton.GetComponent<FoodButton>().Food.gameObject.transform.position = new Vector3(pos.x, pos.y, -10);
                }
                else
                {
                    foodButton.GetComponent<FoodButton>().Food = null;
                }

                foodButton.transform.GetChild(0).GetComponent<Image>().sprite = null;
                foodButton.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 225, 0);

                if (selectedFood != null && selectedFood.GetComponent<SpriteRenderer>())
                {
                    foodButton.transform.GetChild(0).GetComponent<Image>().sprite = selectedFood.GetComponent<SpriteRenderer>().sprite;
                    foodButton.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 225, 255);
                }
                
                if (i == 1)
                {
                    CurrentFoodActive = foodButton.GetComponent<FoodButton>().Food;
                    foodButton.GetComponent<FoodButton>().IsActive = true;
                }

                //todo
                //Destroy(selectedFood);
            }
        }

        public void Update()
        {
            var currentActive = FindObjectsOfType<FoodButton>().Single(x => x.IsActive);
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
