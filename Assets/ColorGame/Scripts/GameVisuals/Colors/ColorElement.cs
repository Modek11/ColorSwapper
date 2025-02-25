using UnityEngine;

namespace ColorGame.Scripts.GameVisuals.Colors
{
    public class ColorElement : MonoBehaviour
    {
        [SerializeField] private Collider2D col2D;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public Collider2D Collider2D => col2D;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
    }
}
