using System;
using System.Collections.Generic;
using Components;
using GameLoop;
using GameLoop.Interfaces;
using UnityEngine;
using Zenject;

namespace UI.Screens
{
    public class ScreenManager : MonoBehaviour, IInitializable,  IInitGameListener, IPauseGameListener,IResumeGameListener, IFinishGameListener, IStartPlayGameListener
    {
        [Inject] [SerializeField] private MainGameLoop _mainGameLoop;
        
        // активируется до начала игры
        [SerializeField] private List<ViewBase> _screensGameInit;
        // активируется во время игры
        [SerializeField] private List<ViewBase> _screensGamePlaying;
        // активируется во время паузы
        [SerializeField] private List<ViewBase> _screensGamePause;
        // активируется во время по завершению
        [SerializeField] private List<ViewBase> _screensGameFinish;

        private List<ViewBase> _lastShowedScreens = null;
        
        public void GameInit()
        {
            SetVisibleScreens(_screensGameInit,true);
        }
        
        public void GamePause()
        {
            SetVisibleScreens(_screensGamePause,true);
        }

        public void GameResume()
        {
            SetVisibleScreens(_screensGamePlaying,true);
        }

        public void GameFinish()
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

        public void StartPlay()
        {
            SetVisibleScreens(_screensGamePlaying,true); 
        }

        public void Initialize()
        {
            _mainGameLoop.Add(this);
        }

        public void OnDestroy()
        {
            _mainGameLoop.Remove(this);
        }
    }
}