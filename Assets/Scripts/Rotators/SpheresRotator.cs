using UnityEngine;
using System.Collections;

public class SpheresRotator : MonoBehaviour {

    public GameObject tower;
    public float speed;

    void Update()
    {
        float translation = speed * Time.deltaTime;
        transform.RotateAround(tower.transform.position, Vector3.up, translation);
    }
}
