using Assets.Sources.Components.Curtain;
using Assets.Sources.Initialization.Contracts;
using Assets.Sources.Initialization.Di;
using UnityEngine;

namespace Assets.Sources.Initialization
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private CurtainLoader _curtainLoader;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, DiContainer.GetInstance(), _curtainLoader);

            DontDestroyOnLoad(this);
            DontDestroyOnLoad(_curtainLoader);
        }

        private void OnApplicationQuit() =>
            _game.Dispose();
    }
}
