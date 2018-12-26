using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAI
{
    protected Entity _entity;
}

public class BarbarianAI : EntityAI
{
    public BarbarianAI()//Entity entity)
    {
        //_entity = entity;
    }

    public IAction chooseAction(Entity entity)
    {
        if (Utils.GetRandomInt(0,2) == 0)
        return new Walk(entity, 1, 1);
        else return new Walk(entity, -1, -1);
    }
}