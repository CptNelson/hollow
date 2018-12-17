using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
	// Use this for initialization
	void Update () {

        if (Input.GetKeyDown("up")) { EntityMover.MoveToPosition(0, 1, gameObject); }
        else if (Input.GetKeyDown("down")) { EntityMover.MoveToPosition(0, -1, gameObject); }
        else if (Input.GetKeyDown("left")) { EntityMover.MoveToPosition(-1, 0, gameObject); }
        else if (Input.GetKeyDown("right")) { EntityMover.MoveToPosition(1, 0, gameObject); }
    }

}

