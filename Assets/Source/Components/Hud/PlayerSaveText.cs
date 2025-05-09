using System;
using DG.Tweening;
using Source.Extensions;
using Source.Services.Progress.Contracts;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Source.Components.Hud
{
    public class PlayerSaveText : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _showDuration;
        [SerializeField, Min(0)] private float _delay;
        [SerializeField, Min(0)] private float _hideDuration;

        [SerializeField] private Text _text;
        [SerializeField] private string _message;

        private IProgressObservable _progressObservable;
        private Sequence _sequence;

        [Inject]
        private void Construct(IProgressObservable progressObservable)
        {
            _progressObservable = progressObservable ??
                                       throw new ArgumentNullException(nameof(progressObservable));
        }

        private void OnEnable()
        {
            _progressObservable.Saved += OnSaved;
            InitSequence();
        }

        private void OnDisable()
        {
            _progressObservable.Saved -= OnSaved;
            DisposeSequence();
        }

        private void OnSaved() =>
            _sequence.Restart();

        private void InitSequence()
        {
            _sequence = DOTween.Sequence();

            _text.SetFade(1f);

            _sequence.SetAutoKill(false);
            _sequence.Pause();

            AppendShowTween();
            AppendHideTween();
        }

        private void AppendShowTween()
        {
            _sequence.Append(_text.DOText(_message, _showDuration)
                .SetEase(Ease.Linear));
        }

        private void AppendHideTween()
        {
            _sequence.Append(_text.DOFade(0f, _hideDuration)
                .SetEase(Ease.Linear).SetDelay(_delay));
        }

        private void DisposeSequence()
        {
            _sequence.Kill();
            _text.text = string.Empty;
        }
    }
}
