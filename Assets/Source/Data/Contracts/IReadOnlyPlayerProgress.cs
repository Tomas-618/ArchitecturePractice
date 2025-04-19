using Source.Data.Surrogates;

namespace Source.Data.Contracts
{
    public interface IReadOnlyPlayerProgress
    {
        string SceneName { get; }

        Vector3Surrogate Position { get; }
    }
}
