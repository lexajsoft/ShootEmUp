using Character;
using Components;
using GameLoop;
using GameLoop.Interfaces;
using Installer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public sealed class GameManager : GameListenerMono , IGameManager, IRegistry, IGameListenerTick, IGameListenerStart, IGameListenerPause, IGameListenerStop, IGameListenerResume
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

        public void Registry()
        {
            ServiceLocator.Registy(typeof(IGameManager), this);
        }

        public void GameTick(float deltaTime)
        {
            
        }

        public void GameStart()
        {
            
        }

        public void GamePause()
        {
            
        }

        public void GameStop()
        {
            
        }

        public void GameResume()
        {
            
        }
    }
}