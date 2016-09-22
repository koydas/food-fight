using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Characters;

public class WinLoseConditions : MonoBehaviour {

	private List<GameObject> ennemySousChefs = new List<GameObject>();
	private List<GameObject> playerSousChefs = new List<GameObject>();

	private LevelManager LevelManager;

	// Use this for initialization
	void Start () {
		LevelManager = GameObject.FindObjectOfType<LevelManager> () as LevelManager;
	}

	// Update is called once per frame
	void Update () {
		ennemySousChefs = GetSousChefs (Constant.Ennemy);
		playerSousChefs = GetSousChefs (Constant.Player);

		//print (ennemySousChefs);

		if (IsAllEnnemySousChefsDead ()) {
			print ("Win");
		} 

		if (IsAllPlayerSousChefsDead()) {
			print ("Lose");
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
