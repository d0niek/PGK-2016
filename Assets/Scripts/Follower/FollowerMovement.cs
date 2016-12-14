using UnityEngine;
using System.Collections;

public class CoworkerMovement : MonoBehaviour
{
    Transform player;
    NavMeshAgent nav;
    bool shouldStop;
    ParticleSystem exp;
    Vector3 vec;
    Renderer[] childrenRenderer;

    void Start ()
    {
        childrenRenderer = gameObject.GetComponentsInChildren<MeshRenderer>();
        vec = new Vector3(0, 0, -3);
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        nav = GetComponent<NavMeshAgent> ();
        exp = GetComponentInChildren<ParticleSystem>();
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * (GetComponent<Rigidbody>().mass * Mathf.Abs(Physics.gravity.y)));
    }
	
	void Update ()
    {
        if(!shouldStop)
        {
            followPlayer();
        }

        if (Input.GetKeyDown(KeyCode.F))
            GoTo();

        if (Input.GetKeyDown(KeyCode.B))
            shouldStop = false;

        if (Input.GetKeyDown(KeyCode.G))
            StartCoroutine(appearAgain());
    }

    IEnumerator appearAgain()
    {
        exp.Play();
        foreach (MeshRenderer abc in childrenRenderer)
            abc.enabled = false;

        yield return new WaitForSeconds(1);

        nav.Warp(player.position + vec);
        foreach (MeshRenderer abc in childrenRenderer)
            abc.enabled = true;

        shouldStop = false;
    }

    void followPlayer()
    {
        nav.SetDestination(player.position + vec);
        nav.stoppingDistance = 3;
    }

    public void GoTo()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            nav.stoppingDistance = 1;
            nav.SetDestination(hit.point);
        }

        shouldStop = true;
    }
}
