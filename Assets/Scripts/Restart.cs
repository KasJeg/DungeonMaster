using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{

    public void OnClick()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}