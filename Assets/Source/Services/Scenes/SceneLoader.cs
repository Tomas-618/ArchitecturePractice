using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Source.Services.Scenes.Contracts;
using UnityEngine.SceneManagement;

namespace Source.Services.Scenes
{
    public class SceneLoader : ISceneLoader
    {
        private readonly IActiveScene _activeScene;

        [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
        public SceneLoader(IActiveScene activeScene) =>
            _activeScene = activeScene ?? throw new ArgumentNullException(nameof(activeScene));

        public async UniTask LoadAsync(string name, CancellationToken cancellationToken)
        {
            if (_activeScene.Name == name)
                return;

            await SceneManager.LoadSceneAsync(name).ToUniTask(cancellationToken: cancellationToken);
            _activeScene.Set(name);
        }
    }
}
