using Commands;
using GameLoop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Screens
{
    public class ScreenMenuPause : ViewBase
    {
        [SerializeField] private Button _pauseButton;

        private void Start()
        {
            _pauseButton.onClick.AddListener(() =>
            {
                Hide();
                // new SetStatusGameLoopCommand(GameLoopStatus.GameResume).Execute();
                ServiceLocator.Get<GameLoop.GameLoop>().SetStatus(GameLoopStatus.GameResume);
                
            });
        }
    }
}