using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class LevelManager : MonoBehaviour {
		public void GamePlay()
        {
            SceneManager.LoadScene("GamePlay");
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
