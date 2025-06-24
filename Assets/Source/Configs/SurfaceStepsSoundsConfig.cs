using Source.Data.Audio;
using UnityEngine;

namespace Source.Configs
{
    [CreateAssetMenu(fileName = "SurfaceStepsSoundsConfig",
        menuName = "Configs/SurfaceStepsSoundsConfig")]
    public class SurfaceStepsSoundsConfig : ScriptableObject
    {
        public SurfaceType SurfaceType;
        public AudioClip[] Clips;
    }
}
