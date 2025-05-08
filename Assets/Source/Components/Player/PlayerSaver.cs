using System;
using Source.Services.Input.Contracts;
using Source.Services.Progress.Contracts;
using UnityEngine;
using VContainer;

namespace Source.Components.Player
{
	public class PlayerSaver : MonoBehaviour
	{
		private IInputService _inputService;
		private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IInputService inputService, ISaveLoadService saveLoadService)
        {
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
            _saveLoadService = saveLoadService ?? throw new ArgumentNullException(nameof(saveLoadService));
        }

        private void OnEnable() =>
            _inputService.SavedButtonPressed += OnSaveButtonPressed;

        private void OnDisable() =>
            _inputService.SavedButtonPressed -= OnSaveButtonPressed;

        private void OnSaveButtonPressed() =>
            _saveLoadService.Save();
    }
}
