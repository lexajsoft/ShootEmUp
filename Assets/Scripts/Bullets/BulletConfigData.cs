using System;
using Common;
using UnityEngine;

namespace Bullets
{
    [Serializable]
    public struct BulletConfigData
    {
        public PhysicsLayer physicsLayer;
        public Color color;
        public int damage;
        public float speed;
    }
}