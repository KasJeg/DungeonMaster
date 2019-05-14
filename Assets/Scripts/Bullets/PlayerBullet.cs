using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    BasicAI AI;
	private int damage = 10;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

	public void setDamage(int damage)
	{
		this.damage = damage;
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "EnemyVision")
        {
            return;
        }
		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Bullet") {
			return;
		} else if (coll.gameObject.tag == "Enemy") {
			AI = coll.gameObject.GetComponent<BasicAI> ();
			AI.TakeDamage (damage);
			Destroy (this.gameObject);
		} 
		else if (coll.gameObject.tag == "CanShootOver") 
		{
			return;
		}
        else
        {
            Debug.Log("destroying bullet");
            Destroy(gameObject);
        }
    }
}
