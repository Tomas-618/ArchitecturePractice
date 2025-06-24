using Source.Data;
using UnityEngine;

namespace Source.Components.InitialPoints
{
    public class PlayerInitialPoint : MonoBehaviour
    {
        public SpawnData SpawnData { get; private set; }

        private void Awake()
        {
            SpawnData = new SpawnData
            {
                Position = transform.position,
                Rotation = transform.rotation
            };
        }
    }
}
