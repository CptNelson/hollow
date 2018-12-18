using UnityEngine;

public class GameMaster : MonoBehaviour {
    public GameObject map;
	// Use this for initialization
	void Start () {
        CreateLevel();
        AddEntities();
	}

    private void CreateLevel ()
    {
        RoomGenerator.CreateRoom();
        map.GetComponent<GameTiles>().CreateTileDictionary();
    }

    private void AddEntities ()
    {
        EntitySpawner.AddEntity("player", new Vector3Int(3,3,0));
        EntitySpawner.AddEntity("barbarian", new Vector3Int(5, 5, 0));
    }
}
