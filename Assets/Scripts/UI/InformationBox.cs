using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    class InformationBox : MonoBehaviour
    {
        public void SetInformations(GameObject food)
        {
            var foodInformations = food.GetComponent<Food.Food>();

            SetTitle(foodInformations.Title);
            SetText(foodInformations.Text);
        }

        private void SetTitle(string title)
        {
            transform.GetChild(0).GetComponent<Text>().text = title;
        }

        private void SetText(string text)
        {
            transform.GetChild(1).GetComponent<Text>().text = text;
        }
    }
}
