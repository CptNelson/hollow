using UnityEngine;

public class GameMaster : MonoBehaviour {
    public GameObject map;
	// Use this for initialization
	void Start () {
        RoomGenerator.CreateRoom();
        EntitySpawner.AddPlayer();
        var GTiles = map.GetComponent<GameTiles>();
        GTiles.DoTiles();

    
	}
	


	// Update is called once per frame
	void Update () {
		
	}
}
