using Character;
using Components;
using GameLoop;
using GameLoop.Interfaces;
using Installer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public sealed class GameManager : GameListenerServiceMono<IGameManager>, IGameManager
    {
        // protected override void OnStart()
        // {
        //     StartGame();
        // }

        public void StartGame()
        {
            Debug.Log("StartGame");
            //Time.timeScale = 1;
            ResetScore();
        }

        private void ResetScore()
        {
            ServiceLocator.Get<IScoreManager>().SetScore(0);
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            SceneManager.LoadScene(0);
        }

        protected override void OnStart()
        {
            
        }
    }
}