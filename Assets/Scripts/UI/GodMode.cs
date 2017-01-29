using Assets._3rd_Party.SimpleDragAndDrop.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    class GodMode : MonoBehaviour
    {
        public void UnlockAllFoods()
        {
            bool unlock;
            string newButtonText;

            var button = GameObject.Find("unlock-all button");
            var buttonChild = button.transform.GetChild(0);
            var buttonText = buttonChild.GetComponent<Text>();
            
            if (buttonText.text == "Unlock All")
            {
                unlock = true;
                newButtonText = "Lock All";
            }
            else
            {
                unlock = false;
                newButtonText = "Unlock All";
            }

            var allLockedFoods = FindObjectsOfType<FoodLock>();

            foreach (var lockedFood in allLockedFoods)
            {
                lockedFood.GetComponent<DragAndDropCell>().enabled = unlock;
                lockedFood.transform.GetChild(1).gameObject.SetActive(!unlock);
            }

            buttonText.text = newButtonText;
        }
    }
}
