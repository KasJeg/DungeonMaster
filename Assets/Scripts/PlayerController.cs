using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace player
{
    public class PlayerController : MonoBehaviour
    {
        
        public float speed;
        private Rigidbody2D rg2d;
        public Image Pilt;
        public Text Tekst;
        TextBoxManager DialogueManager;
        public int rotationOffset = 0;
        public GameObject EndScreen;

        public bool DisableW = false;
        public bool DisableA = false;
        public bool DisableS = false;
        public bool DisableD = false;
        public bool DisableAssaultRifle = false;
        public bool DisableShotgun = false;
        public bool DisableRocketLauncher = false;
        public bool DisableMouse = false;
        PlayerSettings persistentScript;
        
        


        // Use this for initialization
        void Start()
        {
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
            Pilt = GameObject.FindGameObjectWithTag("HealthIcon").GetComponent<Image>();
            Tekst = GameObject.FindGameObjectWithTag("HealthBarText").GetComponent<Text>();
            try
            {
                persistentScript = GameObject.Find("PlayerSettings").GetComponent<PlayerSettings>();
            }
            catch
            {
                Debug.LogWarning("Could not find persistentScript at PlayerController, Instating it.");
                persistentScript = Instantiate(Resources.Load("GlobalGameObjects/PlayerSettings") as GameObject).GetComponent<PlayerSettings>();
                persistentScript.name = "PlayerSettings";
            }

            try
            {
                DialogueManager = GameObject.Find("TextBoxManager").GetComponent<TextBoxManager>();
            }
            catch
            {
                Debug.LogWarning("Could not find DialogueManager at PlayerController, Instating it.");
                DialogueManager = Instantiate(Resources.Load("GlobalGameObjects/TextBoxManager") as GameObject).GetComponent<TextBoxManager>();
                DialogueManager.name = "TextBoxManager";
            }

            rg2d = GetComponent<Rigidbody2D>();
            
            DisableW = persistentScript.DisableW;
            DisableA = persistentScript.DisableA;
            DisableS = persistentScript.DisableS;
            DisableD = persistentScript.DisableD;
            DisableAssaultRifle = persistentScript.DisableAssaultRifle;
            DisableRocketLauncher = persistentScript.DisableRocketLauncher;
            DisableShotgun = persistentScript.DisableShotgun;
        }

        private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
           
        }


        // Update is called once per frame
        void Update()
        { 

            Tekst.text = persistentScript.PlayerHealth.ToString();
            CalcHealth();
            followMouse();

            if (rg2d.velocity != Vector2.zero)
            {
                rg2d.velocity = Vector2.zero;
            }
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && !DisableW)
            {
                moveUp();
            }
            else if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && DisableW)
            {
                DialogueManager.DialogueTalk(1, 0);
            }


            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !DisableS)
            {
                moveDown();
            }
            else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && DisableS)
            {
                DialogueManager.DialogueTalk(2, 0);
            }


            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !DisableA)
            {
                moveLeft();
            }
            else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && DisableA)
            {
                DialogueManager.DialogueTalk(3, 0);
            }



            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !DisableD)
            {
                moveRight();
            }
            else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && DisableD)
            {
                DialogueManager.DialogueTalk(4, 0);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeWeapon();
            }
        }

        void FixedUpdate()
        {
            
            
        }
		Vector3 direction = new Vector3(0,0,0);

        void moveUp()
        {
			rg2d.AddForce(Vector3.up * speed * Time.deltaTime * 5000);
        }
        void moveDown()
        {
			rg2d.AddForce(Vector3.down * speed * Time.deltaTime * 5000);
        }
        void moveLeft()
        {
			rg2d.AddForce(Vector3.left * speed * Time.deltaTime * 5000);
        }
        void moveRight()
        {
			rg2d.AddForce(Vector3.right * speed * Time.deltaTime * 5000);
        }

        // Update is called once per frame
        void followMouse()
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);

        }
        void CalcHealth()
        {

            float calche = ((float)persistentScript.PlayerHealth / persistentScript.PlayerMaxHealth);
            SetHealthbar(calche);
        }
        void SetHealthbar(float myhealth)
        {
            Pilt.transform.localScale = new Vector2(myhealth, Pilt.transform.localScale.y);
        }

        public void TakeDamage(int amount)
        {
            persistentScript.PlayerHealth -= amount;


            Debug.Log(persistentScript.PlayerHealth);
            if (persistentScript.PlayerHealth <= 0)
            {
                die();
            }
        }
        void die()
        {
            Debug.Log("Oops, my system crashed!");
            Destroy(this.gameObject);
            if (SceneManager.GetActiveScene().name == "SurviveMission")
            {
                Debug.Log("xd");
                EndScreen = GameObject.Find("EndScreen");
                EndScreen.GetComponentsInChildren<RawImage>(true)[0].enabled = true;
                return;
            }
            else
            {
                NextScene sceneChanger = new NextScene();
                sceneChanger.persistentScript = persistentScript;
                persistentScript.PlayerHealth = persistentScript.PlayerMaxHealth;
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
            }


            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public bool heal(int amount)
        {
            if (persistentScript.PlayerHealth < persistentScript.PlayerMaxHealth)
            {
                if ((persistentScript.PlayerHealth + amount) >= persistentScript.PlayerMaxHealth)
                {
                    persistentScript.PlayerHealth = persistentScript.PlayerMaxHealth;
                }
                else
                {
                    persistentScript.PlayerHealth += amount;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        
        private void ChangeWeapon()
        {
            //Assaultrifle = 0
            //Shotgun = 1
            //Rocket Launcher = 2
			if(DisableAssaultRifle && DisableRocketLauncher && DisableShotgun) return;
            if(persistentScript.WeaponID == 2)
            {
                if(DisableAssaultRifle)
                {
                    persistentScript.WeaponID = 0;
                    ChangeWeapon();
                    return;
                }
                WeaponChanger.changeWeapon("assaultrifle", gameObject);
                persistentScript.WeaponID = 0;
                return;
            }
            else if(persistentScript.WeaponID == 0)
            {
                if(DisableShotgun)
                {
                    persistentScript.WeaponID++;
                    ChangeWeapon();
                    return;
                }
                WeaponChanger.changeWeapon("shotgun", gameObject);
                persistentScript.WeaponID = 1;
                
            }
            else if(persistentScript.WeaponID == 1)
            {
                if(DisableRocketLauncher)
                {
                    persistentScript.WeaponID++;
                    ChangeWeapon();
                    return;
                }
                WeaponChanger.changeWeapon("rocketlauncher", gameObject);
                persistentScript.WeaponID = 2;
                return;
            }
            
        }
        private void ChangeWeapon(string weapon)
        {
            WeaponChanger.changeWeapon(weapon, gameObject);
            persistentScript.currentWeapon = weapon;
        }


    }
}






