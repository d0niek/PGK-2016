using UnityEngine;
using System.Collections;

public class explosionController : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnParticleCollision (GameObject other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
