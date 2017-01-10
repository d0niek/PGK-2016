using UnityEngine;
using System.Collections;

public class wallController : MonoBehaviour {

    Vector3 target;
    public bool isPlateOnePressed;
    public bool isPlateTwoPressed;

    void Start ()
    {
        target = new Vector3(transform.position.x, transform.position.y-10, transform.position.z);
	}
	void Update () {
        if(isPlateOnePressed && isPlateTwoPressed)
        transform.position = Vector3.MoveTowards(transform.position, target, 0.2f);
	}
}
