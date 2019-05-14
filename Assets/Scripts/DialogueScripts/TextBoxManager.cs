using player;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    public PlayerController player;
    public int TimeToDissapearTextBox = 4;
    public GameObject textBox;
    public Text theText;
    public float timer;
    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;


    private bool isCrunning = false;

	// Use this for initialization
	void Start ()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        if (textBox == null)
        {
            textBox = GameObject.FindGameObjectWithTag("DialogueBox");
        }
        if (theText == null)
        {
            theText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<Text>();
        }
        timer = TimeToDissapearTextBox;
        DontDestroyOnLoad(transform.gameObject);
        textBox.SetActive(false);

        importText();
        
        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
	}

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (textBox == null)
        {
            textBox = GameObject.FindGameObjectWithTag("DialogueBox");
        }
        if (theText == null)
        {
            theText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<Text>();
        }
        
        textBox.SetActive(false);
        timer = TimeToDissapearTextBox;
    }

    // Update is called once per frame
    void Update () {
        
        if(textBox != null)
        {
            DecreaseTime();
        }
        //if (Input.GetKey(KeyCode.F)) textBox.SetActive(false);
    }

    public void importText()
    {
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
    }
    
    public void DialogueTalk(int startline, int endline)
    {
        if (isCrunning) return;
        textBox.SetActive(true);
        currentLine = startline;
        endAtLine = endline;
        
        StartCoroutine(UpdateText(textLines[startline]));
        timer = TimeToDissapearTextBox;
    }

    IEnumerator UpdateText(string text)
    {
        isCrunning = true;
        int i = 0;
        theText.text = "";
        while(i < text.Length)
        {
            theText.text += text[i++];
            yield return new WaitForSeconds(0.02f);
        }
        isCrunning = false;
    }
    private void DecreaseTime()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            textBox.SetActive(false);
            timer = TimeToDissapearTextBox;
        }
    }
}
