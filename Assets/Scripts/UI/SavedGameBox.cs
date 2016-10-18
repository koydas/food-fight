using System.Linq;
using Assets.Scripts.SaveManager;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class SavedGameBox : MonoBehaviour
    {
        public EnumFile EnumFile = EnumFile.None;
        private SavedGame _savedGame;
	
        // Update is called once per frame
        void Update ()
        {
            var savedGames = SaveManager.SaveManager.Saves;

            if (EnumFile != EnumFile.None && savedGames.ContainsKey(EnumFile))
            {
                _savedGame = savedGames.Single(x => x.Key == EnumFile).Value;
            }
            else
            {
                _savedGame = null;
            }

            if (HasSavedData())
            {
                SetBoxInformations();
            }
            else
            {
                SetNewGame();
            }
        }

        private bool HasSavedData()
        {
            if (_savedGame == null)
            {
                return false;
            }

            return true;
        }

        private void SetBoxInformations()
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(true);
        }

        private void SetNewGame()
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);
            transform.GetChild(4).gameObject.SetActive(false);
        }
    }
}
