using System;

namespace Source.Services.Scenes.Contracts
{
    public interface ISceneLoader
    {
        void LoadAsync(string name, Action loaded = null);
    }
}
