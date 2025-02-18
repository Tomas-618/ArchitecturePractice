using UnityEngine;

namespace Assets.Sources.Services.Factories
{
    public class GameObjectsFactory
    {
        public TComponent Create<TComponent>(TComponent prefab, Vector3 position,
            Quaternion rotation, Transform parent = null) where TComponent : Object
        {
            return Object.Instantiate(prefab, position, rotation, parent);
        }
    }
}
