using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PowerFilling : MonoBehaviour {
        private Scrollbar _powerBar;
        private RectTransform _rectTransform;

        public void Start () {

            var scrollBars = FindObjectsOfType<Scrollbar>();

            foreach (var scrollBar in scrollBars)
            {
                if (scrollBar.name == "Power") {
                    _powerBar = scrollBar;
                }
            }

            _rectTransform = GetComponent<RectTransform> ();
        }
	
        public void Update () {
            _rectTransform.sizeDelta = new Vector2 (_rectTransform.sizeDelta.x, _powerBar.value * 157);
        }
    }
}
