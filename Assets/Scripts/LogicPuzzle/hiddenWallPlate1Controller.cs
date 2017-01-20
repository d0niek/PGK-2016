using UnityEngine;
using System.Collections;

public class hiddenWallPlate1Controller : MonoBehaviour {
    public GameObject hiddenwall;

	// Use this for initialization
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Coworker"))
            hiddenwall.GetComponent<hiddenWallController>().isPlateThreePressed = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Coworker"))
            hiddenwall.GetComponent<hiddenWallController>().isPlateThreePressed = false;
    }
}
