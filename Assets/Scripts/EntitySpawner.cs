using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : ScriptableObject
{
    private static Grid grid;
    //private static Entity entity;
    private static List<Entity> entitiesList;
   
    private static EntityFactory factory;
    private static Entity player;
    private static GameObject temp;
    
    
    private static void CreateEntities()
    {
        grid = GameObject.Find("Map").GetComponent<Grid>();
        entitiesList = GameMaster.entitiesList;
        factory = new EntityFactory();

        player = factory.NewPlayer();
        player.sprite = Instantiate(Resources.Load<GameObject>("Prefabs/player"));
        
        Entity barbarian = factory.NewBarbarian();
        barbarian.sprite = Instantiate(Resources.Load<GameObject>("Prefabs/barbarian"));

        for (int i = 0; i < entitiesList.Count; i++)
        {
            entitiesList[i].sprite.transform.position = grid.GetCellCenterLocal(entitiesList[i].position);
        }
        Debug.Log(entitiesList[0].name + " " + entitiesList[1].name);

    }

    public static void AddEntity()
    {
        CreateEntities();
    }

    public static Entity Player
    {
        get { return player; }
    }


}
