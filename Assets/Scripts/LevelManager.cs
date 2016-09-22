using System;
using Assets.Scripts.SaveManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class LevelManager : MonoBehaviour {
		public void GamePlay()
        {
            SceneManager.LoadScene("GamePlay");
        }

        //todo temporary fix
        public void LevelSelection(int enumFileNumber)
        {
			if (enumFileNumber == -1)
			{
				enumFileNumber = FoodSelector.LevelLoaded;
			}

            SaveManager.SaveManager.SetCurrentSavedGame((EnumFile)enumFileNumber);

            SceneManager.LoadScene("LevelSelector");
        }

	    public void OpenModal(GameObject modal)
	    {
	        if (!FoodSelector.HaveSelectedFood())
	        {
                return;
	        }


	        var modalBox = Instantiate(modal);

	        if (modalBox != null)
	        {
                modalBox.transform.SetParent(GameObject.Find("Canvas").transform, false);
                modalBox.transform.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
	            modalBox.gameObject.name = "Modal";
	        }
        }

	    public void CloseModal()
	    {
            Destroy(GameObject.Find("Modal"));
	    }

	    public void Level()
        {
            string numberAsString;
            if (FoodSelector.LevelLoaded < 0)
            {
                throw new Exception("Number need to be positive and at least 1");
            }
            else if (FoodSelector.LevelLoaded < 10)
            {
                numberAsString = string.Format("00{0}", FoodSelector.LevelLoaded);
            }
            else if (FoodSelector.LevelLoaded < 100)
            {
                numberAsString = string.Format("0{0}", FoodSelector.LevelLoaded);
            }
            else
            {
                numberAsString = FoodSelector.LevelLoaded.ToString();
            }

            SaveManager.SaveManager.Save(EnumFile.Save1);
            
            SceneManager.LoadScene(string.Format("Level{0}", numberAsString));
        }

		public void NextLevel() {
			LoadFoodSelector (FoodSelector.LevelLoaded+1);
		}

        public void LoadFoodSelector(int level)
        {
            FoodSelector.DestroySelectedFood();
            LoadLevel.IsLoaded = false;
            FoodSelector.LevelLoaded = level;
            SceneManager.LoadScene("FoodSelector");
        }

        public void StartScreen()
        {
            SceneManager.LoadScene("StartScreen");
		}

		public void OptionScreen()
		{
			SceneManager.LoadScene("Options");
		}

        public void SavedGamesScreen()
        {
            SaveManager.SaveManager.Load(EnumFile.Save1);

            SceneManager.LoadScene("SavedGames");
        }

		public void WinScreen()
		{
			SceneManager.LoadScene("Win");
		}

		public void LoseScreen()
		{
			SceneManager.LoadScene("Lose");
		}

        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}
