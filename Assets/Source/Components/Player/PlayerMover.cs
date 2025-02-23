using Source.Data;
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

        private void Awake()
        {
            _inputService = DiContainer.GetInstance().GetSingle<IInputService>();

            _transform = transform;
        }

        private void Update()
        {
            Vector3 direction = _inputService.GetMoveDirection();

            _characterController.SimpleMove(_speed *
                _transform.TransformDirection(direction));
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            LevelData levelData = playerProgress.WorldData.LevelData;

            levelData.SceneName = SceneManager.GetActiveScene().name;
            levelData.Position = new Vector3Surrogate(_transform.position);
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            LevelData levelData = playerProgress.WorldData.LevelData;

            if (SceneManager.GetActiveScene().name != levelData.SceneName)
                return;

            if (levelData.Position.IsValid == false)
                return;

            _characterController.enabled = false;
            _transform.position = levelData.Position.ConvertToVector3();
            _characterController.enabled = true;
        }
    }
}
