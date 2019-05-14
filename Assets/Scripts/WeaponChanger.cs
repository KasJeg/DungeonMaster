using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class WeaponChanger : MonoBehaviour
    {
        public static void changeWeapon(string weapon, GameObject currentGameObject)
        {
            if(weapon == "shotgun")
            {
                GameObject shotgun = Resources.Load("ShotGunPlayer", typeof(GameObject)) as GameObject;
                Instantiate(shotgun, currentGameObject.transform.position, currentGameObject.transform.rotation);
                
                Destroy(currentGameObject);
            }
            else if(weapon == "assaultrifle")
            {
                GameObject shotgun = Resources.Load("AssaultRiflePlayer", typeof(GameObject)) as GameObject;
                Instantiate(shotgun, currentGameObject.transform.position, currentGameObject.transform.rotation);
                Destroy(currentGameObject);
            }
            else if(weapon == "rocketlauncher")
            {
                GameObject shotgun = Resources.Load("RocketLauncher", typeof(GameObject)) as GameObject;
                Instantiate(shotgun, currentGameObject.transform.position, currentGameObject.transform.rotation);
                Destroy(currentGameObject);
            }
        }
    }
}


