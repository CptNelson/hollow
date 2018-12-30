using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAI
{
    protected Entity _entity;
    protected IAction _action;
}

public class BarbarianAI : EntityAI
{

    
    public BarbarianAI()//Entity entity)
    {
        
    }

    public IAction chooseAction(Entity entity)
    {
        _entity = entity;

        if (_action != null)
        {
            _action = null;
        }

        if (_entity.Goal == new Vector3Int(-1, -1, -1))
        {

            _entity.Goal = Utils.GetRandomEmptyPosition();
           
            Debug.Log("setting goal: " + _entity.Goal);

        } 
            
        Debug.Log("patrolling");
        _action = new Patrol(_entity, _entity.Goal);
        return _action;
        //return new GoTo(entity, entity.Position, GameMaster.entitiesList[0].Position);
    }
}