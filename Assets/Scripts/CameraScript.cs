using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform player;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
		
	}
}
