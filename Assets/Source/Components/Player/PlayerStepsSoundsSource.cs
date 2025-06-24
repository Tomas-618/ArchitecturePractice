using System;
using System.Collections.Generic;
using Source.Components.Audio;
using Source.Components.Checkers;
using Source.Components.Player.Constants;
using Source.Data.Audio;
using Source.Services.AssetsManagement.Contracts;
using UnityEngine;
using VContainer;

namespace Source.Components.Player
{
    public class PlayerStepsSoundsSource : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private SphereOverlapChecker _groundOverlapChecker;
        [SerializeField] private PlayerVelocityObserver _velocityObserver;

        private Collider[] _colliders;
        private Dictionary<Collider, Surface> _lastHitColliders;
        private ISurfaceStepsSoundsProvider _stepsSoundsProvider;
        private float _lastStepTime;

        [Inject]
        private void Construct(ISurfaceStepsSoundsProvider stepsSoundsProvider)
        {
            _stepsSoundsProvider = stepsSoundsProvider ??
                                   throw new ArgumentNullException(nameof(stepsSoundsProvider));
            _colliders = new Collider[PlayerConstants.HitCollidersCapacity];
            _lastHitColliders = new Dictionary<Collider, Surface>(PlayerConstants.HitCollidersCapacity);
        }

        private void OnEnable() =>
            _lastStepTime = Time.time;

        private void OnDisable() =>
            ClearColliders();

        private void FixedUpdate()
        {
            Play();
            ClearColliders();
        }

        private void Play()
        {
            float currentTime = Time.time;
            float sqrSpeed = _velocityObserver.CalculateRelative().sqrMagnitude;

            if (CheckSqrSpeedValidation(sqrSpeed) == false
                || _groundOverlapChecker.Check(_colliders) == false
                || TryGetSurfaceType(_colliders, out var surface) == false
                || CheckLastStepTimePassed(sqrSpeed, currentTime) == false)
                return;

            PlayRandomClip(surface.Type);
            UpdateLastTimeStep(currentTime);
        }

        private bool CheckSqrSpeedValidation(float sqrSpeed) =>
            sqrSpeed > PlayerConstants.MinSqrSpeed;

        private bool TryGetSurfaceType(Collider[] hitColliders, out Surface surface)
        {
            surface = null;

            var mainHitCollider = hitColliders[0];
            var extraHitCollider = hitColliders[1];

            return CheckExtraSurface(extraHitCollider, ref surface)
                   || CheckMainSurface(mainHitCollider, ref surface);
        }

        private bool CheckExtraSurface(Collider extraHitCollider, ref Surface surface)
        {
            return extraHitCollider != null
                   && (CheckCashedCollider(extraHitCollider, ref surface) ||
                       TryGetCarpetSurface(extraHitCollider, out surface));
        }

        private bool CheckMainSurface(Collider mainHitCollider, ref Surface surface)
        {
            return mainHitCollider != null
                   && (CheckCashedCollider(mainHitCollider, ref surface) ||
                       TryGetMainSurface(mainHitCollider, out surface));
        }

        private bool CheckCashedCollider(Collider hitCollider, ref Surface surface)
        {
            if (_lastHitColliders.TryGetValue(hitCollider, out var lastHitSurface) == false)
                return false;

            surface = lastHitSurface;

            return true;
        }

        private bool TryGetCarpetSurface(Collider extraHitCollider, out Surface surface)
        {
            if (extraHitCollider.TryGetComponent(out surface) == false
                || surface.Type != SurfaceType.Carpet)
                return false;

            UpdateCashed(extraHitCollider, surface);

            return true;
        }

        private bool TryGetMainSurface(Collider mainHitCollider, out Surface surface)
        {
            if (mainHitCollider.TryGetComponent(out surface) == false)
                return false;

            UpdateCashed(mainHitCollider, surface);

            return true;
        }

        private void UpdateCashed(Collider hitCollider, Surface surface) =>
            _lastHitColliders[hitCollider] = surface;

        private bool CheckLastStepTimePassed(float sqrSpeed, float currentTime)
        {
            float requiredDelay = CalculateRequiredDelay(sqrSpeed);

            return !(currentTime - _lastStepTime < requiredDelay);
        }

        private float CalculateRequiredDelay(float sqrSpeed)
        {
            float delay = PlayerConstants.StepsSoundDelayFactor / sqrSpeed;

            return Mathf.Clamp(delay, PlayerConstants.MinStepSoundDelay, PlayerConstants.MaxStepSoundDelay);
        }

        private void PlayRandomClip(SurfaceType surfaceType)
        {
            var clip = _stepsSoundsProvider.GetRandomClip(surfaceType);
            _audioSource.PlayOneShot(clip, _audioSource.volume);
        }

        private void UpdateLastTimeStep(float currentTime) =>
            _lastStepTime = currentTime;

        private void ClearColliders()
        {
            for (int i = 0; i < _colliders.Length; i++)
                _colliders[i] = null;
        }
    }
}
