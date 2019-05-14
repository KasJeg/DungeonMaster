using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour {

    public float SecondsToSpawnAnotherEnemy;
    public Transform Waypoint;
    public int HowManyEnemiesToSpawn = 5;
    public bool everyOneDead = false;
    //public ShopController shopController = new ShopController();
    public List<Transform> SpawnerLocations = new List<Transform>();
    private float _nextSpawn;
    public GameObject Player;

    public GameObject Enemy;
    public bool Enabled = true;


    int i = 0;
	// Use this for initialization
	void Start () {
        _nextSpawn = SecondsToSpawnAnotherEnemy;
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        

        if (Enabled == false) return;

        if (HowManyEnemiesToSpawn <= 0)
        {
            if (areAllDead() == true)
            {
                Debug.Log("Everyone is ded");
                //shopController.alldead();
            }

        }

        else if (Time.time > _nextSpawn)
        {
            if (Player == null) return;
            Transform currentSpawner = getRandomSpawnLocation();

            

            HowManyEnemiesToSpawn--;
            _nextSpawn = Time.time + SecondsToSpawnAnotherEnemy;
            GameObject enemyInstance = Instantiate(Enemy, currentSpawner.position, currentSpawner.rotation) as GameObject;
            BasicAI aiScript = enemyInstance.GetComponent<BasicAI>();
            aiScript.target = Player.transform;
            aiScript._following = true;

            foreach (object enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                i++;
            }
            i = 0;
        }

        

        

        
	}
    public Transform getRandomSpawnLocation()
    {
        
        List<int> DeniedSpawners = new List<int>();
        
        for (int i = 0; i < SpawnerLocations.Count; i++)
        {
            if(Vector2.Distance(Player.transform.position, SpawnerLocations[i].transform.position) < 18)
            {
                DeniedSpawners.Add(i);
            }
        }
        if(DeniedSpawners.Count >= SpawnerLocations.Count)
        {
            Debug.Log("No spawners are available");
            return null;
        }
New:
        int randomSpawn = GetRandomNumber(0, SpawnerLocations.Count);
        if(DeniedSpawners.Contains(randomSpawn))
        {
            goto New;    
        }

        return SpawnerLocations[randomSpawn];
    }
    private int GetRandomNumber(int start, int end)
    {
        System.Random rng = new System.Random();
        int randomSpawn = rng.Next(start, end + 1);
        return randomSpawn;
    }

    public bool areAllDead()
    {
        int i = 0;
        foreach (object enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            i++;
        }
        if (i == 0) return true;
        else
        {
            return false;
        }
    }

    public void alldead()
    {

    }


}
