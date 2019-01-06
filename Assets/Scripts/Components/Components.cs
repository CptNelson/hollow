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
    public List<IComponent> components = new List<IComponent>();
    public Entity entity { get; set; }



    //Components are updated every turn. Override this if update is needed.
    abstract public void UpdateComponent();
}


public class LivingComponent : Component
{


    //Position in the xy gridmap
    public Vector3Int Position { get { return _position; } set { _position = value; } }
    //The sprite is set from the prefabs. It is for rendering the entity.
    public GameObject Sprite { get { return _sprite; } set { _sprite = value; } }
    //Game loop checks if entity is player or AI
    public bool NeedsUserInput { get { return _needsUserInput; } set { _needsUserInput = value; } }

    //TODO: Not sure if this is needed, or is Id enough
    public string Name { get { return _name; } set { _name = value; } }
    //If entity is dead, it is removed from the Game loop
    public bool Alive { get { return _alive; } set { _alive = value; } }


    private Vector3Int _position;
    private GameObject _sprite;
    private bool _needsUserInput;
    private string _name;
    private bool _alive = true;

    public override void UpdateComponent()
    {
       
    }
}

public class BodyComponent : Component
{
    //check if this is physical entity.
    public bool HasBody { get { return _hasBody; } set { _hasBody = value; } }
    public int Strength { get { return _strength; } set { _strength = value; } }
    public List<Entity> Items { get { return _items; } set { _items = value; } }

    private List<Entity> _items = new List<Entity>();
    private bool _hasBody = true;
    private int _strength = 3;

    public override void UpdateComponent()
    {

    }

    public void UseItem()
    {
    //    Items[0].GetComponent<Potion>().Use();
    }

    public void TakeDamage(int amount)
    {
        var _hpObserver = new HPObserver();

        
        entity.HP -= amount;
        _hpObserver.OnNotify();
        Debug.Log("HP: " + entity.HP);
        if (entity.HP <= 0)
        {
            entity.GetComponent<LivingComponent>().Sprite.SetActive(false);
            entity.GetComponent<LivingComponent>().Alive = false;
        }
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
    public int Speed { get { return _speed; } set { _speed = value; } }
    public int Energy { get { return _energy; } set { _energy = value; } }

    private int _speed = 50;
    private int _energy = 0;
    private Vector3Int _goal = new Vector3Int(-1, -1, -1);
    private IAction _nextAction;

    public override void UpdateComponent()
    {
        Energy += _speed;
        //Debug.Log(entity.Id + " Energy: " + _energy + " " + _speed);
    }

    public virtual IAction GetAction()
    {
        //entity is updated at beginning of its turn.
        //this should be somewhere else, probably.
        //maybe a ActorComponent that calls every components updates when it's asked for next action.
        if (entity.GetComponent<ActionComponent>().Energy < 100)
        {
            return new WaitAction();
        }

        Energy -= 100;
        //return the action entity chose for the next move.
        var _action = NextAction;
        //Debug.Log(entity.Id + " getting action: " + _action);
        return _action;
    }
}

public class AttackComponent : Component
{

    public override void UpdateComponent()
    {
    }

    public void Attack(Entity target)
    {
        int damage = entity.GetComponent<BodyComponent>().Strength + Utils.GetRandomInt(0, 6);

        target.GetComponent<BodyComponent>().TakeDamage(damage);
    }

}

public class AIComponent : Component
{
    public List<Entity> EntitiesInFov { get { return _entitiesNear; } set { _entitiesNear = value; } }
    private Entity _entity;
    private IAction _action;
    public List<Entity> _entitiesNear;

    public override void UpdateComponent()
    {
        //set next action every turn
        entity.GetComponent<ActionComponent>().NextAction = entity.GetComponent<AIComponent>().ChooseAction();
    }

    public IAction ChooseAction()
    {
        _entity = entity;


        _action = new Walk(entity, Utils.GetRandomInt(-1, 2), Utils.GetRandomInt(-1, 2));

        return _action;

        //reset _action.
        if (_action != null)
        {
          //  _action = null;
        }

        _entitiesNear = new List<Entity>();

        //patrol until sees player, then go to player.
        SCast.ComputeVisibility(entity.GetComponent<LivingComponent>().Position, 4, entity);
        foreach (Entity fovEntity in EntitiesInFov)
        {
            //if entity sees player, set goal to player's position. 
            if (fovEntity.Id == "Player")
            {
                //Debug.Log("goal: " + _entity.GetComponent<ActionComponent>().Goal);
                entity.GetComponent<ActionComponent>().Goal = fovEntity.GetComponent<LivingComponent>().Position;
            } 
        }

        //TODO: if doesn't see player, start patrolling again.

        //If there's no goal position yet, set new one.
        if (entity.GetComponent<ActionComponent>().Goal == new Vector3Int(-1, -1, -1))
        {
            entity.GetComponent<ActionComponent>().Goal = Utils.GetRandomEmptyPosition();
        }
        //should patrol be another AIComponent?
        _action = new Patrol(entity, entity.GetComponent<ActionComponent>().Goal);
        //Debug.Log("set: " + _action);

        _action = new Walk(entity, Utils.GetRandomInt(-1,2), Utils.GetRandomInt(-1, 2));

        return _action;
    }
}

// -----------------------ITEM COMPONENTS---------------------

