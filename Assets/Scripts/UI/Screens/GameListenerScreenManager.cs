using System.Collections.Generic;
using Components;
using GameLoop;
using GameLoop.Interfaces;
using UnityEngine;

namespace UI.Screens
{
    public class GameListenerScreenManager : GameListenerMono, IGameListenerStart, IGameListenerPause,IGameListenerResume, IGameListenerStop
    {
        // активируется до начала игры
        [SerializeField] private List<ViewBase> _screensGameNone;
        // активируется во время игры
        [SerializeField] private List<ViewBase> _screensGamePlaying;
        // активируется во время паузы
        [SerializeField] private List<ViewBase> _screensGamePause;
        // активируется во время по завершению
        [SerializeField] private List<ViewBase> _screensGameFinish;

        private List<ViewBase> _lastShowedScreens = null;

        protected override void OnStart()
        {
            SetVisibleScreens(_screensGameNone,true);
        }

        public void GameStart()
        {
            SetVisibleScreens(_screensGamePlaying,true);
        }

        public void GamePause()
        {
            SetVisibleScreens(_screensGamePause,true);
        }

        public void GameResume()
        {
            SetVisibleScreens(_screensGamePlaying,true);
        }

        public void GameStop()
        {
            SetVisibleScreens(_screensGameFinish,true);
        }
        
        private void SetVisibleScreens(List<ViewBase> screens, bool visible)
        {
            if (visible)
            {
                SetVisibleScreens(screens,false);
                _lastShowedScreens = screens;
            }

            for (int i = 0; i < screens.Count; i++)
            {
                if (visible)
                {
                    screens[i].Show();
                }
                else
                {
                    screens[i].Hide();
                }
            }
        }
    }
}