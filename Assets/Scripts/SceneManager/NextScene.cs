using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum List
{
    tutorial,
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
    None,
    Main,
    world2,
    SurviveMission,
	TeachWeapons,
	TeachWeapon2
}



public class NextScene : MonoBehaviour {

    private bool PressingF = false;
    public PlayerSettings persistentScript;
    void Start()
    {
        
    }
    void Update()
    {
        if (persistentScript == null)
        {
            try
            {
                persistentScript = GameObject.Find("PlayerSettings").GetComponent<PlayerSettings>();
            }
            catch
            {
                Debug.LogWarning("PlayerSettings not found");
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PressingF = true;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            PressingF = false;
        }
    }

    public List list;
    

    void OnTriggerStay2D(Collider2D coll)
    {
        if (list.ToString() == "Main") return;
        

        if(PressingF && coll.gameObject.tag == "Player")
        {
            GoToNextScene(list.ToString());
        }
    }

    public void GoToNextScene(string scene)
    {
		if (SceneManager.GetActiveScene ().name == "TeachWeapons") {
			if (GameObject.FindGameObjectWithTag ("Enemy") != null) {
				GameObject.Find ("TextBoxManager").GetComponent<TextBoxManager> ().DialogueTalk (9, 0);
				return;
			}
		}
		if (scene == "tutorial") {
			if (persistentScript.DisableA == true) {
				scene = "Level2";
				persistentScript.DisableA = false;
			} else if (persistentScript.DisableW == true) {
				scene = "Level3";
				persistentScript.DisableW = false;
			} else if (persistentScript.DisableS == true) {
				scene = "Level4";
				persistentScript.DisableS = false;
			} else if (persistentScript.DisableS == false && SceneManager.GetActiveScene ().name == "Level4") {
				scene = "Level1";
			} else if (persistentScript.DisableA == false && SceneManager.GetActiveScene ().name == "Level1") {
				GameObject.Find ("TextBoxManager").GetComponent<TextBoxManager> ().DialogueTalk (9, 0);
				return;
			}
            
			SceneManager.LoadScene (scene);
		} 
		else if (scene == "SurviveMission") {
			persistentScript.DisableMouse = false;
			persistentScript.DisableAssaultRifle = false;
			persistentScript.DisableRocketLauncher = false;
			persistentScript.DisableShotgun = false;
			SceneManager.LoadScene (scene);
		}
        else if(SceneManager.GetActiveScene().name == "SurviveMission")
        {
            var spawners = GameObject.FindGameObjectsWithTag("Spawner");
            foreach (var spawner in spawners)
            {
                if (spawner.GetComponent<EnemySpawner>().areAllDead() != true)
                {
                    try
                    {
                        var DialogueManager = GameObject.Find("TextBoxManager").GetComponent<TextBoxManager>();
                        DialogueManager.DialogueTalk(8, 0);
                        DialogueManager.textBox.SetActive(true);
                    }
                    catch
                    {
                        Debug.Log("No DialogueManager/TextBoxManager found");
                    }
                    return;
                }
            }
            SceneManager.LoadScene(scene);
        }
        else
        {
            SceneManager.LoadScene(list.ToString());
        }
        
        
        
    }

   
}
