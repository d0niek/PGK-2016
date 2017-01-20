using UnityEngine;
using System.Collections;

public class hiddenWallPlate2Controller : MonoBehaviour
{

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
            hiddenwall.GetComponent<hiddenWallController>().isPlateFourPressed = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Coworker"))
            hiddenwall.GetComponent<hiddenWallController>().isPlateFourPressed = false;
    }
}
