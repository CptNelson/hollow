using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Entity
{
    public string Id = string.Empty;

    public List<IComponent> components;
    //Every entity has a HP.
    public int HP { get { return _hp; } set { _hp = value; } }
    private int _hp = 10;



    public Entity() : this(string.Empty) { }

    public Entity(string _id) 
    {
        Id = _id;
        components = new List<IComponent>();
    }


    //Every turn entity and its components are updated. 
    public void UpdateEntity()
    {
        // Debug.Log("Update: " + Id + " cmp: " + components.Count);
        foreach (Component cmp in components)
        {
            cmp.UpdateComponent();
        }
    }

    public Entity AddComponent(IComponent newComponent)
    {
        if (newComponent == null)
        {
            Console.WriteLine("Component that you intented to add is null, method will return void");
            return this;
        }
        components.Add(newComponent);
        newComponent.entity = this;

        // Notifies systems so they can perfom operations, like manipulating componet data
        //if (notifySystems) SystemObserver.NotifySystems(this);
        return this;
    }


    public Entity AddComponents(List<IComponent> components)
    {
        foreach (IComponent i in components)
        {
            AddComponent(i);
        }

        if (components.Count == 0)
        {
            Console.WriteLine("Component that you intented to add is null, method will return void");
            return this;
        }

        return this;
    }

    public T GetComponent<T>() where T : class, IComponent
    {
        foreach (IComponent cmp in components)
            if (cmp is T)
                return (T)cmp;

        return null;
    }

    public bool HasComponent<T>() where T : class, IComponent
    {
        foreach (IComponent cmp in components)
            if (cmp is T) return true;

        return false;
    }

}

public class EntityFactory
{
    private Entity entity;
    public EntityFactory()
    {
    }

    public Entity CreateEntity(String id, List<IComponent> componentList)
    {
        entity = new Entity(id);
        entity.AddComponents(componentList);
        return entity;
    }

}