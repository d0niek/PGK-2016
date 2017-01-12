using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class backToMainMenu : MonoBehaviour {

    public void backToMenu ()
    {
        SceneManager.LoadScene("Menu");
    }
}
