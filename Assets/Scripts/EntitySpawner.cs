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
        Entity player =  factory.newPlayer();
        Debug.Log(player);
        entitiesList.Add(player);
    }

    public static void AddEntity(string name, Vector3Int position)
    {
        CreateEntities();
        Debug.Log(entitiesList[0]);
        //entityObject = Resources.Load<GameObject>("Prefabs/" + name);
        //entityObject.transform.position = grid.GetCellCenterLocal(position);
        //Instantiate(entityObject, new Vector3(entityObject.transform.position.x, entityObject.transform.position.y, 0), Quaternion.identity);
        entitiesList[0].sprite.transform.position = grid.GetCellCenterLocal(entitiesList[0].position);
        Instantiate(entitiesList[0].sprite, new Vector3(entitiesList[0].sprite.transform.position.x, entitiesList[0].sprite.transform.position.y, 0), Quaternion.identity);
        
    }



}
