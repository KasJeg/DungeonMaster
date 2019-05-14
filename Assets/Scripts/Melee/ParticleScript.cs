
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour {
    public int damage { get; set; }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public string attacker { get; set; }
    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Enemy" && attacker != "enemy")
        {
            BasicAI ai = coll.gameObject.GetComponent<BasicAI>();
            ai.TakeDamage(damage);
        }
        if(coll.gameObject.tag == "Player" && attacker == "enemy")
        {
            player.PlayerController Player = coll.gameObject.GetComponent<player.PlayerController>();
            Player.TakeDamage(damage);
            Debug.Log(damage);
        }
    }
}
