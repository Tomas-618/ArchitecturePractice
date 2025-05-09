using Source.Services.Progress.Contracts;

namespace Source.Services.Scenes.Contracts
{
    public interface IActiveScene : IProgressSaver
    {
        string Name { get; }

        void Set(string progressName);
    }
}
