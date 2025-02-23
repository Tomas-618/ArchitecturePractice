using System;

namespace Source.Data
{
    [Serializable]
    public class WorldData
    {
        public WorldData(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName))
                throw new ArgumentNullException(nameof(sceneName));

            LevelData = new LevelData { SceneName = sceneName };
        }

        public LevelData LevelData { get; }
    }
}
