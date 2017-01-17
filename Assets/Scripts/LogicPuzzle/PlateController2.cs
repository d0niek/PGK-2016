using UnityEngine;
using System.Collections;

public class PlateController2 : MonoBehaviour
{

    public GameObject wall;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Follower"))
            wall.GetComponent<wallController>().isPlateTwoPressed = true;
    }

    void OnTriggerExit(Collider other)
    {
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Follower"))
            wall.GetComponent<wallController>().isPlateTwoPressed = false;
    }
}
