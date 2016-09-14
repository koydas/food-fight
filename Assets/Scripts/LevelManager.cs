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
            SaveManager.SaveManager.SetCurrentSavedGame((EnumFile)enumFileNumber);

            SceneManager.LoadScene("LevelSelector");
        }

	    public void OpenModal(GameObject modal)
	    {
	        var modalBox = Instantiate(modal);

	        if (modalBox != null)
	        {
                modalBox.transform.SetParent(GameObject.Find("Canvas").transform);
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

            var SelectedFoodsWrapper = GameObject.FindGameObjectWithTag(Constant.SelectedFood);
            int nbOfSelectedFoods = SelectedFoodsWrapper.transform.childCount;

            for (int i = 0; i < nbOfSelectedFoods; i++)
            {
                if (SelectedFoodsWrapper.transform.GetChild(i).transform.childCount > 0)
                {
                    var child = SelectedFoodsWrapper.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0);
                    var food = Instantiate(child.gameObject);
                    food.name = child.name;
                    DontDestroyOnLoad(food);
                    
                    FoodSelector.SelectedFoods[i] = food;
                }
            }

            if (FoodSelector.HaveSelectedFood())
            {
                throw new UnityException("No food selected");
            }

            SaveManager.SaveManager.Save(EnumFile.Save1);
            
            SceneManager.LoadScene(string.Format("Level{0}", numberAsString));
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

        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}
