using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAI
{

    protected Entity entity;




    public IAction GetAction(IAction _action)
    {
        return _action;
    }
}

public class BarbarianAI : EntityAI
{
    public BarbarianAI(Entity entity)
    {

    }
}