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
            if (Level > GameStatus.MaxCompletedLevel+1)
            {
                gameObject.GetComponent<Button>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<Button>().enabled = true;
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
