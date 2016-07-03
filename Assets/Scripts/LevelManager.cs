using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour {

        public void NewGame()
        {
            SceneManager.LoadScene("GamePlay");
        }

        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}
