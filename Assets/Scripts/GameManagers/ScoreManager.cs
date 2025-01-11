using System;
using Character;
using GameLoop;
using Installer;
using ShootEmUp;
using UnityEngine;
using UnityEngine.Events;

namespace GameManagers
{
    //public class ScoreManager : ServiceMono<IScoreManager>, IScoreManager
    public class ScoreManager : IScoreManager
    {
        public event UnityAction<int> OnScoreChanged;
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
    }
}