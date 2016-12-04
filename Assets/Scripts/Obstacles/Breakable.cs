using UnityEngine;

namespace Assets.Scripts.Obstacles
{
    public class Breakable : MonoBehaviour
    {
        public int NumberOfHitToBreak = 3;

        public void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.tag == Constant.Fragment)
            {
                Destroy(coll.gameObject);
                return;
            }

            if (NumberOfHitToBreak <= 0)
            {
                Destroy(gameObject);
            }

            var spriteRenderer = GetComponent<SpriteRenderer>();
            switch (NumberOfHitToBreak)
            {
                case 2:
                    spriteRenderer.color = Color.yellow;

                    break;
                case 1:
                    spriteRenderer.color = Color.red;
                    break;
                case 0:
                    Destroy(gameObject);
                    break;
            }

            NumberOfHitToBreak--;
        }
    }
}
