using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class OptionsFillUp : MonoBehaviour
    {
        public Scrollbar _master;
        public Scrollbar _sfx;
        public Scrollbar _music;

        void Start()
        {
            var scrollbars = FindObjectsOfType<Scrollbar>();
            foreach (var scrollbar in scrollbars)
            {
                switch (scrollbar.name)
                {
                    case "MasterVolume":
                        scrollbar.value = VolumeManager.Master;
                        break;
                    case "SfxVolume":
                        scrollbar.value = VolumeManager.Sfx;
                        break;
                    case "MusicVolume":
                        scrollbar.value = VolumeManager.Music;
                        break;
                }
            }
        }
    }
}
