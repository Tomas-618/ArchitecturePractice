using System;
using Assets.Sources.Initialization.Contracts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Sources.Services
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner ?? throw new ArgumentNullException(nameof(coroutineRunner));

        public void LoadAsync(string name, Action loaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(name, loaded));
        }

        private IEnumerator LoadScene(string name, Action loaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                loaded?.Invoke();

                yield break;
            }

            AsyncOperation operation = SceneManager.LoadSceneAsync(name);

            while (operation.isDone == false)
                yield return null;

            loaded?.Invoke();
        }
    }
}
