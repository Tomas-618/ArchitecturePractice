using System;
using System.Collections;
using Source.Infrastructure.Contracts;
using Source.Services.Progress.Contracts;
using Source.Services.Scenes.Contracts;
using UnityEngine.SceneManagement;

namespace Source.Services.Scenes
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IActiveScene _activeScene;

        public SceneLoader(ICoroutineRunner coroutineRunner, IActiveScene activeScene)
        {
            _coroutineRunner = coroutineRunner ?? throw new ArgumentNullException(nameof(coroutineRunner));
            _activeScene = activeScene ?? throw new ArgumentNullException(nameof(activeScene));
        }

        public void LoadAsync(string name, Action<string> loaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(name, loaded));

        private IEnumerator LoadScene(string name, Action<string> loaded = null)
        {
            if (_activeScene.Name == name)
            {
                loaded?.Invoke(name);

                yield break;
            }

            var operation = SceneManager.LoadSceneAsync(name);

            while (operation.isDone == false)
                yield return null;

            loaded?.Invoke(name);
        }
    }
}
