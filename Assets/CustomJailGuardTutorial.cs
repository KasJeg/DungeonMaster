using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using player;


public class CustomJailGuardTutorial : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void GiveGuns()
	{
		var persistentScript = GameObject.Find("PlayerSettings").GetComponent<PlayerSettings>();
		persistentScript.DisableAssaultRifle = false;
		persistentScript.DisableRocketLauncher = false;
		persistentScript.DisableShotgun = false;
		persistentScript.DisableMouse = false;
		WeaponChanger.changeWeapon ("assaultrifle", GameObject.FindGameObjectWithTag("Player"));
		persistentScript.currentWeapon = "assaultrifle";
		var text = GameObject.Find ("TextBoxManager").GetComponent<TextBoxManager>();
		text.DialogueTalk (10, 0);
	}
}
