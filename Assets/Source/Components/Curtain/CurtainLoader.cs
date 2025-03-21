using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Source.Components.Curtain
{
    public class CurtainLoader : MonoBehaviour
    {
        [FormerlySerializedAs("_duration")] [SerializeField, Min(0)] private float duration;

        [FormerlySerializedAs("_panel")] [SerializeField] private Image panel;

        public void Hide()
        {
            if (panel.color.a == 0f)
                return;

            panel.DOFade(0f, duration)
                .SetEase(Ease.Linear);
        }

        public void Show() =>
            panel.color = Color.black;
    }
}
