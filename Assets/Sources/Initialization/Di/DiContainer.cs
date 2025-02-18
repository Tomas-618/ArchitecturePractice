using System;

namespace Assets.Sources.Initialization.Di
{
    public class DiContainer
    {
        private static class Implementation<TService>
        {
            public static TService ServiceInstance;
        }

        private static DiContainer s_instance;

        public static DiContainer GetInstance()
        {
            s_instance ??= new DiContainer();

            return s_instance;
        }

        public void RegisterSingle<TService>(TService service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            Implementation<TService>.ServiceInstance = service;
        }

        public TService GetSingle<TService>() =>
            Implementation<TService>.ServiceInstance;
    }
}
