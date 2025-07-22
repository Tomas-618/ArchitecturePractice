using Source.Services.Progress.Contracts;

namespace Source.Services.Scenes.Contracts
{
    public interface IReadOnlyActiveScene : IProgressSaver
    {
        string Name { get; }
    }
}
