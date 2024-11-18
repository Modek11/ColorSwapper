using UnityEngine;

namespace ColorGame.Scripts.InteractableObjects.Obstacles
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ObstacleParent : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D boxCollider2D;

        [SerializeField, HideInInspector] private GameObject colorA;
        [SerializeField, HideInInspector] private GameObject colorB;
        [SerializeField, HideInInspector] private GameObject colorC;
        [SerializeField, HideInInspector] private GameObject colorD;

        public GameObject ColorA => colorA;
        public GameObject ColorB => colorB;
        public GameObject ColorC => colorC;
        public GameObject ColorD => colorD;
        
        private float _obstacleHeight;
        
        public float ObstacleHeight
        {
            get
            {
                if (_obstacleHeight <= 0) 
                {
                    boxCollider2D.enabled = true;
                    _obstacleHeight = boxCollider2D.bounds.size.y;
                    boxCollider2D.enabled = false;
                }

                return _obstacleHeight;
            }
        }
        
        protected void OnValidate()
        {
            if (boxCollider2D == null)
            {
                boxCollider2D = GetComponent<BoxCollider2D>();
            }

            if (transform.childCount >= 4 && (colorA == null || colorB == null || colorC == null || colorD == null))
            {
                colorA = transform.GetChild(0).gameObject;
                colorB = transform.GetChild(1).gameObject;
                colorC = transform.GetChild(2).gameObject;
                colorD = transform.GetChild(3).gameObject;
            }
        }
    }
}
