using UnityEngine;
using System.Collections;

public class KeyRotator : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0, 100, 0) * Time.deltaTime);
    }
}
