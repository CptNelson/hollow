﻿using System;
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
    private string _name;
    private int _speed;

    public IAction NextAction { get { return _nextAction; } set { _nextAction = value; } }
    public int X { get { return _x; } set { _x = value; } }
    public int Y { get { return _y; } set { _y = value; } }
    public Vector3Int Position { get { return _position; } set { _position = value; } }


    public GameObject Sprite { get { return _sprite; } set { _sprite = value; } }
    public bool NeedsUserInput { get { return _needsUserInput; } set { _needsUserInput = value; } }

    public string Name { get { return _name; } set { _name = value; } }
    public int Speed { get { return _speed; } set { _speed = value; } }

    public virtual void SetNextAction(IAction action)
    {
        NextAction = action;
    }

    public virtual IAction GetAction()
    {
        var action = NextAction;
        NextAction = null;
        return action;
    }
}

public class Barbarian : Entity
{
    
    public static Barbarian Create()
    {
        return new Barbarian
        {
            Name = "Barbarian",
            Speed = 10,
            
        };

    }

    override public IAction GetAction()
    {
        NextAction = new Walk(this, Utils.GetRandomInt(-1, 2), Utils.GetRandomInt(-1, 2));
        var action = NextAction;
        NextAction = null;
        return action;
    }

}

public class Player : Entity
{
    public static Player Create()
    {
        return new Player
        {
            Name = "Player",
            Speed = 10,
            NeedsUserInput = true
        };
    }


    
}