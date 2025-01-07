using System;
using UI.Elements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Screens
{
    public class ScreenUI : ViewBase
    {
        [SerializeField] private ShowerValueText _hitPointsShowerValueText;
        [SerializeField] private ShowerValueText _scoreShowerValueText;
        [SerializeField] private Button _pauseButton;

        public UnityAction OnPauseButtonClick;
        
        public void SetHitPoints(int value)
        {
            _hitPointsShowerValueText.SetText(value.ToString());
        }
        
        public void SetScore(int value)
        {
            _scoreShowerValueText.SetText(value.ToString());
        }

        private void Start()
        {
            _pauseButton.onClick.AddListener(PauseButtonClickHandle);
        }

        private void PauseButtonClickHandle()
        {
            OnPauseButtonClick?.Invoke();
        }

        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveListener(PauseButtonClickHandle);
        }
    }
}