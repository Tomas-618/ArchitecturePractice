using Source.Data;
using UnityEngine;

namespace Source.Services.Progress.Contracts
{
    public interface IProgressRegisterService
    {
        void RegisterChildrenWatchers(GameObject gameObject);

        void UpdateProgress(PlayerProgress progress);

        void Load(PlayerProgress progress);

        void Clear();
    }
}
