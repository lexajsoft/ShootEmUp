using System.Collections;
using Commands;
using GameLoop;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Screens
{
    public class ScreenMenuStart : ViewBase
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private TMPro.TextMeshProUGUI _timeRemainText;
        
        private MainGameLoop _mainGameLoop;

        [Inject]
        public void Construct(MainGameLoop mainGameLoop)
        {
            _mainGameLoop = mainGameLoop;
        }
        
        private void Start()
        {
            _startButton.onClick.AddListener(() =>
            {
                StartCoroutine(WaitAndStart());
            });
        }

        
        
        private IEnumerator WaitAndStart()
        {
            _startButton.gameObject.SetActive(false);
            for (int i = 3; i > 0; i--)
            {
                _timeRemainText.text = i.ToString();
                yield return new WaitForSeconds(1);
            }
            _timeRemainText.text = "Go";
            yield return new WaitForSeconds(1);
            
            //new SetStatusGameLoopCommand(GameLoopStatus.GamePlay).Execute();
            _mainGameLoop.SetStatus(GameLoopStatus.GamePlay);
            
            Hide();
            _startButton.gameObject.SetActive(true);
        }
    }
}