using System.Collections.Generic;
using UnityEngine;


public class GameMaster : MonoBehaviour {
    public GameObject map;
    public static List<Entity> entitiesList;
    private Utils utils;
    

    private Engine engine;
    // Use this for initialization
    void Start () {
        
       
        CreateLevel();
        AddEntities();
        engine = new Engine();
        //engine.ProcessGame();
        engine.Process();
        
    }

    private void CreateLevel ()
    {
        RoomGenerator.CreateRoom();
        map.GetComponent<GameTiles>().CreateTileDictionary();
    }

    private void AddEntities ()
    {
        entitiesList = new List<Entity>();
        EntitySpawner.AddEntity();
    }

}
