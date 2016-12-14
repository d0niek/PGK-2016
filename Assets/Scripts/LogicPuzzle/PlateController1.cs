using UnityEngine;
using System.Collections;

public class PlateController1 : MonoBehaviour {

    public GameObject wall;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
    
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Coworker"))
        wall.GetComponent<wallController>().isPlateOnePressed = true;
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Coworker"))
            wall.GetComponent<wallController>().isPlateOnePressed = false;
    }
}
