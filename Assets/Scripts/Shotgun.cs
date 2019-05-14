using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour {
    public float fireRate = 0.5F;
    public int Damage;
    private float _nextFire = 0.0F;
    public float Recoil = 0;
    public float projectileSpeed = 500;
    bool DisableShooting;
    public GameObject ShotgunBullet;
    public Transform FirePoint;
    public AudioSource GunShooting;
    //float timeToFire = 0;
    void Start()
    {
        _nextFire = fireRate;
        if(FirePoint == null)
        {
            FirePoint = GameObject.Find("shotgunGunPoint").transform;
        }
        var persistentGameObject = GameObject.Find("PlayerSettings");
        PlayerSettings persistentScript = persistentGameObject.GetComponent<PlayerSettings>();
        DisableShooting = persistentScript.DisableMouse;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !DisableShooting)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Time.time > _nextFire)
        {
            
            for (int i = 0; i < 20; i++)
            {
                
                GameObject bullet = Instantiate(ShotgunBullet, FirePoint.position, Quaternion.Euler(0, 0, FirePoint.eulerAngles.z + Random.Range(-Recoil, Recoil)));
                bullet.tag = "PlayerBullet";
                bullet.GetComponent<ShotgunBullet>().setDamage(Damage);
                Vector3 velocity = bullet.transform.rotation * Vector3.right;
               Rigidbody2D bulletRB2D = bullet.GetComponent<Rigidbody2D>();
               bulletRB2D.AddForce(velocity * projectileSpeed);
            }
            _nextFire = Time.time + fireRate;
            GunShooting.Play();
           

        }

    }
}
