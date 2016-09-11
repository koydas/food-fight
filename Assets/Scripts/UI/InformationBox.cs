using System.Globalization;
using Assets.Scripts.Food;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    class InformationBox : MonoBehaviour
    {
        public void Start()
        {
            var blueberry = FindObjectOfType<Blueberry>();

            SetInformations(blueberry.gameObject);
        }

        public void SetInformations(GameObject food)
        {
            var foodInformations = food.GetComponent<Food.Food>();

            SetTitle(foodInformations.Title);
            SetText(foodInformations.Text);
            SetImage(foodInformations.Image);
        }

        private void SetTitle(string title)
        {
            transform.GetChild(0).GetComponent<Text>().text = title;
        }

        private void SetText(string text)
        {
            transform.GetChild(2).GetComponent<Text>().text = text;
        }

        private void SetImage(Sprite image)
        {
            transform.GetChild(1).GetComponent<Image>().sprite = image;
        }
    }
}
