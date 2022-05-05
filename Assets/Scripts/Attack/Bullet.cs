using UnityEngine;

namespace Attack
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private ParticleSystem particle;

        private BulletData _data;

        public BulletData Data
        {
            get { return _data; }
        }

        public void BindData(BulletData data)
        {
            _data = data;

            var spriteRender = GetComponent<SpriteRenderer>();
            spriteRender.color = _data.OwnerColor * 1.3f;

            var component = GetComponent<Rigidbody2D>();
            component.velocity = data.Direction * speed;

            particle.startColor = _data.OwnerColor * 1.3f;
        }
    }

    public class BulletData
    {
        public int OwnerId { get; set; }
        public Color OwnerColor { get; set; }
        public Vector3 Direction { get; set; }
    }
}