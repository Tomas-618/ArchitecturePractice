using Source.Data.Audio;
using UnityEngine;

namespace Source.Components.Audio
{
    public class Surface : MonoBehaviour
    {
        [field: SerializeField] public SurfaceType Type { get; private set; }
    }
}
