using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class LevelManager : MonoBehaviour {
		public void GamePlay()
        {
            SceneManager.LoadScene("GamePlay");
        }

        public void LevelSelection()
        {
            SceneManager.LoadScene("LevelSelector");
        }

        public void Level(int number)
        {
            string numberAsString;
            if (number < 0)
            {
                throw new Exception("Number need to be positive and at least 1");
            }
            else if (number < 10)
            {
                numberAsString = string.Format("00{0}", number);
            }
            else if (number < 100)
            {
                numberAsString = string.Format("0{0}", number);
            }
            else
            {
                numberAsString = number.ToString();
            }

            SceneManager.LoadScene(string.Format("Level{0}", numberAsString));
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
            SceneManager.LoadScene("SavedGames");
        }

        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}
