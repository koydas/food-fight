using Assets.Scripts.Obstacles;
using UnityEngine;

namespace Assets.Scripts.Food
{
    public class Coconut : Food
    {

        public override string Title
        {
            get { return "Coconut"; }
        }

        public override string Text
        {
            get { return "Description to write."; }
        }
        
        public override Sprite Image
        {
            get { return GetComponent<SpriteRenderer>().sprite; }
        }

        public override EnumFood EnumFood
        {
            get
            {
                return EnumFood.Coconut;
            }
            set { }
        }

        public override void OnCollisionEnter2D(Collision2D coll)
        {
            base.OnCollisionEnter2D(coll);

            if (coll.gameObject.GetComponent<Breakable>())
            {
                Destroy(coll.gameObject);
            }
        }

        protected override void Rotation()
        {
            transform.Rotate(new Vector3(0, 0, 4.5f));
        }
    }
}
