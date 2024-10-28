using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static Dictionary<Type, System.Object> _objects = new Dictionary<Type, object>();
    
    public static T Get<T>() where T : class
    {
        if (_objects.ContainsKey(typeof(T)))
        {
            if (_objects.TryGetValue(typeof(T), out object obj))
            {
                return obj as T;
            }
        }

        return null;
    }
    
    public static object Get(Type type)
    {
        if (_objects.ContainsKey(type))
        {
            if (_objects.TryGetValue(type, out object obj))
            {
                return obj;
            }
        }

        return null;
    }

    public static bool Registy(Type type, System.Object obj)
    {
        if (_objects.ContainsKey(type) == false)
        {
            _objects.Add(type,obj);
            return true;
        }

        return false;
    }
}