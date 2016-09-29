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

            SetAngle(Random.Range(0f, 1f));
            AngleSet = true;
        }
        
        public override void Fire()
        {
            if (!LoadLevel.IsLoaded)
            {
                return;
            }

            if (Projectile != null && CurrentProjectile == null && AngleSet)
            {
                Fire(Random.Range(0f, 1f), true);
                
                AngleSet = false;
            }
        }
    }
}
