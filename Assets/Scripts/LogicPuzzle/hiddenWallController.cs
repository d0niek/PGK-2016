using UnityEngine;
using System.Collections;

public class hiddenWallController : MonoBehaviour
{

    Vector3 target;
    public bool isPlateThreePressed;
    public bool isPlateFourPressed;
    

    void Start()
    {
        target = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
    }
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        PlayerController accessor = player.GetComponent<PlayerController>();

        if (accessor.keyCount == 5)
        {
            if (isPlateThreePressed && isPlateFourPressed)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, 0.2f);
            }
        }
    }
}
