using System;
using System.Linq;

namespace Essentials
{
    public static class Utils
    {
        public static Type[] GetTypeImplementationsNotUnityObject<T>()
        {
            return GetTypeImplementations<T>().Where(impl=>!impl.IsSubclassOf(typeof(UnityEngine.Object))).ToArray();
        }
        
        public static Type[] GetTypeImplementations<T>()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());

            var interfaceType = typeof(T);
            return types.Where(p => interfaceType.IsAssignableFrom(p) && !p.IsAbstract).ToArray();
        }
    }
}
