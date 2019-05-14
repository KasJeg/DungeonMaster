using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    PlayerController player;
	private int damage = 10;



	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setDamage(int damage)
	{
		this.damage = damage;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
        if(coll.gameObject.tag=="EnemyVision")
        {
            return;
        }
		if (coll.gameObject.tag == "Player")
        {
            player = coll.gameObject.GetComponent<PlayerController>();
			player.TakeDamage(damage);
			Destroy (this.gameObject);
		} 
		else if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Bullet" || coll.gameObject.tag == "CanShootOver")	
		{
            return;
		}
        else
        {
            Destroy(gameObject);
        }
	}
}
