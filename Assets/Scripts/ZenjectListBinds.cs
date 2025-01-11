using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class ZenjectListBinds : MonoBehaviour
{
    public List<string> Binds;
    [Header("Контекст из которого будут извлекаться все зависимости для просмотра")]
    public Context Context;
    
    [Button()]
    public void Check()
    {
        Binds = new List<string>();

        var container = Context.Container;

        foreach (var item in container.AllContracts)
        {
            Binds.Add(item.Type.Name);
        }
    }
}
