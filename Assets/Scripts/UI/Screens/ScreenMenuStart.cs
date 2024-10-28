using System.Collections;
using Commands;
using GameLoop;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class ScreenMenuStart : ViewBase
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private TMPro.TextMeshProUGUI _timeRemainText;
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
            
            new SetStatusGameLoopCommand(GameLoopStatus.GameStart).Execute();
            Hide();
            _startButton.gameObject.SetActive(true);
        }
    }
}