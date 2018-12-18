using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Entity player = EntitySpawner.Player;
	// Use this for initialization
	void Update () {

        if (Input.GetKeyDown("up"))         { EntityMover.MoveToPosition(0, 1, player); }
        else if (Input.GetKeyDown("down"))  { EntityMover.MoveToPosition(0, -1, player); }
        else if (Input.GetKeyDown("left"))  { EntityMover.MoveToPosition(-1, 0, player); }
        else if (Input.GetKeyDown("right")) { EntityMover.MoveToPosition(1, 0, player); }
    }

}

