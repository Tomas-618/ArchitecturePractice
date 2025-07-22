namespace Source.Services.Scenes.Contracts
{
    public interface IActiveScene : IReadOnlyActiveScene
    {
        new string Name { get; set; }
    }
}
