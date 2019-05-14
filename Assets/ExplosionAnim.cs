using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnim : MonoBehaviour {
    BasicAI ai;
    PlayerController player;
    int DamageRange;
    Collider2D _collider;
    // Use this for initialization
    void Start ()

    {
        _collider = GetComponent<Collider2D>();

	}
	
	// Update is called once per frame
	void Update () {
        Collider2D.Destroy(_collider, Time.fixedDeltaTime);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Static Obstacle") return;

        var distance = Vector2.Distance(collision.gameObject.transform.position, this.gameObject.transform.position);
        if (collision.gameObject.tag == "Enemy")
        {
            ai = collision.gameObject.GetComponent<BasicAI>();
        }
        else if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerController>();
        }

        DamageRange = ((int)(100 - (distance * 50)));
        if (DamageRange >= 1) DealDamage(DamageRange);
        else
        {
            DealDamage(1);
            Debug.Log("damage is negative == " + DamageRange);
        }
        
    }
    private void DealDamage(int _damage)
    {
        
        if (ai != null)
        {
            ai.TakeDamage(_damage);
        }
        else if (player != null)
        {
            player.TakeDamage(_damage);
        }
        

    }



}
