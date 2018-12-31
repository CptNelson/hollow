using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entity
{
    public readonly List<IComponent> components;

    //public IAction NextAction { get { return _nextAction; } set { _nextAction = value; } }
    public int X { get { return _x; } set { _x = value; } }
    public int Y { get { return _y; } set { _y = value; } }
    public Vector3Int Position { get { return _position; } set { _position = value; } }


    public GameObject Sprite { get { return _sprite; } set { _sprite = value; } }
    public bool NeedsUserInput { get { return _needsUserInput; } set { _needsUserInput = value; } }


    public int HP { get { return _hp; } set { _hp = value; } }
    
    public string Name { get { return _name; } set { _name = value; } }
    //public int Speed { get { return _speed; } set { _speed = value; } }
    public bool Alive { get { return _alive; } set { _alive = value; } }

    private int _x;
    private int _y;
    private Vector3Int _position;
    private GameObject _sprite;
    private bool _needsUserInput;

    private int _hp = 10;
    private EntityAI _ai;
    private string _name;
    private int _speed;
    private bool _alive = true;



    public Entity() : this(string.Empty)
    {
    }
    public Entity(string _id)
    {
        Id = _id;
        components = new List<IComponent>();
    }
        public string Id = string.Empty;

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

    public void UpdateEntity()
    {
        Debug.Log("Update: " + Id + " cmp: " + components.Count);
        foreach(Component cmp in components)
        {
            cmp.UpdateComponent();
        }
    }

}

public class EntityFactory
{
    private Entity entity;
    public EntityFactory()
    {
    }

    public Entity CreateEntity(String name, List<IComponent> componentList)
    {
        entity = new Entity(name);
        entity.AddComponents(componentList);
        
        return entity;
    }


}



    