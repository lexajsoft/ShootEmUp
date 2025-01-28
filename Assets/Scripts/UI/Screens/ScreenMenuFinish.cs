using Commands;
using GameLoop;
using GameManagers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace UI.Screens
{
    public class ScreenMenuFinish : ViewBase
    {
        [SerializeField] private Button _repeatButton;
        private IGameManager _gameManager;

        [Inject]
        public void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public UnityAction OnRepeatButtonClick;
        private void Start()
        {
            _repeatButton.onClick.AddListener(() =>
            {
                Hide();
                _gameManager.FinishGame();
                //FinishGameCommand.FinishGameCommandFactory.Create().Execute();
            });
        }
    }
}