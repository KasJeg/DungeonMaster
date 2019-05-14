using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRangeForAI : MonoBehaviour {

    BasicAI ai;
	// Use this for initialization
	void Start () {
        ai = GetComponentInParent<BasicAI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            ai.SearchForPlayer();
        }
    }
}
