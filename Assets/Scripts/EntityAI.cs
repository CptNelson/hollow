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
        return new GoTo(entity, entity.Position, GameMaster.entitiesList[0].Position);
    }
}