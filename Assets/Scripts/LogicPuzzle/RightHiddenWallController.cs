using UnityEngine;
using System.Collections;

public class RightHiddenWallController : MonoBehaviour
{
    Vector3 target;
    public bool isPlate1Pressed;
    public bool isPlate2Pressed;


    void Start()
    {
        target = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
    }
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        PlayerController accessor = player.GetComponent<PlayerController>();

        if (accessor.keyCount == 4)
        {
            if (isPlate1Pressed && isPlate2Pressed)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, 0.1f);
            }
        }
    }
}
