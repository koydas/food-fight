using UnityEngine;
using System.Linq;
using Assets.Scripts.SaveManager;
using UnityEngine.UI;

public class LevelNbStars : MonoBehaviour {
    public int Level;
    public Sprite sprite;

    public void Update()
    {
        var savedGame2 = SaveManager.CurrentSavedGame;
            
            //Saves.FirstOrDefault(x => x.Key == savedGame);

        if (savedGame2 != null)
        {
            var stars = savedGame2.NbOfStars;
            int nbStarsThisLevel = 0;
            if (stars.Any() && stars.ContainsKey(Level))
            {
                nbStarsThisLevel = stars[Level];
            }

            var starsWrapper = gameObject.transform.GetChild(1);

            if (nbStarsThisLevel > 2)
            {
                starsWrapper.GetChild(2).GetComponent<Image>().sprite = sprite;
            }

            if (nbStarsThisLevel > 1)
            {
                starsWrapper.GetChild(1).GetComponent<Image>().sprite = sprite;
            }

            if (nbStarsThisLevel > 0)
            {
                starsWrapper.GetChild(0).GetComponent<Image>().sprite = sprite;
            }
        }
    }
}
