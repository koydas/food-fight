using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerFilling : MonoBehaviour {
	private Scrollbar _powerBar;
	private RectTransform _rectTransform;

	void Start () {

		var scrollBars = FindObjectsOfType<Scrollbar>();

		foreach (var scrollBar in scrollBars)
		{
			if (scrollBar.name == "Power") {
				_powerBar = scrollBar;
			}
		}

		_rectTransform = GetComponent<RectTransform> ();
	}
	
	void Update () {
		_rectTransform.sizeDelta = new Vector2 (_rectTransform.sizeDelta.x, _powerBar.value * 157);
	}
}
