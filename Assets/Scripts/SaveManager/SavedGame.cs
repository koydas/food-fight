﻿using UnityEngine;

namespace Assets.Scripts.SaveManager
{
    [System.Serializable]
    public class SavedGame
    {
        public int NbOfLevelsUnlocked;
        public int NbOfStars;
        
        public string[] SelectedFoods = new string[6];
    }
}