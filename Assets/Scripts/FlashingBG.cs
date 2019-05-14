using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingBG : MonoBehaviour {
    public Camera cam;
    public float Aeg = 3f;
	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        float t = Mathf.PingPong(Time.time, Aeg) / Aeg;
        cam.backgroundColor = Color.Lerp(Color.Lerp(Color.black,Color.red,t), Color.black, t);
	}
}
