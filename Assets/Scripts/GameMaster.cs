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
        MapRoom.CreateRoom();
        map.GetComponent<GameTiles>().CreateTileDictionary();
    }

    private void AddEntities ()
    {

        EntitySpawner.AddPlayer();

    }
}
