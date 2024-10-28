using Commands;
using GameLoop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Screens
{
    public class ScreenMenuFinish : ViewBase
    {
        [SerializeField] private Button _repeatButton;

        public UnityAction OnRepeatButtonClick;
        private void Start()
        {
            _repeatButton.onClick.AddListener(() =>
            {
                Hide();
                new FinishGameCommand().Execute();
            });
        }
    }
}