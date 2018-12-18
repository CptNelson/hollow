using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFactory 
{
    public Entity NewPlayer()
    {
        Entity player = new Entity();
        player.name = "player";
        //world.addAtEmptyLocation(player);
        //new PlayerAi(player);'
        player.x = 1;
        player.y = 1;
        player.position = new Vector3Int(player.x, player.y, 0);
        player.sprite = Resources.Load<GameObject>("Prefabs/player");
        return player;
    }

    public Entity NewBarbarian()
    {
        Entity barbarian = new Entity();
        barbarian.name = "player";
        barbarian.x = 5;
        barbarian.y = 5;
        barbarian.position = new Vector3Int(barbarian.x, barbarian.y, 0);
        barbarian.sprite = Resources.Load<GameObject>("Prefabs/barbarian");
        return barbarian;
    }

}
