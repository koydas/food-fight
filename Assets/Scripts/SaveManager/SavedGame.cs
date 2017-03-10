using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SaveManager
{
    [System.Serializable]
    public class SavedGame
    {
        public SavedGame()
        {
            NbOfStars = new Dictionary<int, int>();
        }

        public int MaxLevelCompleted;
        public Dictionary<int, int> NbOfStars;
        
        public string[] SelectedFoods = new string[6];
    }
}
