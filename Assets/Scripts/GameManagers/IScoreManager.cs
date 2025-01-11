using UnityEngine.Events;

namespace GameManagers
{
    public interface IScoreManager
    {
        void SetScore(int value);
        void AddScore(int value);
        int GetScore();
        event UnityAction<int> OnScoreChanged;
        
    }
}