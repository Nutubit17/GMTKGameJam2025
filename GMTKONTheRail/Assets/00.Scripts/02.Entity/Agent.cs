using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Agent : MonoBehaviour
{
    protected Dictionary<Type, IGetCompoable> _components;
    protected virtual void Awake()
    {
        _components = new Dictionary<Type, IGetCompoable>();

        List<IGetCompoable> list = GetComponentsInChildren<IGetCompoable>(true)
    .ToList();
        AddComponentToDictionary(ComponentInitialize(list));

        AfterInit(list);
    }

    private void AddComponentToDictionary(List<IGetCompoable> list)
    {
        list.ForEach(component => _components.TryAdd(component.GetType(), component));
    }

    private List<IGetCompoable> ComponentInitialize(List<IGetCompoable> list)
    {
        list.ForEach(component => component.Init(this));
        return list;
        //_components.Values.ToList().ForEach(component => component.Initialize(this));
    }

    protected virtual void AfterInit(List<IGetCompoable> list)
    {
        list.OfType<IAfterInitable>()
            .ToList().ForEach(afterInitable => afterInitable.AfterInit());
    }

    public T GetCompo<T>(bool isDerived = false) where T : IGetCompoable
    {
        if (_components.TryGetValue(typeof(T), out var component))
        {
            return (T)component;
        }

        if (isDerived == false) return default;

        Type findType = _components.Keys.FirstOrDefault(type => type.IsSubclassOf(typeof(T)));
        if (findType != null)
            return (T)_components[findType];

        return default;
    }
}
