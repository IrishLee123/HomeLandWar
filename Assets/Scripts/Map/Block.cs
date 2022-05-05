using Attack;
using UnityEngine;

namespace Map
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer renderer;

        private int _ownerId;
        private Color _color;

        public void Init(int ownerId, Color color)
        {
            _ownerId = ownerId;
            _color = color;

            renderer.color = _color;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var bullet = other.GetComponent<Bullet>();

            if (_ownerId == bullet.Data.OwnerId) return;//相同势力不处理

            _ownerId = bullet.Data.OwnerId;
            _color = bullet.Data.OwnerColor;
            renderer.color = _color;
            
            GameController.Instance.RecycleBullet(bullet.gameObject);
        }
    }
}