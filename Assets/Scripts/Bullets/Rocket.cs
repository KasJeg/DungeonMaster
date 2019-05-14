using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rocket : MonoBehaviour {


    public GameObject Plahvatus;


    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }




    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "EnemyVision")
        {
            return;
        }
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Bullet")
        {
            return;
        }
        else if (coll.gameObject.tag == "CanShootOver")
        {
            return;
        }
        else
        {
            GameObject instantiateexplo = Instantiate(Plahvatus, this.transform.position, this.transform.rotation) as GameObject;
            Animator plahvatus = instantiateexplo.GetComponent<Animator>();
            plahvatus.SetTrigger("Explode");
            Destroy(instantiateexplo, 2.3f);
            Debug.Log("destroying bullet");
            Destroy(gameObject);
        }
    }

}