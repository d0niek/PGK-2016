using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public float stunnedTime = 3f;

	FieldOfView fow;
	AreaOfHearing aoh;
	NavMeshAgent navMeshAgent;
	Vector3 startPosition;
	bool isStunned;
	float timer;

	void Start ()
	{
		fow = GetComponent <FieldOfView> ();
		aoh = GetComponent<AreaOfHearing> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		startPosition = transform.position;
		isStunned = false;
		timer = 0f;
	}

	void Update ()
	{
		timer += Time.deltaTime;

		if (isStunned && timer < stunnedTime) {
			return;
		}

		isStunned = false;
		timer = 0f;

		if (fow.targetInFieldOfView != null) {
			navMeshAgent.SetDestination (fow.targetInFieldOfView.position);
		} else if (aoh.targetInAreaOfHearing != null) {
			navMeshAgent.SetDestination (aoh.targetInAreaOfHearing.position);
		} else if (startPosition != transform.position) {
			navMeshAgent.SetDestination (startPosition);
		}
	}

	public void Stun()
	{
		isStunned = true;
	}
}
