using Assets.Scripts.Food;
using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class Sticky : MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.GetComponent<FoodFragment>())
            {
                return;
            }

            var rigidbody = coll.gameObject.GetComponent<Rigidbody2D>();
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

            var food = coll.gameObject.GetComponent<Food.Food>();
            if (food)
            {
                food.RotationAllowed = false;
            }

            Destroy(coll.gameObject, 2.0f);
        }
    }
}