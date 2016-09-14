using System.Linq;
using Assets.Scripts.SaveManager;
using Assets._3rd_Party.SimpleDragAndDrop.Scripts;
using UnityEngine;

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
            
    }
}
