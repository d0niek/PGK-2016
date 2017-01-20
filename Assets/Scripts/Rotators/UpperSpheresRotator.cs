using UnityEngine;
using System.Collections;

public class UpperSpheresRotator : MonoBehaviour {

    public GameObject tower;
    public float speed;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float translation = speed * Time.deltaTime;
        transform.RotateAround(tower.transform.position, Vector3.down, translation);
    }
}
