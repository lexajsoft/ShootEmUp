using System.Collections.Generic;
using Character;
using UnityEngine;

namespace Installer
{
    public class Installer : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _objectsRegistryOnAwake;
        // вызывает  объектов регистрацию, а далее регистрация у каждого объекта своя
        protected void Awake()
        {
            for (int i = 0; i < _objectsRegistryOnAwake.Count; i++)
            {
                var registryObject = _objectsRegistryOnAwake[i].GetComponent<IRegistry>();
                if (registryObject != null)
                {
                    Debug.Log("Registry:" + registryObject.GetType().Name);
                    registryObject?.Registry();
                }
                else
                {
                    Debug.LogError("GameObject not implemented IRegistry:" + _objectsRegistryOnAwake[i].gameObject);
                }
            }
            
        }

    }
}