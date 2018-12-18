using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine
{
    private static List<Entity> actors = GameMaster.entitiesList;
    private int currentActor = 0;

    public void Process()
    {
        //Command pattern
        //var action = actors[currentActor].DoAction();
        //action.perform();
        actors[currentActor].DoAction();
        currentActor = (currentActor + 1) % actors.Count;

    }

}
