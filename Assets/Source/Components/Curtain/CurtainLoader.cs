using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Components.Curtain
{
    public class CurtainLoader : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _duration;

        [SerializeField] private Image _panel;

        public void Hide()
        {
            if (_panel.color.a == 0f)
                return;

            _panel.DOFade(0f, _duration)
                .SetEase(Ease.Linear);
        }

        public void Show() =>
            _panel.color = Color.black;
    }
}
