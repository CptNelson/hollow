using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityAI
{
    IAction ChooseAction { get; set; }
}

public class EntityAI : IEntityAI
{
    protected Entity _entity;
    protected IAction _action;
    public IAction ChooseAction { get; set; }
}

public class BarbarianAI : EntityAI
{

    
    public BarbarianAI()//Entity entity)
    {
        
    }

    public new IAction ChooseAction(Entity entity)
    {
        _entity = entity;

        if (_action != null)
        {
            _action = null;
        }
        //patrol until sees player, then go to player
        var entitiesInFov = FOV.UpdateEntityFOV(_entity, 2);

        foreach(Entity foventity in entitiesInFov)
        {
            //Debug.Log("foventity " + foventity.ToString());
            if (foventity.ToString() == "Player")
            {
                Debug.Log("goal: "+ _entity.Goal);
                _entity.Goal = foventity.Position;
            }
        }

        if (_entity.Goal == new Vector3Int(-1, -1, -1))
        {

            _entity.Goal = Utils.GetRandomEmptyPosition();
           
            //Debug.Log("setting goal: " + _entity.Goal);

        } 
            
        //Debug.Log("patrolling");
        _action = new Patrol(_entity, _entity.Goal);
        return _action;
        //return new GoTo(entity, entity.Position, GameMaster.entitiesList[0].Position);
    }
}