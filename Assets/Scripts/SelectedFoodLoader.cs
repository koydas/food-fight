using System.Linq;
using Assets.Scripts.SaveManager;
using Assets._3rd_Party.SimpleDragAndDrop.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class SelectedFoodLoader : MonoBehaviour
    {
        void Start()
        {
            var selectedFoods = GameObject.Find("Selected Foods");

            if (SaveManager.SaveManager.CurrentSavedGame.SelectedFoods.Any(x => x != null))
            {
                int i = 0;
                foreach (var selectedFoodName in SaveManager.SaveManager.CurrentSavedGame.SelectedFoods)
                {
                    var availaibleCell = GameObject.Find(selectedFoodName).GetComponentInParent<DragAndDropItem>();

                    availaibleCell.transform.parent = selectedFoods.transform.GetChild(i);
                    availaibleCell.transform.position = selectedFoods.transform.GetChild(i).position;
                    
                    i++;
                }
            }
        }

        void Update()
        {
            ShowPlayButton();
            FillFoodSelector();
        }


        private void ShowPlayButton()
        {
            var items = GameObject.Find("Selected Foods").GetComponentsInChildren<DragAndDropItem>();
            
            GameObject.Find("Play").GetComponent<Button>().interactable = items.Any();
        }

        private void FillFoodSelector()
        {
           // var items = GameObject.Find("Selected Foods").GetComponentsInChildren<Food.Food>();

            var selectedFoodsWrapper = GameObject.FindGameObjectWithTag(Constant.SelectedFood);
            int nbOfSelectedFoods = selectedFoodsWrapper.transform.childCount;

            for (int i = 0; i < nbOfSelectedFoods; i++)
            {
                if (selectedFoodsWrapper.transform.GetChild(i).transform.childCount > 0)
                {
                    var child = selectedFoodsWrapper.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0);
                    var food = Instantiate(child.gameObject);
                    food.name = child.name;
                    DontDestroyOnLoad(food);

                    FoodSelector.SelectedFoods[i] = food;
                }
            }

        //    FoodSelector.SelectedFoods = items.Select(x => x.gameObject).ToArray();
        }
    }
}
