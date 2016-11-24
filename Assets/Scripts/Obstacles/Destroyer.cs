using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class Destroyer : MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D coll)
        {
            Destroy(coll.gameObject);
        }
    }
}
