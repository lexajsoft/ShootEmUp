using UI.Elements;
using UnityEngine;

namespace UI.Screens
{
    public class ScreenUI : ViewBase
    {
        [SerializeField] private ShowerValueText _hitPointsShowerValueText;
        [SerializeField] private ShowerValueText _scoreShowerValueText;

        public void SetHitPoints(int value)
        {
            _hitPointsShowerValueText.SetText(value.ToString());
        }
        
        public void SetScore(int value)
        {
            _scoreShowerValueText.SetText(value.ToString());
        }
    }
}