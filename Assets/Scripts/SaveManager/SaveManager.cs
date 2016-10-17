using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.SaveManager
{
    public static class SaveManager
    {
        public static Dictionary<EnumFile, SavedGame> Saves = new Dictionary<EnumFile, SavedGame>();

        public static SavedGame CurrentSavedGame;

        public static SavedOptions SavedOptions;

        public static void SetCurrentSavedGame(EnumFile enumFile)
        {
            if (Saves.ContainsKey(enumFile))
            {
                CurrentSavedGame = Saves[enumFile];
            }
            else
            {
                CurrentSavedGame = new SavedGame();
// throw new UnityException("enumFile not present in dictionary");
            }
        }

        public static void Save(EnumFile enumFile)
        {
            var bf = new BinaryFormatter();
            var file = File.Create(string.Format("{0}/{1}", Application.persistentDataPath, enumFile));

            var selectedFoods = FoodSelector.SelectedFoods;
            var savedGame = new SavedGame
            {
                SelectedFoods = selectedFoods.Where(x => x != null).Select(x => x.name).ToArray(),
                NbOfStars = 0,
                NbOfLevelsUnlocked = 0
            };

            bf.Serialize(file, savedGame);
            file.Close();
        }
        
        public static void Delete()
        {

        }

        public static void Load(EnumFile enumFile)
        {
            var filePath = string.Format("{0}/{1}", Application.persistentDataPath, enumFile);
            if (File.Exists(filePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(filePath, FileMode.Open);
                var savedData = (SavedGame) bf.Deserialize(file);

                if (Saves.ContainsKey(enumFile))
                {
                    Saves[enumFile] = savedData;
                }
                else
                {
                    Saves.Add(enumFile, savedData);
                }

                file.Close();
            }
        }

        public static void SaveOptions()
        {
            var bf = new BinaryFormatter();
            var file = File.Create(Application.persistentDataPath + "/options");
            
            var savedOptions = new SavedOptions
            {
                MasterSound = VolumeManager.Master,
                SfxSound = VolumeManager.Sfx,
                MusicSound = VolumeManager.Music
            };

            bf.Serialize(file, savedOptions);
            file.Close();
        }
        

        public static void LoadOptions()
        {
            var filePath = Application.persistentDataPath + "/options";
            if (File.Exists(filePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(filePath, FileMode.Open);
                var savedData = (SavedOptions)bf.Deserialize(file);

                SavedOptions = savedData;
                
                file.Close();

                VolumeManager.Master = SavedOptions.MasterSound;
                VolumeManager.Sfx = SavedOptions.SfxSound;
                VolumeManager.Music = SavedOptions.MusicSound;
            }
        }
    }
}
