using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OnClickTutorialEnter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OpenTutorialInMainMenu()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OpenTutorialInGame()
    {
        SceneManager.LoadScene("PauseTuttorial");
    }
}
