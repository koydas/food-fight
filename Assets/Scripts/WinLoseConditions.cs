using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Canon;
using Assets.Scripts.Characters;
using Assets.Scripts.SaveManager;

public class WinLoseConditions : MonoBehaviour {

	private List<GameObject> ennemySousChefs = new List<GameObject>();
	private List<GameObject> playerSousChefs = new List<GameObject>();

	private LevelManager LevelManager;

	// Use this for initialization
	void Start () {
		LevelManager = GetComponent<LevelManager> ();
	}

	// Update is called once per frame
	void Update () {
	    if (!FindObjectOfType<PlayerCanon>())
	    {
	        return;
	    }

		ennemySousChefs = GetSousChefs (Constant.Ennemy);
		playerSousChefs = GetSousChefs (Constant.Player);
        
		if (IsAllEnnemySousChefsDead ()) {
            var playerCanon = FindObjectOfType<PlayerCanon>();

		    int nbOfStars = 0;

		    if (playerCanon.ShotsCount <= LevelManager.ThreeStars)
		    {
		        nbOfStars = 3;
		    }
            else if (playerCanon.ShotsCount <= LevelManager.TwoStars)
		    {
		        nbOfStars = 2;
		    }
            else if (playerCanon.ShotsCount <= LevelManager.OneStar)
		    {
		        nbOfStars = 1;
		    }

		    if (!SaveManager.CurrentSavedGame.NbOfStars.ContainsKey(FoodSelector.LevelLoaded))
		    {
                SaveManager.CurrentSavedGame.NbOfStars.Add(FoodSelector.LevelLoaded, nbOfStars);
            }
            else if (SaveManager.CurrentSavedGame.NbOfStars[FoodSelector.LevelLoaded] < nbOfStars)
		    {
		        SaveManager.CurrentSavedGame.NbOfStars[FoodSelector.LevelLoaded] = nbOfStars;
		    }

		    LevelManager.WinScreen();
		} 

		if (IsAllPlayerSousChefsDead()) {
			LevelManager.LoseScreen();
		}
	}

	private List<GameObject> GetSousChefs(int layer) {
		var sousChefs = GameObject.FindObjectsOfType<SousChef> ();

		return sousChefs.Select (x => x.gameObject).Where (x => x.layer == layer).ToList ();
	}

	private bool IsAllPlayerSousChefsDead() {
		return !playerSousChefs.Any ();
	}

	private bool IsAllEnnemySousChefsDead() {
		return !ennemySousChefs.Any ();
	}
}
