using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : ScriptableObject
{
    public static List<Entity> barbarians;

    private static Grid grid;
    private static List<Entity> entitiesList;  
    //private static Player player;
    private static GameObject temp;
    
    
    private static void CreateEntities()
    {
        grid = GameObject.Find("Map").GetComponent<Grid>();
        entitiesList = GameMaster.entitiesList;

        //create entity and add its prefab to entity.sprite.
        //then go over the entitylist and give them random starting positions.
        var player = Player.Create();
        entitiesList.Add(player);
        player.Sprite = Instantiate(Resources.Load<GameObject>("Prefabs/player"));


        barbarians = new List<Entity>();
        for (int i = 0; i < 1; i++)
        {
            barbarians.Add(Barbarian.Create());
            barbarians[i].Sprite = Instantiate(Resources.Load<GameObject>("Prefabs/barbarian"));
            entitiesList.Add(barbarians[i]);
        }
        
        //Entity barbarian = factory.NewBarbarian();
        //barbarian.sprite = Instantiate(Resources.Load<GameObject>("Prefabs/barbarian"));

        for (int i = 0; i < entitiesList.Count; i++)
        {
            entitiesList[i].Position = Utils.GetRandomEmptyPosition();
            entitiesList[i].Sprite.transform.position = grid.GetCellCenterLocal(entitiesList[i].Position);
        }
        //Debug.Log(entitiesList[0].name + " " + entitiesList[1].name);

    }

    public static void AddEntity()
    {
        CreateEntities();
    }



}
