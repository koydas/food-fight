using UnityEngine;

namespace Assets.Scripts.Canon
{
    public abstract class Canon : MonoBehaviour
    {
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
        protected GameObject CurrentProjectile;
        protected LoadLevel LoadLevel ;
        
        public virtual void Start ()
        {
            LoadLevel = FindObjectOfType<LoadLevel>();
            
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
        
    }
}
