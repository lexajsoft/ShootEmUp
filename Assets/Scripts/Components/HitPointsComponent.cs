using System;
using UnityEngine;
using UnityEngine.Events;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> OnIsLiveChanged;
        public event UnityAction<int> OnHitPointsChanged; 
        
        [SerializeField] private int _hitPoints;
        
        public bool IsLive() {
            return this._hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            if (damage > 0)
            {
                
                _hitPoints -= damage;
                OnHitPointsChanged?.Invoke(_hitPoints);
                if (_hitPoints <= 0)
                {
                    OnIsLiveChanged?.Invoke(this.gameObject);
                }
            }
        }

        public int GetHitPoints()
        {
            return _hitPoints;
        }
    }
}