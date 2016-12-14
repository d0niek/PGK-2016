using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public float hearingRange = 5.0f;
	public GameObject hearingRangeCircle;
	public float viewDistance = 10.0f;
	public float viewAngle = 30.0f;

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
		hearingRangeCircle.transform.localScale = new Vector3 (2 * hearingRange, 0.0001f, 2 * hearingRange);
	}

	void Update ()
	{
		if ((InHearingRange () || InViewRange ()) && playerHealth.currentHealth > 0) {
			navMeshAgent.SetDestination (player.transform.position);
		} else if (startPosition != transform.position) {
			navMeshAgent.SetDestination (startPosition);
		}
	}

	bool InHearingRange ()
	{
		float distance = Vector3.Distance (transform.position, player.transform.position);
		distance = Mathf.Abs (distance);

		return distance <= hearingRange;
	}

	bool InViewRange()
	{
		RaycastHit hit;
		Vector3 rayDirection = (player.transform.position - transform.position).normalized;

		if (Vector3.Angle (transform.forward, rayDirection) < viewAngle / 2) {
			if (Physics.Raycast (transform.position + transform.forward, rayDirection, out hit, viewDistance)) {
				Debug.Log (hit.transform.name + ", " + hit.transform.tag);
				return hit.transform.tag == "Player";
			}
		}

		return false;
	}
}
