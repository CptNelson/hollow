using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//-----Components------------


public class ItemComponent : Component
{
    public int Weigth { get { return _weigth; } set { _weigth = value; } }
    private int _weigth;

    public override void UpdateComponent()
    {

    }

    virtual public void Use(Entity target)
    {
        Debug.Log("You look at the " + entity.Id + ".");
        CheckEffects(target);
    }

    public void  CheckEffects(Entity target)
    {
        foreach (BaseEffect effect in components)
        {
            effect.UseEffect(target);
            Debug.Log(components.Count);
        }
    }
}
// base class for containers.
public class ItemHolder : ItemComponent
{
    public int Capacity { get { return _capacity; } set { _capacity = value; } }
    private int _capacity;
}

public class Backpack : ItemHolder
{
    public Backpack()
    {
        Weigth = 3;
        Capacity = 10;
    }
}


//Base class for items that have limited uses.
public class Consumable : ItemComponent
{
    public int UsesLeft { get { return _usesLeft;  } set { _usesLeft = value;  } }

    private int _usesLeft;
}


public class Potion : Consumable
{
    
    public Potion()
    {
        Weigth = 1;
        components.Add(new HealEffect());
        UsesLeft = 3;
    }
    

    public override void Use(Entity target)
    {
        
        if (UsesLeft > 1)
        {
            CheckEffects(target);
            UsesLeft -= 1;
        }

        else Debug.Log("No uses left!");

    }
}


//------------------EFFECTS----------------
public class BaseEffect : Component
{
    BaseEffect effect;
    public override void UpdateComponent()
    {
    }
    public virtual void UseEffect(Entity target)
    {
    }
}

public class HealEffect : BaseEffect
{
    public override void UseEffect(Entity target)
    {
        Debug.Log(target.Id + " HP: " + target.HP);
        target.HP += 4;
    }
}