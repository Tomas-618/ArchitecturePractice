namespace Source.Services.Progress.Contracts
{
    public interface IActiveScene
    {
        string Name { get; }

        void Update(string progressName);
    }
}
