using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : ScriptableObject
{
    private static GameObject map = GameObject.Find("Map");
    private static Grid grid = GameObject.Find("Map").GetComponent<Grid>();
    private static GameObject entityObject;
    private static Entity entity;
    private static List<Entity> entitiesList;
    private static EntityFactory factory;

    //Adds entity(name is the prefab name) to map, to position.

    private static void CreateEntities()
    {
        entitiesList = new List<Entity>();
        factory = new EntityFactory();

        Entity player = factory.NewPlayer();
        Entity barbarian = factory.NewBarbarian();
        //TODO: create helper method to add all at once
        entitiesList.Add(player);
        entitiesList.Add(barbarian);
    }

    public static void AddEntity(string name, Vector3Int position)
    {
        CreateEntities();
        for (int i = 0; i < entitiesList.Count; i++)
        {
            entitiesList[i].sprite.transform.position = grid.GetCellCenterLocal(entitiesList[i].position);
            Instantiate(entitiesList[i].sprite, new Vector3(entitiesList[i].sprite.transform.position.x, entitiesList[i].sprite.transform.position.y, 0), Quaternion.identity);
        }
    }

    public static List<Entity> EntitiesList
    {
        get { return entitiesList; }
        set { entitiesList = value; }
    }



}
