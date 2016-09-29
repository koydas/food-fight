using System;
using System.Linq;
using Assets.Scripts.SaveManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class LevelManager : MonoBehaviour
	{
	    public bool? IsForward;
	    public AudioClip ForwardSound;
	    public AudioClip BackSound;

	    public void Start()
	    {
	        DontDestroyOnLoad(gameObject);
	    }

		public void GamePlay()
        {
            SceneManager.LoadScene("GamePlay");
        }

        //todo temporary fix
        public void LevelSelection(int enumFileNumber)
        {
            PlaySound();

            if (enumFileNumber == -1)
			{
				enumFileNumber = FoodSelector.LevelLoaded;
			}

            SaveManager.SaveManager.SetCurrentSavedGame((EnumFile)enumFileNumber);

            SceneManager.LoadScene("LevelSelector");
        }

	    public void OpenModal(GameObject modal)
	    {
            PlaySound();

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
            SetBack();
            PlaySound();
            Destroy(GameObject.Find("Modal"), BackSound.length);
	    }

	    public void Level()
        {
            PlaySound();

            LoadLevel.IsLoaded = false;

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
            
            //todo memory leaks possible
            //foreach (var levelManager in FindObjectsOfType<LevelManager>())
            //{
            //    if (levelManager != this)
            //    {
            //        Destroy(levelManager.gameObject);
            //    }
            //}
            
            SceneManager.LoadScene(string.Format("Level{0}", numberAsString));
        }

		public void NextLevel() {
			LoadFoodSelector (FoodSelector.LevelLoaded+1);
		}

		public void LoadFoodSelector()
		{
            PlaySound();

            LoadFoodSelector(FoodSelector.LevelLoaded);
		}

        public void LoadFoodSelector(int level)
        {
            PlaySound();

            FoodSelector.DestroySelectedFood();
            LoadLevel.IsLoaded = false;
            FoodSelector.LevelLoaded = level;
            SceneManager.LoadScene("FoodSelector");
        }

        public void StartScreen()
        {
            PlaySound();
            
            SceneManager.LoadScene("StartScreen");
		}

		public void OptionScreen()
		{
            PlaySound();
            
            SceneManager.LoadScene("Options");
		}
        
	    public void SavedGamesScreen()
	    {
	        PlaySound();
	        
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

	    public void PlaySound()
	    {
	        if (IsForward == null)
	        {
	            return;
	        }

	        AudioClip sound = IsForward.Value ? ForwardSound : BackSound;
            
            var audioSource = GetComponent<AudioSource>();
            audioSource.clip = sound;
            audioSource.Play();
        }

        public void SetForward()
	    {
	        IsForward = true;
	    }

        public void SetBack()
	    {
	        IsForward = false;
	    }

        public void SetNoSound()
        {
            IsForward = null;
        }

		public void PauseGame() {
			PauseManager.PauseGame();
		}
    }
}
