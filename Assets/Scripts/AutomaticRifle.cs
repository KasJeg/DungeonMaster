using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AutomaticRifle : MonoBehaviour
{

    public float projectileSpeed = 500;
    public float fireRate = 0.5F;
    public int bulletDamage = 10;
    public GameObject bullet;
    public float Recoil = 0;

    public GameObject muzzleFlash;
    private Transform bulletPoint;
    public float _nextFire = 0.0F;
	bool DisableShooting = true;
    public AudioSource GunShooting;
    public AudioSource GunFireEnd;


    // Use this for initialization
    void Start()
    {
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
        if(Input.GetKeyUp(KeyCode.Mouse0) && !DisableShooting)
        {
            GunFireEnd.Play();
            GunShooting.Stop();
        }

    }
    void shoot()
    {
        
        if (Time.time > _nextFire)
        {
            
            _nextFire = Time.time + fireRate;
            
            
            GameObject bulletInstance = Instantiate(bullet, bulletPoint.position, Quaternion.Euler(0,0,bulletPoint.eulerAngles.z + Random.Range(-Recoil, Recoil))) as GameObject;

            PlayerBullet bulletScript;
            bulletScript = bulletInstance.GetComponent<PlayerBullet>();
            bulletScript.setDamage(bulletDamage);

            Rigidbody2D bulletRB2D = bulletInstance.GetComponent<Rigidbody2D>();

            Vector3 velocity = bulletInstance.transform.rotation * Vector3.right;
            bulletRB2D.AddForce(velocity * projectileSpeed);
            GameObject muzzle = Instantiate(muzzleFlash, bulletPoint.position, transform.rotation) as GameObject;
            Destroy(muzzle, Time.fixedDeltaTime);
            if (GunShooting.isPlaying == true) return;
            else GunShooting.Play();





        }
    }
    
}
