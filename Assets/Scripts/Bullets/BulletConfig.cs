using UnityEngine;

namespace Bullets
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]
    
    
    
    public sealed class BulletConfig : ScriptableObject
    {
        [SerializeField] private BulletConfigData _bulletConfigData;

        public BulletConfigData GetData() => _bulletConfigData;
    }
}