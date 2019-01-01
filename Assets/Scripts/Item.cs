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


public class Potion : ItemComponent
{
    public Potion()
    {
        components.Add(new HealEffect());
    }
    

    public override void Use(Entity target)
    {
        Debug.Log("You look at the " + entity.Id + ".");

        CheckEffects(target);

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