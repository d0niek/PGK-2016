using UnityEngine;
using System.Collections;

public class ParticleSystemController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("colliosion");
            gameObject.SetActive(false);
        }
    }
}
