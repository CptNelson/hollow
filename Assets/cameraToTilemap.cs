using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class cameraToTilemap : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //Tilemap tilemap = transform.parent.GetComponent<Tilemap>();

        Camera cam = gameObject.GetComponent<Camera>();

        cam.transform.position = new Vector3Int(21, 10, -10);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
