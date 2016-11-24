using System;
using Assets.Scripts.SaveManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class PauseManager : MonoBehaviour
	{
        private static bool _ispaused;
		public static bool IsPaused {
            get
            {
                return _ispaused;
            }

            set
            { 
                _ispaused = value;

                if (value)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
            }
        }
        
		public static void TogglePause() {
		    if (!LoadLevel.IsLoaded)
		    {
		        return;
		    }

		    Time.timeScale = IsPaused ? 1 : 0;
			IsPaused = !IsPaused;
		}
	}
}
