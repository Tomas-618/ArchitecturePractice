using System;

namespace Source.Services.Progress.Contracts
{
    public interface IProgressObservable
    {
        event Action Saved;
    }
}
