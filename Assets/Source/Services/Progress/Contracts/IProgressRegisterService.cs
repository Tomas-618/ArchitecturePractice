using Source.Data;
using UnityEngine;

namespace Source.Services.Progress.Contracts
{
    public interface IProgressRegisterService
    {
        void RegistChildrenWatchers(GameObject gameObject);

        void UpdateProgress(PlayerProgress progress);

        void Load(PlayerProgress progress);

        void Clear();
    }
}
