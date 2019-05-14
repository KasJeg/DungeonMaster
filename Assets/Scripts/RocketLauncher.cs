using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour {

    public float projectileSpeed = 200;
    public float fireRate = 0.5F;
    
    public GameObject rocket;

    public Transform bulletPoint;
    private float _nextFire = 0.0F;
    bool DisableShooting = false;

    // Use this for initialization
    void Start()
    {
        _nextFire = fireRate;
        bulletPoint = GameObject.FindGameObjectWithTag("Gunpoint").transform;
        var persistentGameObject = GameObject.Find("PlayerSettings");
        PlayerSettings persistentScript = persistentGameObject.GetComponent<PlayerSettings>();
        DisableShooting = persistentScript.DisableMouse;
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletPoint == null)
        {
            bulletPoint = GameObject.FindGameObjectWithTag("Gunpoint").transform;
        }
        if (Input.GetKey(KeyCode.Mouse0) && !DisableShooting)
        {
            shoot();
        }
    }
    void shoot()
    {
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + fireRate;

            GameObject bulletInstance = Instantiate(rocket, bulletPoint.position, bulletPoint.rotation) as GameObject;
            Rigidbody2D bulletRB2D = bulletInstance.GetComponent<Rigidbody2D>();
            bulletRB2D.AddForce(transform.right * projectileSpeed);
            
        }

    }
}
