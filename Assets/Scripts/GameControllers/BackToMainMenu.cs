using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public class BackToMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	        if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnClickBackToMainMenu();
            }
	}

    public void OnClickBackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
