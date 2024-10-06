using System;
using UnityEngine;

namespace Bullets
{
    public struct BulletData
    {
        public bool isPlayer;
        public int damage;
        public Color color;
        public Vector3 position;
        public Vector2 velocity;
        public int physicsLayer;
    }
    
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public BulletData BulletData { get; private set; }

        public event Action<Bullet, GameObject> OnCollisionEntered;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            this.OnCollisionEntered?.Invoke(this, collision.gameObject);
        }

        public void SetData(BulletData data)
        {
            BulletData = data;
            SetVelocity(BulletData.velocity);
            SetColor(BulletData.color);
            SetPosition(BulletData.position);
            SetPhysicsLayer(BulletData.physicsLayer);
        }

        public void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }
    }
}