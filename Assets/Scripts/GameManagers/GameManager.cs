using Character;
using Components;
using GameLoop;
using GameLoop.Interfaces;
using Installer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using Zenject;

namespace GameManagers
{
    //public sealed class GameManager : GameListenerServiceMono<IGameManager>, IGameManager
    public sealed class GameManager : IGameListener, IGameManager
    {
        private IScoreManager _scoreManager;
        
        [Inject]
        public void Construct(IScoreManager scoreManager)
        {
            _scoreManager = scoreManager;
        }

        public void StartGame()
        {
            Debug.Log("StartGame");
            //Time.timeScale = 1;
            ResetScore();
        }

        private void ResetScore()
        {
            _scoreManager.SetScore(0);
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            SceneManager.LoadScene(0);
        }
    }
}