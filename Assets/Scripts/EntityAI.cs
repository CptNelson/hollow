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

    
    public BarbarianAI(Entity entity) { ChooseActio(entity); }

    public IAction ChooseActio(Entity entity)
    {
        _entity = entity;

        if (_action != null)
        {
            _action = null;
        }
        //patrol until sees player, then go to player
        var entitiesInFov = FOV.UpdateEntityFOV(_entity, 6);

        foreach(Entity foventity in entitiesInFov)
        {
            if (foventity.ToString() == "Player")
            {
                Debug.Log("goal: "+ _entity.GetComponent<ActionComponent>().Goal);
                _entity.GetComponent<ActionComponent>().Goal = foventity.Position;
            }
        }

        //If there's no goal yet, set new one.
        if (_entity.GetComponent<ActionComponent>().Goal == new Vector3Int(-1, -1, -1))
        {
            _entity.GetComponent<ActionComponent>().Goal = Utils.GetRandomEmptyPosition();
        }             
        _action = new Patrol(_entity, _entity.GetComponent<ActionComponent>().Goal);
        Debug.Log("set: " + _action);
        return _action;

    }
}

public class TrollAI : EntityAI
{


    public TrollAI(Entity entity) { ChooseAction(entity); }

    public new IAction ChooseAction(Entity entity)
    {
        _action = new SayName(entity);
        Debug.Log("set: " + _action);
        return _action;

    }
}