using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoreEntity
{
    public List<IComponent> components;
    //Every entity has a HP.
    public int HP { get { return _hp; } set { _hp = value; } }
    private int _hp = 10;
}

public class Entity : CoreEntity
{
    //Position in the xy gridmap
    public Vector3Int Position { get { return _position; } set { _position = value; } }
    //The sprite is set from the prefabs. It is for rendering the entity.
    public GameObject Sprite { get { return _sprite; } set { _sprite = value; } }
    //Game loop checks if entity is player or AI
    public bool NeedsUserInput { get { return _needsUserInput; } set { _needsUserInput = value; } }

    //TODO: Not sure if this is needed, or is Id enough
    public string Name { get { return _name; } set { _name = value; } }
    //If entity is dead, it is removed from the Game loop
    public bool Alive { get { return _alive; } set { _alive = value; } }
    public string Id = string.Empty;

    private Vector3Int _position;
    private GameObject _sprite;
    private bool _needsUserInput; 
    private string _name;
    private bool _alive = true;



    public Entity() : this(string.Empty) { }

    public Entity(string _id) 
    {
        Id = _id;
        Name = _id;
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