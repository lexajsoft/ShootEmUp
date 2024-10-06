using System;
using System.Collections.Generic;
using Character;
using Pattern;
using UnityEngine;


public class AddictionManager : SingleTon<AddictionManager>
{
    [SerializeField] private List<GameObject> _objectNeedRegistryOnAwake;
    
    private Dictionary<Type, System.Object> _objects;

    protected override void OnAwake()
    {
        _objects = new Dictionary<Type, object>();
        for (int i = 0; i < _objectNeedRegistryOnAwake.Count; i++)
        {
            
            var registryObject = _objectNeedRegistryOnAwake[i].GetComponent<IRegistry>();
            if (registryObject != null)
            {
                Debug.Log("Registry:" + registryObject.GetType().Name);
                registryObject?.Registry();
            }
            else
            {
                Debug.LogError("GameObject not implemented IRegistry:" + _objectNeedRegistryOnAwake[i].gameObject);
            }
        }
    }

    public T Get<T>() where T : class
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

    public bool Registy(Type type, System.Object obj)
    {
        if (_objects.ContainsKey(type) == false)
        {
            _objects.Add(type,obj);
            return true;
        }

        return false;
    }

    
}