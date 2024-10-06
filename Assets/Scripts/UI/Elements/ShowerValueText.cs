using UnityEngine;

namespace UI.Elements
{
    public class ShowerValueText : ViewBase
    {
        [SerializeField] private TMPro.TextMeshProUGUI _text;

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}
