﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CallPauseScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("PauseGame", LoadSceneMode.Additive);
        }
	}
}
