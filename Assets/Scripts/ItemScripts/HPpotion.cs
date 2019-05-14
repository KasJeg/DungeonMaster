using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPpotion : MonoBehaviour {

    public float dropChance = 0.2f;
	public float meleeDropChance = 0.1f;
    public int healAmount = 20;

    public float DropChance
    {
        get
        {
            return dropChance;
        }
        set
        {
            dropChance = value;
        }
    }
    public int HealAmount
    {
        get
        {
            return healAmount;
        }
        set
        {
            healAmount = value;
        }
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            PlayerController playerScript = coll.gameObject.GetComponent<PlayerController>();
            if(playerScript.heal(healAmount) == true)
            {
                Destroy(this.gameObject);
            }
            
        }
    }
}
