using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : ScriptableObject
{
    public static List<Entity> barbarians;

    private static Grid grid;
    private static List<Entity> entitiesList;  
    private static EntityFactory factory;
    private static Entity player;
    private static GameObject temp;
    
    
    private static void CreateEntities()
    {
        grid = GameObject.Find("Map").GetComponent<Grid>();
        entitiesList = GameMaster.entitiesList;
        factory = new EntityFactory();

        //create entity and add its prefab to entity.sprite.
        //then go over the entitylist and give them random starting positions.
        player = factory.NewPlayer();
        player.sprite = Instantiate(Resources.Load<GameObject>("Prefabs/player"));


        barbarians = new List<Entity>();
        for (int i = 0; i < 3; i++)
        {
            barbarians.Add(factory.NewBarbarian());
            barbarians[i].sprite = Instantiate(Resources.Load<GameObject>("Prefabs/barbarian"));
        }

        //Entity barbarian = factory.NewBarbarian();
        //barbarian.sprite = Instantiate(Resources.Load<GameObject>("Prefabs/barbarian"));

        for (int i = 0; i < entitiesList.Count; i++)
        {
            entitiesList[i].position = Utils.GetRandomEmptyPosition();
            entitiesList[i].sprite.transform.position = grid.GetCellCenterLocal(entitiesList[i].position);
        }
        //Debug.Log(entitiesList[0].name + " " + entitiesList[1].name);

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
