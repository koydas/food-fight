using System.Collections.Generic;
using Assets.Scripts.Food.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Food
{
    public class Blowfish : Food, ISecondAbility, IFragments
    {
        [SerializeField]
        private GameObject _fragment;

        private int _nbOfFragments = 20;

        public override string Title
        {
            get { return "Blowfish"; }
        }

        public override string Text
        {
            get { return "This fish will blow your opponent away! Watch out for its spikes, they sting!"; }
        }
        
        public override Sprite Image
        {
            get { return GetComponent<SpriteRenderer>().sprite; }
        }

        public override EnumFood EnumFood
        {
            get
            {
                return EnumFood.Blowfish;
            }
            set { }
        }

        protected override void Rotation()
        {
            transform.Rotate(new Vector3(0, 0, -.8f));
        }

        public float VelocityModifier
        {
            get { return -.3f; }
            set { }
        }

        public bool SecondSkillDestroyObject
        {
            get { return true; }
            set { }
        }

        public bool SecondSkillRepeatable
        {
            get { return false; }
            set { }
        }

        public void UseSecondAbility()
        {
            var frags = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                child.gameObject.SetActive(true);
                frags.Add(child.gameObject);
            }
            
            foreach (var frag in frags)
            {
                if (FragmentToFollow == null)
                {
                    FragmentToFollow = frag.gameObject;
                }

                frag.gameObject.SetActive(true);

                var fragRigidbody = frag.GetComponent<Rigidbody2D>();
                if (fragRigidbody)
                {
                    fragRigidbody.freezeRotation = true;
                    fragRigidbody.velocity = frag.transform.right * 10;
                }

                frag.transform.SetParent(transform.parent);
                frag.GetComponent<BlowfishFragment>().IsLaunched = true;
            }
        }
        
        public GameObject Fragment
        {
            get { return _fragment; }
            set { _fragment = value; }
        }

        public GameObject FragmentToFollow { get; set; }

        public int NbOfFragments
        {
            get { return _nbOfFragments; }
            set { _nbOfFragments = value; }
        }
    }
}
