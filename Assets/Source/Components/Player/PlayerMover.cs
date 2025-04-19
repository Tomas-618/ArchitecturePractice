using Source.Data;
using Source.Data.Contracts;
using Source.Data.Surrogates;
using Source.Infrastructure.Di;
using Source.Services.Input.Contracts;
using Source.Services.Progress.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Components.Player
{
    public class PlayerMover : MonoBehaviour, IProgressSaver
    {
        [SerializeField, Min(0)] private float _speed;

        [SerializeField] private CharacterController _characterController;

        private Transform _transform;
        private IInputService _inputService;
        private IActiveScene _activeScene;

        private void Awake()
        {
            var diContainer = DiContainer.GetInstance();

            _inputService = diContainer.GetSingle<IInputService>();
            _activeScene =  diContainer.GetSingle<IActiveScene>();

            _transform = transform;
        }

        private void Update()
        {
            var direction = _inputService.GetMoveDirection();

            _characterController.SimpleMove(_speed *
                                            _transform.TransformDirection(direction));
        }

        public void UpdateProgress(PlayerProgress playerProgress) =>
            playerProgress.Position = new Vector3Surrogate(_transform.position);

        public void LoadProgress(IReadOnlyPlayerProgress playerProgress)
        {
            if (_activeScene.Name != playerProgress.SceneName)
                return;

            var position =  playerProgress.Position;

            if (position.IsValid == false)
                return;

            _characterController.enabled = false;
            _transform.position = position.ConvertToVector3();
            _characterController.enabled = true;
        }
    }
}
