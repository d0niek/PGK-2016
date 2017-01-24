using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OnClickNewGame : MonoBehaviour {

    public void OpenMainLevel()
    {
        SceneManager.LoadScene("Main_Level");
    }
}
