using Commands;
using GameLoop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace UI.Screens
{
    public class ScreenMenuPause : ViewBase
    {
        [SerializeField] private Button _pauseButton;
        private MainGameLoop _mainGameLoop;

        [Inject]
        public void Construct(MainGameLoop mainGameLoop)
        {
            _mainGameLoop = mainGameLoop;
        }

        private void Start()
        {
            _pauseButton.onClick.AddListener(() =>
            {
                Hide();
                _mainGameLoop.SetStatus(GameLoopStatus.GameResume);
            });
        }
    }
}