using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public float firerate = 0;
	public float Damage = 10;
	
	
	float timeToFire = 0;
	public GameObject Bullet;
    public Transform FirePoint;
    
	
	void Update () {
		if (firerate ==0)
		{
			if(Input.GetButtonDown ("Fire1"))
			{
				Shoot();
			}
		}
		else
		{
			if(Input.GetButton ("Fire1")&& Time.time > timeToFire)
			{
				timeToFire = Time.time + 1 / firerate;
				Shoot();
			}
		}

        Bullet.GetComponent<Rigidbody>().velocity = Bullet.transform.forward * 6;
        Destroy(Bullet, 2.0f);
    }
    void FixedUpdate()
    {
        Bullet.GetComponent<Rigidbody>().velocity = Bullet.transform.forward * 6;
        Destroy(Bullet, 2.0f);

    }
	void Shoot ()
	{
        
        Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
       

    }
}
