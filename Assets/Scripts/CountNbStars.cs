using System.Linq;
using UnityEngine;
using Assets.Scripts.SaveManager;
using UnityEngine.UI;

public class CountNbStars : MonoBehaviour
{
    public EnumFile savedGame;

	public void Update ()
	{
	    var savedGame2 = SaveManager.Saves.FirstOrDefault(x => x.Key == savedGame);

	    if (savedGame2.Value != null)
	    {
            var stars = savedGame2.Value.NbOfStars;
            gameObject.GetComponent<Text>().text = stars.Sum(x => x.Value).ToString();
        }
	}
}
