using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour {

    PlayerController player;
    BasicAI AI;
    public int damage;
    
    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "EnemyVision")
        {
            return;
        }
        if (coll.gameObject.tag == "Player")
        {
            player = coll.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (coll.gameObject.tag == "Enemy")
        {
            AI = coll.gameObject.GetComponent<BasicAI>();
            AI.TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (coll.gameObject.tag == "Bullet" || coll.gameObject.tag == "PlayerBullet" || coll.gameObject.tag == "CanShootOver")
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}