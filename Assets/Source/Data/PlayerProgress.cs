using System;

namespace Source.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public PlayerProgress(string sceneName) =>
            WorldData = new WorldData(sceneName);

        public WorldData WorldData { get; }
    }
}
