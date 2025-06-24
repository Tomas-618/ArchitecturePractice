using Source.Data.Audio;
using UnityEngine;

namespace Source.Services.AssetsManagement.Contracts
{
    public interface ISurfaceStepsSoundsProvider
    {
        AudioClip GetRandomClip(SurfaceType surfaceType);
    }
}
