using UnityEngine;
using System.Collections;

public class hiddenWallController : MonoBehaviour
{

    Vector3 target;
    public bool isPlateThreePressed;
    public bool isPlateFourPressed;
    

    void Start()
    {
        target = new Vector3(transform.position.x - 20, transform.position.y, transform.position.z);
    }
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        PlayerController accessor = player.GetComponent<PlayerController>();

        if (accessor.keyCount == accessor.keyAmount)
        {
            if (isPlateThreePressed && isPlateFourPressed)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, 0.1f);
            }
        }
    }
}
