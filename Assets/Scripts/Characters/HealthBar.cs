using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private float _maxWidth = 21.35f;
        private SpriteRenderer _spriteRenderer;
        private SousChef _sousChef;

        // Use this for initialization
        void Start ()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _sousChef = GetComponentInParent<SousChef>();
        }
	
        // Update is called once per frame
        void Update ()
        {
            var xx = new Vector3(_sousChef.CurrentHealth / (float)_sousChef.MaxHealth * _maxWidth, _spriteRenderer.gameObject.transform.localScale.y, _spriteRenderer.gameObject.transform.localScale.z);

            _spriteRenderer.gameObject.transform.localScale = xx;
        }
    }
}
