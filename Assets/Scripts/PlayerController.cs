using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Entity player = EntitySpawner.Player;
	// Use this for initialization
	void Update () {
        
        if (Input.anyKeyDown)
        {  
            if (Input.GetKeyDown("up"))         { player.MoveTo(0, 1); }
            else if (Input.GetKeyDown("down"))  { player.MoveTo(0, -1); }
            else if (Input.GetKeyDown("left"))  { player.MoveTo(-1, 0); }
            else if (Input.GetKeyDown("right")) { player.MoveTo(1, 0); }
        }
        foreach (Entity ent in GameMaster.entitiesList)
        {
            ent.MoveToRandomDir();
        }
    }

}

