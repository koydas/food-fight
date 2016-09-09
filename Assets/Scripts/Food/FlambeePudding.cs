using System;
using System.Collections.Generic;
using Assets.Scripts.Canon;
using Assets.Scripts.Characters;
using Assets.Scripts.Food.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Food
{
    public class FlambeePudding : Food, IDot
    {
        private bool _exploded;
        private bool _isIgnited;
        private List<GameObject> _affectedSousChefs;

        [SerializeField]
        private int NbOfShotsBeforeDestroy = 3;

        private int PlayerCanonTargetShots;
        private PlayerCanon _playerCanon;
        
        private int _inspectorDotDamage;
        private int _inspectorDotTimer;

        [SerializeField]
        private int _dotDamage;
        [SerializeField]
        private int _dotTimer;

        private int _dotNbOfTicks;

        public int DotDamage
        {
            get { return _dotDamage; }
            set { _dotDamage = value; }
        }

        public int DotTimer
        {
            get { return _dotTimer; }
            set { _dotTimer = value; }
        }

        public void Start()
        {
            _playerCanon = FindObjectOfType<PlayerCanon>();
            _affectedSousChefs = new List<GameObject>();

            _inspectorDotDamage = DotDamage;
            _inspectorDotTimer = DotTimer;

            DotDamage = 0;
            DotTimer = 0;
        }

        public new void Update()
        {
            base.Update();

            if (_isIgnited && _dotNbOfTicks <= 0)
            {
                Destroy();
                }
            else
            {
                _dotNbOfTicks--;
            }

            if (_exploded && _playerCanon.ShotsCount > PlayerCanonTargetShots)
            {
                Destroy();
            }
        }

        public override EnumFood EnumFood
        {
            get
            {
                return EnumFood.FlambeePudding;
            }
            set { }
        }

        public override void OnCollisionEnter2D(Collision2D coll)
        {
            if (IsLaunched && coll.gameObject.tag != Constant.PlatformLimiter && coll.gameObject.tag != Constant.Fragment)
            {
                RotationAllowed = false;
                transform.localEulerAngles = new Vector3();

                // Change active gameObject
                if (!_exploded)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

                    // remove from PlayerCannon                    
                    _playerCanon.CurrentProjectile = null;
                    PlayerCanonTargetShots = _playerCanon.ShotsCount + NbOfShotsBeforeDestroy;
                }
                
                _exploded = true;

            }
        }
        
        public void OnTriggerStay2D(Collider2D coll)
        {
            if (_exploded && coll.gameObject.GetComponent<HotPepper>())
            {
                _isIgnited = true;

                DotDamage = _inspectorDotDamage;
                DotTimer = _inspectorDotTimer;

                var fps = 1.0f / Time.deltaTime;
                _dotNbOfTicks = DotTimer * (int)fps;

                transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.red;
            }

            if (IsLaunched && coll.gameObject.tag == Constant.SousChef)
            {
                coll.GetComponent<SousChef>().IsSlowed = true;
                _affectedSousChefs.Add(coll.gameObject);
            }
        }

        private void Destroy()
        {
            foreach (var souschef in _affectedSousChefs)
            {
                souschef.GetComponent<SousChef>().IsSlowed = false;
            }

            Destroy(gameObject);
        }
        
    }
}
