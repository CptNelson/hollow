using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://github.com/adizhavo/ECS

public interface IComponent
{
    Entity entity { get; set; }
}

abstract public class Component : IComponent
{
    public Entity entity { get; set; }

    //Components are updated every turn. Override this if update is needed.
    abstract public void UpdateComponent();
}


public class BodyComponent : Component
    {
        //check if this is physical entity.
        public bool HasBody { get { return _hasBody; } set { _hasBody = value; } }
        //Every entity has speed. It effects Amount of actions entity can do in given turn. 
        public int Speed { get { return _speed; } set { _speed = value; } }
 
        private bool _hasBody = true;
        private int _speed;

        public override void UpdateComponent()
        {

        }
    }
//Component that makes the entity an actor. Game loop asks for actions from everyone who has this component
//don't add any actions here, make them own components. This is only updater/initiator for those components. Maybe Inheritance.
public class ActionComponent : Component
{
    //If the entity is going somewhere, it has a goal position.
    public Vector3Int Goal { get { return _goal; } set { _goal = value; } }
    //sets actions tha the Game loop executes.
    public IAction NextAction { get { return _nextAction; } set { _nextAction = value; } }
    //Every actionable entity has some kind of AI that chooses what they will do at given situation.
    public EntityAI Ai { get { return _ai; } set { _ai = value; } }

    private Vector3Int _goal = new Vector3Int(-1, -1, -1);
    private IAction _nextAction;
    private EntityAI _ai;

    public override void UpdateComponent()
    {
    }

    //Goal has to be something, so set it outside the map. 
    //TODO: make a better bug catcher.
    public void ResetGoal()
    {
     //   Goal = new Vector3Int(-1, -1, -1);
    }

    public virtual IAction GetAction()
    {
        //entity is updated at beginning of its turn.
        //this should be somewhere else, probably.
        //maybe a ActorComponent that calls every components updates when it's asked for next action.
        entity.UpdateEntity();
        //return the action entity chose for the next move.
        var _action = NextAction;
        Debug.Log(entity.Id + " getting action: " + _action);
        return _action;
    }
}

public class AIComponent : Component
{
    private Entity _entity;
    private IAction _action;

    public override void UpdateComponent()
    {
        //set next action every turn
        entity.GetComponent<ActionComponent>().NextAction = entity.GetComponent<AIComponent>().ChooseAction();
    }

    public IAction ChooseAction()
    {
        _entity = entity;

        //reset _action.
        if (_action != null)
        {
          //  _action = null;
        }

        //patrol until sees player, then go to player.
        var entitiesInFov = FOV.UpdateEntityFOV(_entity, 8);

        foreach (Entity fovEntity in entitiesInFov)
        {
            //if entity sees player, set goal to player's position. 
            if (fovEntity.Id == "Player")
            {
                //Debug.Log("goal: " + _entity.GetComponent<ActionComponent>().Goal);
                _entity.GetComponent<ActionComponent>().Goal = fovEntity.Position;
            }
        }

        //If there's no goal position yet, set new one.
        if (_entity.GetComponent<ActionComponent>().Goal == new Vector3Int(-1, -1, -1))
        {
            _entity.GetComponent<ActionComponent>().Goal = Utils.GetRandomEmptyPosition();
        }
        //should patrol be another AIComponent?
        _action = new Patrol(_entity, _entity.GetComponent<ActionComponent>().Goal);
        //Debug.Log("set: " + _action);

        return _action;
    }

}
