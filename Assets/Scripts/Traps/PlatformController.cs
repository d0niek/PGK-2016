using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {

    public float speed = 1;
    private int direction = 1;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);
	}

    void OnTriggerEnter (Collider other)
    {
        if(other.tag == "PlatformTarget")
        {
            if (direction == 1)
                direction = -1;
            else
                direction = 1;
        }

        if(other.tag == "Player")
        {
            other.transform.parent = transform;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
