using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BasicAI : MonoBehaviour
{
    public int visionRange = 50;
	public GameObject bullet;
    public float StopRange;
	public float attackSpeed;
	public int bulletDamage = 10;
    public Transform target;
	public Transform RayCastStart;
    public float fireRate = 0.5F;
    public int health = 100;
    public bool CanKite = true;
    public bool WanderToWaypoints = false;
    public float wanderSpeed = 2;
    public GameObject muzzleFlash;
    public float waitAtWaypoint;
    public float Recoil = 0;

    public List<Transform> Waypoints = new List<Transform>();
    private float _nextWaypoint = 0;
    private int _CurrentHealth;
    private Seeker _seeker;
    private Rigidbody2D _rb2d;
    public bool _seePlayer = false;
    public bool _following = false;
    private float _nextFire = 1.0F;
    private bool _kiteMode = false;
    private int _currentWanderWaypoint = 0;
    private bool _WaitingAtWaypoint = false;
    public bool isMeleeUnit = false;


    public List<GameObject> Drops = new List<GameObject>();

    //calculated path
    public Path path;

    public float speed;


    // The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;
    // The waypoint we are currently moving towards
    private int currentWaypoint = 0;
    // How often to recalculate the path (in seconds)
    public float repathRate = 0.5f;
    private float lastRepath = -9999;

    // Use this for initialization
    void Start()

    {
        _CurrentHealth = health;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _seeker = GetComponent<Seeker>();
        _rb2d = GetComponent<Rigidbody2D>();
    }



    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
    }
    public void Update()
    {
        if(target == null)
        {
			try{
				target = GameObject.FindGameObjectWithTag("Player").transform;
			}
			catch{
			}
        }
		FollowPlayer ();
		ShootPlayer ();
        wanderToWaypoints();
    }

	void FixedUpdate()
	{
        
	}

	void lookAtPlayer()
	{
        if (target == null) return;
		var dir = target.position - transform.position;
		var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		var q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp (transform.rotation, q, 1F);
	}


    
    void lookAtDir(Vector3 direction)
    {
		
        if (currentWaypoint == 0) return;
        var pos = direction + transform.position;
        var dir = pos - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, 0.2F);
    }

   

    public void SearchForPlayer()
	{
		Debug.DrawRay (RayCastStart.position, target.position - transform.position, Color.red);
        RaycastHit2D[] rayCastAll = Physics2D.RaycastAll(RayCastStart.position, target.position - transform.position);

        

        int WallPos = 99;
        int PlayerPos = 100;
        bool firstWall = true;

        for (int i = 0; i < rayCastAll.Length; i++)
        {
            if (rayCastAll[i].collider.tag == "Object")
            {
                if(firstWall)
                {
                    WallPos = i;
                    firstWall = false;
                }
            }
            if (rayCastAll[i].collider.tag == "Player")
            {
                PlayerPos = i;
            }
        }
        if (PlayerPos < WallPos)
        {
            if (Vector2.Distance(transform.position, target.position) >= visionRange)
                return;

            if (WanderToWaypoints == true)
            {
                currentWaypoint = 0;
                WanderToWaypoints = false;
            }
            _seePlayer = true;
            _following = true;
        }
        else
        {
            _seePlayer = false;
        }

        //      if (rayCastAll.Length < 2) return; 

        //if (rayCastAll[1].collider.tag == "Player" || (rayCastAll[1].collider.tag == "CanShootOver" && rayCastAll[2].collider.tag == "Player"))
        //      { 

        //	if (Vector2.Distance(transform.position, target.position) >= visionRange)
        //              return;

        //	if (WanderToWaypoints == true) 
        //	{
        //		currentWaypoint = 0;
        //		WanderToWaypoints = false;
        //	}
        //	_seePlayer = true;
        //	_following = true;
        //} 
        //      else if(rayCastAll[1].collider.tag == "StaticObstacle")
        //      {

        //      }
        //else 
        //{

        //	_seePlayer = false;
        //}

    }

    

	void FollowPlayer()
	{
        if (target == null) return;
		if (!_following) {
			return;
		}
		lookAtPlayer ();
		if(StopRange >= Vector3.Distance(transform.position, target.transform.position) && _seePlayer)
		{
            _rb2d.velocity = Vector2.zero;
			if(StopRange - 0.5 >= Vector3.Distance(transform.position, target.transform.position) && CanKite)
			{
				_kiteMode = true;
			}
			else
			{
                return;
			}

		}
		if (Vector3.Distance(transform.position, target.transform.position) > StopRange && _kiteMode == true)
		{
			_kiteMode = false;
		}
		if (Time.time - lastRepath > repathRate && _seeker.IsDone())
		{
			lastRepath = Time.time + UnityEngine.Random.value * repathRate * 0.5f;
			// Start a new path to the targetPosition, call the the OnPathComplete function
			// when the path has been calculated (which may take a few frames depending on the complexity)
			_seeker.StartPath(transform.position, target.position, OnPathComplete);
		}
		if (path == null)
		{
			//No path to fellow, dont do anything.
			return;
		}
		if (currentWaypoint > path.vectorPath.Count)
		{
			return;
		} 
		if (currentWaypoint == path.vectorPath.Count)
		{
			currentWaypoint++;
			return;
		}

		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
		dir *= speed;
		
        if(_kiteMode)
        {
            _rb2d.velocity = -dir;
        }
        else
        {
            _rb2d.velocity = dir;
        }

		// The commented line is equivalent to the one below, but the one that is used
		// is slightly faster since it does not have to calculate a square root
		//if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
		if ((transform.position - path.vectorPath[currentWaypoint]).sqrMagnitude < nextWaypointDistance * nextWaypointDistance)
		{
			currentWaypoint++;
			return;
		}
	}



	void ShootPlayer()
	{
		if (!_seePlayer)
			return;
        
        if (Time.time > _nextFire )
        {
            
            _nextFire = Time.time + fireRate;
            if (isMeleeUnit)
            {
                if (Vector3.Distance(this.transform.position, target.transform.position) > 3) return;
                GameObject bulletInstance = Instantiate(bullet, RayCastStart.position, RayCastStart.rotation) as GameObject;

                ParticleScript bulletScript;
                bulletScript = bulletInstance.GetComponent<ParticleScript>();
                bulletScript.setDamage(bulletDamage);
                bulletScript.attacker = "enemy";
                Animator anim = GetComponentInChildren<Animator>();
                anim.SetTrigger("Attack");
                

                Destroy(bulletInstance, Time.fixedDeltaTime);

                
            }
            else
            {
                GameObject bulletInstance = Instantiate(bullet, RayCastStart.position, Quaternion.Euler(0, 0, RayCastStart.eulerAngles.z + Random.Range(-Recoil, Recoil))) as GameObject;

                Bullet bulletScript;
                bulletScript = bulletInstance.GetComponent<Bullet>();
                bulletScript.setDamage(bulletDamage);

                Rigidbody2D bulletRB2D = bulletInstance.GetComponent<Rigidbody2D>();
                Vector3 velocity = bulletInstance.transform.rotation * Vector3.right;
                bulletRB2D.AddForce(velocity * attackSpeed);

                GameObject muzzle = Instantiate(muzzleFlash, RayCastStart.position, transform.rotation) as GameObject;
                Destroy(muzzle, Time.fixedDeltaTime);
            }
           
        }
        
	}
    public void TakeDamage(int amount)
    {
        _CurrentHealth -= amount;
        _following = true;
        if(_CurrentHealth <= 0)
        {
            die();
        }
    }
    void die()
    {
        DropItems();
		try
		{
			CustomJailGuardTutorial script = gameObject.GetComponent<CustomJailGuardTutorial>();
			script.GiveGuns();
		}
		catch (System.Exception e)
		{}

        Destroy(this.gameObject);
    }

    void DropItems()
    {
        float randValue = Random.value;
        foreach(GameObject drop in Drops)
        {
			if (isMeleeUnit) {
				HPpotion potScript = drop.GetComponent<HPpotion> ();
				if (randValue <= potScript.meleeDropChance) {
					GameObject dropInstance = Instantiate (drop, transform.position, Quaternion.Euler (0, 0, 0))as GameObject;
				}
			} else {
				HPpotion potScript = drop.GetComponent<HPpotion>();
				if(randValue <= potScript.dropChance)
				{
					GameObject dropInstance = Instantiate(drop, transform.position, Quaternion.Euler(0,0,0))as GameObject;
				}
			}
            
        }
    }

    public void EveryoneDead()
    {

    }

    void OnWaypointReached()
    {
        if(_currentWanderWaypoint >= Waypoints.Count)
        {
            _currentWanderWaypoint = 0;
            
        }

        if (Vector3.Distance(transform.localPosition, Waypoints[_currentWanderWaypoint].transform.position) < 1.5)
        {
            _currentWanderWaypoint++;
            _WaitingAtWaypoint = true;
            _rb2d.velocity = Vector2.zero;
        }
    }

    bool DoesRayContainTag(RaycastHit2D[] rayCastAll, string tag)
    {
        return false;
    }

    void wanderToWaypoints()
    {
        if (_following || WanderToWaypoints == false) return;

        if (Waypoints.Count == 0) return;

        if(_WaitingAtWaypoint)
        {
            _nextWaypoint += Time.deltaTime;
            if (_nextWaypoint >= waitAtWaypoint)
            {
                _nextWaypoint = Time.time + waitAtWaypoint;
                _WaitingAtWaypoint = false;
                _nextWaypoint = 0;
            }
            else
            {
                return;
            }
        }
        if (Time.time - lastRepath > repathRate && _seeker.IsDone())
        {
            lastRepath = Time.time + UnityEngine.Random.value * repathRate * 0.5f;
            _seeker.StartPath(transform.position, Waypoints[_currentWanderWaypoint].transform.position, OnPathComplete);

        }
        if (path == null)
        {
            return;
        }
        if (currentWaypoint > path.vectorPath.Count)
        {
            return;
        }
        if (currentWaypoint == path.vectorPath.Count)
        {
            currentWaypoint++;
            return;
        }

        
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        
        dir *= wanderSpeed;
        lookAtDir(dir);

        _rb2d.velocity = dir;
        
        

        if ((transform.position - path.vectorPath[currentWaypoint]).sqrMagnitude < nextWaypointDistance * nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
        OnWaypointReached();
    }



}
