using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class FoodButton : MonoBehaviour
    {
        public void OnClick(GameObject clickedButton)
        {
            if (clickedButton.transform.FindChild("Active food").GetComponent<Image>().sprite)
            {
                var foodButtons = GameObject.FindGameObjectsWithTag(Constant.FoodButton);

                foreach (var foodButton in foodButtons)
                {
                    
                    foodButton.GetComponent<Image>().color = new Color32(255, 255, 225, 75);
                }
                
                clickedButton.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
            }
        }
    }
}
