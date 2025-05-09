using System;
using Source.Data.Contracts;
using Source.Data.Surrogates;

namespace Source.Data
{
    [Serializable]
    public class PlayerProgress : IReadOnlyPlayerProgress
    {
        public string SceneName { get; set; }

        public Vector3Surrogate Position { get; set; }

        public float Yaw { get; set; }

        public bool IsValid { get; set; }
    }
}
