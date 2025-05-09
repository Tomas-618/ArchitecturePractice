using Source.Data;
using Source.Data.Contracts;
using UnityEngine;

namespace Source.Services.Progress.Contracts
{
    public interface IProgressRegisterService
    {
        void RegisterActiveSceneService();

        void RegisterChildrenWatchers(GameObject gameObject);

        void Update(PlayerProgress progress);

        void Load(IReadOnlyPlayerProgress progress);

        void ClearWatchers();
    }
}
