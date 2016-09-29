using System;
using Assets.Scripts.SaveManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class PauseManager : MonoBehaviour
	{
		public static bool IsPaused;

		public static void PauseGame() {
		    if (!LoadLevel.IsLoaded)
		    {
		        return;
		    }

		    Time.timeScale = IsPaused ? 1 : 0;
			IsPaused = !IsPaused;
		}
	}
}
