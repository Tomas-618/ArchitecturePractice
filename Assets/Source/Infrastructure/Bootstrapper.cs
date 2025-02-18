using Source.Components.Curtain;
using Source.Infrastructure.Contracts;
using Source.Infrastructure.Di;
using UnityEngine;

namespace Source.Infrastructure
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
