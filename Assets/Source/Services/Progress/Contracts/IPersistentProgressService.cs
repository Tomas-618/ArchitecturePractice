using Source.Data;
using UnityEngine;

namespace Source.Services.Progress.Contracts
{
    public interface IPersistentProgressService
    {
        PlayerProgress Progress { get; set; }

        void Update();

        void Load();

        void Regist(GameObject gameObject);

        void ClearRegistered();
    }
}
