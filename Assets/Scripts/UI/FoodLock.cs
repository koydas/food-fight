using Assets._3rd_Party.SimpleDragAndDrop.Scripts;
using UnityEngine;

namespace Assets.Scripts.UI
{
    class FoodLock : MonoBehaviour
    {
        public int UnlockedLevel;
        void Start()
        {
            if (UnlockedLevel > GameStatus.MaxCompletedLevel + 1)
            {
                gameObject.GetComponent<DragAndDropCell>().enabled = false;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                gameObject.GetComponent<DragAndDropCell>().enabled = true;
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
