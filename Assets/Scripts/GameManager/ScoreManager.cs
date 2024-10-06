using System;
using Character;
using ShootEmUp;
using UnityEngine;
using UnityEngine.Events;

namespace GameManager
{
    public class ScoreManager : MonoBehaviour, IScoreManager,IRegistry
    {
        private int _score = 0;

        public void SetScore(int value)
        {
            _score = value;
            OnScoreChanged?.Invoke(_score);
        }

        public void AddScore(int value)
        {
            _score += value;
            OnScoreChanged?.Invoke(_score);
        }

        public int GetScore()
        {
            return _score;
        }

        public event UnityAction<int> OnScoreChanged;
        
        public void Registry()
        {
            AddictionManager.Instance.Registy(typeof(IScoreManager), this);
        }
    }
}