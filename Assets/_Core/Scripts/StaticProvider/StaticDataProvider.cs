using System;
using System.Collections.Generic;

namespace _Core.StaticProvider
{
    public interface IStaticDataProvider { }
    
    public static class StaticDataProvider
    {
        private static Dictionary<Type, IStaticDataProvider> datas = new(20);
        
        public static void Add<T>(T provider) where T : IStaticDataProvider
        {
            datas.TryAdd(typeof(T), provider);
        }
        
        public static T Get<T>() where T : class, IStaticDataProvider
        {
            datas.TryGetValue(typeof(T), out var data);
            return data as T;
        }
    }
}