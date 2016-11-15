using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public float range = 10.0f;

	GameObject player;
	PlayerHealth playerHealth;
	NavMeshAgent navMeshAgent;
	Vector3 startPosition;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		startPosition = transform.position;
	}

	void Update ()
	{
		if (InRange () && playerHealth.currentHealth > 0) {
			navMeshAgent.SetDestination (player.transform.position);
		} else if (startPosition != transform.position) {
			navMeshAgent.SetDestination (startPosition);
		}
	}

	bool InRange ()
	{
		float distance = Vector3.Distance (transform.position, player.transform.position);
		distance = Mathf.Abs (distance);

		return distance <= range;
	}
}
