using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour {

    public GameObject particleEffect;
    public GameObject aimPoint;
    private Animator animator;
    private SpriteRenderer renderer;

    public float attackRate = 0.5F;
    public int damage = 20;

    private float _nextAttack;
    

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        renderer = GameObject.FindGameObjectWithTag("renderer").GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space))
        {
            AttackMelee();
        }
        if(renderer == null)
        {
            renderer = GameObject.FindGameObjectWithTag("renderer").GetComponent<SpriteRenderer>();
        }
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            renderer.enabled = false;
        }
        else
        {
            renderer.enabled = true;
        }
	}

    

    public void AttackMelee()
    {
        if(Time.time > _nextAttack)
        {
            
            animator.SetTrigger("MeleeAttack");
          //  animator.renderer.transform.position += new Vector3()
          //  renderer.enabled = false;
            _nextAttack = Time.time + attackRate;
            
            GameObject meleeInstance = Instantiate(particleEffect, aimPoint.transform.position, aimPoint.transform.rotation * Quaternion.Euler(0, 0, 90));
            ParticleScript script = meleeInstance.GetComponent<ParticleScript>();
            script.setDamage(damage);
            Destroy(meleeInstance, Time.fixedDeltaTime);
        }

        
    }
}
