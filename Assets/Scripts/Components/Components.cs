using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IComponent
{
    Entity entity { get; set; }
}

abstract public class Component : IComponent
{
    public Entity entity { get; set; }
    abstract public void UpdateComponent();

}


public class BodyComponent : Component
    {
        private bool _hasBody = true;

        private int _speed;
        private bool _alive = true;

      //  public Entity entity { get; set; }


        public bool HasBody { get { return _hasBody; } set { _hasBody = value; } }

        public int Speed { get { return _speed; } set { _speed = value; } }
        public bool Alive { get { return _alive; } set { _alive = value; } }

        public override void UpdateComponent()
        {

        }


        //    Vector3Int Goal { get; set; }
        //    EntityAI Ai { get; set; }
    }

public class ActionComponent : Component
{
    //public Entity entity { get; set; }
    public Vector3Int Goal { get { return _goal; } set { _goal = value; } }
    public IAction NextAction { get { return _nextAction; } set { _nextAction = value; } }
    public EntityAI Ai { get { return _ai; } set { _ai = value; } }

    private Vector3Int _goal = new Vector3Int(-1, -1, -1);

    private IAction _nextAction;
    private EntityAI _ai;

    public override void UpdateComponent()
    {
     //   NextAction = entity.GetComponent<AIComponent>().ChooseAction();
    }


    public void ResetGoal()
    {
        Goal = new Vector3Int(-1, -1, -1);
    }

    public virtual IAction GetAction()
    {
        entity.UpdateEntity();
        //NextAction = tempEnt.GetComponent<ActionComponent>().Ai.ChooseAction()
        var action = NextAction;
        //NextAction = null;
        Debug.Log(entity.Id + " getting action: " + action);
        return action;
    }
}

public class AIComponent : Component
{
    //public Entity entity { get; set; }
    private Entity _entity;
    private IAction _action;

    public override void UpdateComponent()
    {
        entity.GetComponent<ActionComponent>().NextAction = entity.GetComponent<AIComponent>().ChooseAction();
    }

    public IAction ChooseAction()
    {
        _entity = entity;

        if (_action != null)
        {
            _action = null;
        }
        //patrol until sees player, then go to player
        var entitiesInFov = FOV.UpdateEntityFOV(_entity, 6);

        foreach (Entity foventity in entitiesInFov)
        {
            if (foventity.Id == "Player")
            {
                Debug.Log("goal: " + _entity.GetComponent<ActionComponent>().Goal);
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

        int test = 1;

        if (test == 0)
        {
            _action = new TestAction();

        }
        return _action;
    }

}
