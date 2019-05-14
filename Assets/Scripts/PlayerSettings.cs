using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSettings : MonoBehaviour {

    public bool DisableW = false;
    public bool DisableA = false;
    public bool DisableS = false;
    public bool DisableD = false;
    public bool DisableAssaultRifle = false;
    public bool DisableShotgun = false;
    public bool DisableRocketLauncher = false;
    public bool DisableMouse = false;
    public string currentWeapon { get; set; }
    public GameObject currentPlayer { get; set; }
    public int PlayerHealth { get; set; }
    public int PlayerMaxHealth { get; set; }
    public int WeaponID { get; set; }
    

    // Use this for initialization
    void Start () {
        WeaponID = 0;
        PlayerHealth = 100;
        PlayerMaxHealth = 100;
        if(currentPlayer == null)
        {
            currentPlayer = GameObject.FindGameObjectWithTag("Player");
        }
        DontDestroyOnLoad(transform.gameObject);
        currentWeapon = "assaultrifle";
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
	}

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
		WeaponID = 0;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
