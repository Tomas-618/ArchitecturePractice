using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Source.Configs;
using Source.Data.Audio;
using Source.Services.AssetsManagement.Constants;
using Source.Services.AssetsManagement.Contracts;
using UnityEngine;

namespace Source.Services.AssetsManagement
{
    public class SurfaceStepsSoundsProvider : ISurfaceStepsSoundsProvider
    {
        private readonly Dictionary<SurfaceType, AudioClip[]> _soundsMap;

        [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
        public SurfaceStepsSoundsProvider()
        {
            _soundsMap = Resources.LoadAll<SurfaceStepsSoundsConfig>(AssetsPaths.StepsSoundsPath)
                .ToDictionary(config => config.SurfaceType, config => config.Clips);
        }

        public AudioClip GetRandomClip(SurfaceType surfaceType)
        {
            var surfaceSounds = _soundsMap[surfaceType];

            int clipIndex = Random.Range(0, surfaceSounds.Length);

            return surfaceSounds[clipIndex];
        }
    }
}
