using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFactory 
{

    public Entity newPlayer()
    {
        Entity player = new Entity();
        //world.addAtEmptyLocation(player);
        //new PlayerAi(player);'
        player.x = 1;
        player.y = 1;
        player.position = new Vector3Int(player.x, player.y, 0);
        player.sprite = Resources.Load<GameObject>("Prefabs/player");

        
        return player;
    }

}
