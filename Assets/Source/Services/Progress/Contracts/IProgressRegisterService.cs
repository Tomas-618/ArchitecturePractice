using System.Collections.Generic;
using UnityEngine;

namespace Source.Services.Progress.Contracts
{
    public interface IProgressRegisterService
    {
        IReadOnlyList<IProgressLoader> Loaders { get; }

        IReadOnlyList<IProgressSaver> Savers { get; }

        void RegistChildrenWatchers(GameObject gameObject);

        void Clear();
    }
}
