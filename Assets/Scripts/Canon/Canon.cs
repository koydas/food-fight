using System;
using Assets.Scripts.Food;
using Assets.Scripts.Food.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Canon
{
    public abstract class Canon : MonoBehaviour
    {
        public int ShotsCount;

        [Header("Projectile")]
        [SerializeField]
        protected GameObject Projectile;
        
        [Header("Power")]
        [SerializeField]
        protected float MinimumPower = 3.6f;
        [SerializeField]
        protected float MaximumPower = 7f;

        [Header("Angle")]
        [SerializeField]
        protected float MinimumAngle = 70;
        [SerializeField]
        protected float MaximumAngle = 70;

        protected float OriginalAngle;
        protected Transform CanonBody;
        public GameObject CurrentProjectile;
        
        public virtual void Start ()
        {
            CanonBody = transform.FindChild("Canon-body");
            OriginalAngle = CanonBody.eulerAngles.z;
        }
	
        public virtual void Update()
        {
            if (!LoadLevel.IsLoaded) return;

            SetAngle();
        }
        
        protected abstract void SetAngle();
        public abstract void Fire();

        //todo faire des Unit tests sur le SetAngle
        protected void SetAngle(float ajustment)
        {
            float rangeAngle = MaximumAngle - MinimumAngle;
            float rotateValue = -MinimumAngle - (rangeAngle * ajustment);

            CanonBody.localEulerAngles = new Vector3(0, 0, rotateValue);
        }

        protected bool Fire(float power, bool isEnnemy)
        {
            CurrentProjectile = Instantiate(Projectile, new Vector3(CanonBody.position.x, CanonBody.position.y, 3), Quaternion.identity) as GameObject;

            if (CurrentProjectile != null)
            {

                float z = isEnnemy ? CanonBody.localEulerAngles.z : -CanonBody.localEulerAngles.z;

                CurrentProjectile.transform.localEulerAngles = new Vector3(0, 0, z + 7);
                CurrentProjectile.transform.GetComponent<Rigidbody2D>().freezeRotation = true;

                float rangePower = MaximumPower - MinimumPower;
                float powerVelocity = MinimumPower + (rangePower * power);

                if (isEnnemy)
                {
                    powerVelocity *= -1;
                    CurrentProjectile.layer = Constant.Ennemy;
                }
                else
                {
                    CurrentProjectile.layer = Constant.Player;
                }

                var velocityModifier = CurrentProjectile.GetComponent<Food.Food>() as IVelocityModifier;
                
                CurrentProjectile.GetComponent<Rigidbody2D>().velocity = CurrentProjectile.transform.right * powerVelocity * (velocityModifier != null ? 1 + velocityModifier.VelocityModifier : 1);

                CurrentProjectile.GetComponent<Food.Food>().IsLaunched = true;

                var audioSource = GetComponent<AudioSource>();

                if (audioSource != null)
                {
                    audioSource.Play();
                }

                ShotsCount++;

                return true;
            }

            return false;
        }
    }
}
