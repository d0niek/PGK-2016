using UnityEngine;
using System.Collections;

public class CoworkerMovement : MonoBehaviour
{
	Transform player;
	NavMeshAgent nav;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
	}

	void Update ()
	{
		nav.SetDestination (player.position);
	}
}