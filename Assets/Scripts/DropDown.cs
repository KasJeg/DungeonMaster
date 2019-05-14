using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DropDown : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject listtext;
    public List<string> names;

    string text;

    void Start()
    {
        LevelsList();
        
    }
    void LevelsList()
    {
        names = new List<string>()
        {
            "world3","world2","SurviveMission","Level1"
        };
        dropdown.AddOptions(names);
        
    }
    public void OnClick()
    {
        text = listtext.GetComponent<Text>().text;
        Debug.Log(text);
        SceneManager.LoadScene(text);
        
    }

}
