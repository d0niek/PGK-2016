using UnityEngine;
using System.Collections;

public class hiddenWallPlate2Controller : MonoBehaviour
{

    public GameObject hiddenwall;
    public GameObject hiddenwall2;
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
        {
            hiddenwall.GetComponent<hiddenWallController>().isPlateFourPressed = true;
            hiddenwall2.GetComponent<RightHiddenWallController>().isPlate2Pressed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Coworker"))
            hiddenwall.GetComponent<hiddenWallController>().isPlateFourPressed = false;
            hiddenwall2.GetComponent<RightHiddenWallController>().isPlate2Pressed = false;
    }
}
