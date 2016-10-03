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

					if (availaibleCell != null) {
						availaibleCell.transform.SetParent (selectedFoods.transform.GetChild (i));
						availaibleCell.transform.position = selectedFoods.transform.GetChild(i).position;
					}
                                        
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
            var find = GameObject.Find("Selected Foods");
            if (find != null)
            {
                var items = find.GetComponentsInChildren<DragAndDropItem>();
                GameObject.Find("Play").GetComponent<Button>().interactable = items.Any();
            }
        }

		//todo temporary fix. Only call when Level will be loaded.
        private void FillFoodSelector()
        {
            var selectedFoodsWrapper = GameObject.FindGameObjectWithTag(Constant.SelectedFood);

            if (selectedFoodsWrapper != null)
            {
                int nbOfSelectedFoods = selectedFoodsWrapper.transform.childCount;

                var dontDestroyOnLoadItems = GameObject.FindGameObjectsWithTag(Constant.DontDestroyOnLoad);

                foreach (var obj in dontDestroyOnLoadItems)
                {
                    Destroy(obj);
                }

                for (int i = 0; i < nbOfSelectedFoods; i++)
                {
                    if (selectedFoodsWrapper.transform.GetChild(i).transform.childCount > 0)
                    {
                        var child = selectedFoodsWrapper.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0);
                        var food = Instantiate(child.gameObject);
                        food.name = child.name;
                        food.tag = Constant.DontDestroyOnLoad;

                        DontDestroyOnLoad(food);

                        FoodSelector.SelectedFoods[i] = food;
                    }
                }
            }
        }
    }
}
