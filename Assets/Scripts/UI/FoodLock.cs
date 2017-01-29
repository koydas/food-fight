using Assets._3rd_Party.SimpleDragAndDrop.Scripts;
using UnityEngine;

namespace Assets.Scripts.UI
{
    class FoodLock : MonoBehaviour
    {
        void Start()
        {
            // TODO check for unlocked levels to see if the food is unlocked
            gameObject.GetComponent<DragAndDropCell>().enabled = false;
        }
    }
}
