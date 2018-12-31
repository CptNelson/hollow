using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComponent
{
    Entity entity { get; set; }
}

public class BodyComponent : IComponent
{
    private int _x;
    private int _y;
    private Vector3Int _position;
    private GameObject _sprite;
    private bool _hasBody = true;
    private int _hp = 10;
    private int _speed;
    private bool _alive = true;

    public Entity entity { get; set; }

    public int X { get { return _x; } set { _x = value; } }
    public int Y { get { return _y; } set { _y = value; } }
    public Vector3Int Position { get { return _position; } set { _position = value; } }
    public GameObject Sprite { get { return _sprite; } set { _sprite = value; } }
    public bool HasBody { get { return _hasBody; } set { _hasBody = value; } }
    public int HP { get { return _hp; } set { _hp = value; } }
    public int Speed { get { return _speed; } set { _speed = value; } }
    public bool Alive { get { return _alive; } set { _alive = value; } }
}


public interface IEntity
{
    

    IAction NextAction { get; set; }
    int X { get; set; }
    int Y { get; set; }
    Vector3Int Position { get; set; }
    GameObject Sprite { get; set; }
    bool NeedsUserInput { get; set; }
    Vector3Int Goal { get; set; }

    int HP { get; set; }
    bool HasBody { get; set; }
    EntityAI Ai { get; set; }
    string Name { get; set; }
    int Speed { get; set; }
    bool Alive { get; set; }
}

public class Entity : IEntity
{
    public readonly List<IComponent> components;

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




    private IAction _nextAction;
    private int _x;
    private int _y;
    private Vector3Int _position;
    private GameObject _sprite;
    private bool _needsUserInput;
    private Vector3Int _goal = new Vector3Int(-1,-1,-1);

    private bool _hasBody = true;
    private int _hp = 10;
    private EntityAI _ai;
    private string _name;
    private int _speed;
    private bool _alive = true;

    public IAction NextAction { get { return _nextAction; } set { _nextAction = value; } }
    public int X { get { return _x; } set { _x = value; } }
    public int Y { get { return _y; } set { _y = value; } }
    public Vector3Int Position { get { return _position; } set { _position = value; } }
    public Vector3Int Goal { get { return _goal; } set { _goal = value; } }
   

    public GameObject Sprite { get { return _sprite; } set { _sprite = value; } }
    public bool NeedsUserInput { get { return _needsUserInput; } set { _needsUserInput = value; } }

    public bool HasBody { get { return _hasBody; } set { _hasBody = value; } }
    public int HP { get { return _hp; } set { _hp = value; } }
    public EntityAI Ai { get { return _ai; } set { _ai = value; } }
    public string Name { get { return _name; } set { _name = value; } }
    public int Speed { get { return _speed; } set { _speed = value; } }
    public bool Alive { get { return _alive; } set { _alive = value; } }

    public object FOV { get; internal set; }


    public void ResetGoal()
    {
        Goal = new Vector3Int(-1, -1, -1);
    }

    public virtual void SetNextAction(IAction action)
    {
        NextAction = action;
    }

    public virtual IAction GetAction()
    {
        //this.UpdateEntity();
        var action = NextAction;
        //NextAction = null;
        return action;
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
        entity.NextAction = new SayName(entity);
        return entity;
    }


}



public class Barbarian : Entity
{
    BarbarianAI barbaAi;
    public Barbarian()
    {
        {
            Name = "Barbarian";
            Speed = 10;
            HP = 12;
        };
    }

    override public IAction GetAction()
    {

        NextAction = new BarbarianAI().ChooseAction(this);
        var action = NextAction;
        
        return action;
    }

}

public class Player : Entity
{
    public Player()
    {
        //return new Player
        {
            Name = "Player";
            Speed = 10;
            HP = 12;
            NeedsUserInput = true;
            //Fov = new FOV(this, 14);
        };
    }


    
}
