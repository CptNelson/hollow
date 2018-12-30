using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IEntity
{
    IAction NextAction { get; set; }
    int X { get; set; }
    int Y { get; set; }
    Vector3Int Position { get; set; }
    GameObject Sprite { get; set; }
    bool NeedsUserInput { get; set; }
    Vector3Int Goal { get; set; }
    FOV Fov { get; set; }

    EntityAI Ai { get; set; }
    string Name { get; set; }
    int Speed { get; set; }
}

public class Entity : IEntity
{
    private IAction _nextAction;
    private int _x;
    private int _y;
    private Vector3Int _position;
    private GameObject _sprite;
    private bool _needsUserInput;
    private Vector3Int _goal = new Vector3Int(-1,-1,-1);

    private FOV _fov;
    private EntityAI _ai;
    private string _name;
    private int _speed;

    public IAction NextAction { get { return _nextAction; } set { _nextAction = value; } }
    public int X { get { return _x; } set { _x = value; } }
    public int Y { get { return _y; } set { _y = value; } }
    public Vector3Int Position { get { return _position; } set { _position = value; } }
    public Vector3Int Goal { get { return _goal; } set { _goal = value; } }
   

    public GameObject Sprite { get { return _sprite; } set { _sprite = value; } }
    public bool NeedsUserInput { get { return _needsUserInput; } set { _needsUserInput = value; } }

    public FOV Fov { get { return _fov; } set { _fov = value; } }
    public EntityAI Ai { get { return _ai; } set { _ai = value; } }
    public string Name { get { return _name; } set { _name = value; } }
    public int Speed { get { return _speed; } set { _speed = value; } }

    public object FOV { get; internal set; }

    public void UpdateEntity()
    {
       // Fov.UpdateFOV();
    }

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
        NextAction = null;
        return action;
    }
}

public class Barbarian : Entity
{
    BarbarianAI barbaAi;
    public Barbarian()
    {
        //return new Barbarian
        {
            Name = "Barbarian";
            Speed = 10;
            //Fov = new FOV(this, 10);
            barbaAi = new BarbarianAI();
        };

    }

    override public IAction GetAction()
    {

        NextAction = barbaAi.ChooseAction(this); //Walk(this, Utils.GetRandomInt(-1, 2), Utils.GetRandomInt(-1, 2));
        //NextAction = new SayName(this);
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
            NeedsUserInput = true;
            //Fov = new FOV(this, 14);
        };
    }


    
}
