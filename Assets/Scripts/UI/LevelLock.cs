using Assets._3rd_Party.SimpleDragAndDrop.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    class LevelLock : MonoBehaviour
    {
        public int Level;
        void Start()
        {
            var maxCompletedLevel = GameStatus.MaxCompletedLevel + 1;
            if (Level > maxCompletedLevel)
            {
                gameObject.GetComponent<Button>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<Button>().enabled = true;
                gameObject.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
}
