using Character;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public sealed class GameManager : MonoBehaviour , IGameManager , IRegistry
    {
        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            Debug.Log("StartGame");
            Time.timeScale = 1;
            ResetScore();
        }

        private void ResetScore()
        {
            AddictionManager.Instance.Get<IScoreManager>().SetScore(0);
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
            SceneManager.LoadScene(0);
        }

        public void Registry()
        {
            AddictionManager.Instance.Registy(typeof(IGameManager), this);
        }
    }
}