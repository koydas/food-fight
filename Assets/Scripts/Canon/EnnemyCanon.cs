using Assets.Scripts.Food;
using UnityEngine;

namespace Assets.Scripts.Canon
{
    class EnnemyCanon : Canon
    {
        protected bool AngleSet;
        
        public override void Update()
        {
            base.Update();
            Fire();

        }
        
        protected override void SetAngle()
        {
            if (AngleSet) return;

            float rangeAngle = MaximumAngle - MinimumAngle;
            float rotateValue = -MinimumAngle - (rangeAngle * Random.Range(0f, 1f));

            print(rotateValue);

            CanonBody.eulerAngles = new Vector3(0, 0, rotateValue);
            AngleSet = true;
        }

        //todo revoir pour mettre ça dans la classe de base
        public override void Fire()
        {
            if (Projectile != null && CurrentProjectile == null && AngleSet)
            {
                CurrentProjectile = Instantiate(Projectile, new Vector3(CanonBody.position.x, CanonBody.position.y, Projectile.transform.position.z), Quaternion.identity) as GameObject;

                if (CurrentProjectile != null)
                {
                    CurrentProjectile.transform.eulerAngles = new Vector3(0, 0, CanonBody.eulerAngles.z + 7);
                    CurrentProjectile.transform.GetComponent<Rigidbody2D>().freezeRotation = true;

                    //todo utiliser le min/max power

                    float rangePower = MaximumPower - MinimumPower;
                    float powerVelocity = MinimumPower + (rangePower * Random.Range(0f, 1f));

                    CurrentProjectile.GetComponent<Rigidbody2D>().velocity = CurrentProjectile.transform.right * -powerVelocity;
                    
                    //todo rendre plus generique
                    CurrentProjectile.GetComponent<Fraise>().IsLaunched = true;

                    AngleSet = false;
                }
            }
        }
    }
}
