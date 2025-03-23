using System;
using System.Collections;
using Source.Infrastructure.Contracts;
using Source.Services.Scenes.Contracts;
using UnityEngine.SceneManagement;

namespace Source.Services.Scenes
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner ?? throw new ArgumentNullException(nameof(coroutineRunner));

        public void LoadAsync(string name, Action loaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(name, loaded));

        private IEnumerator LoadScene(string name, Action loaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                loaded?.Invoke();

                yield break;
            }

            var operation = SceneManager.LoadSceneAsync(name);

            while (operation.isDone == false)
                yield return null;

            loaded?.Invoke();
        }
    }
}
