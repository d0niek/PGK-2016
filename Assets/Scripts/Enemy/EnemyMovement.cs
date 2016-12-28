using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	FieldOfView fow;
	AreaOfHearing aoh;
	NavMeshAgent navMeshAgent;
	Vector3 startPosition;

	void Start ()
	{
		fow = GetComponent <FieldOfView> ();
		aoh = GetComponent<AreaOfHearing> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		startPosition = transform.position;
	}

	void Update ()
	{
		if (fow.targetInFieldOfView != null) {
			navMeshAgent.SetDestination (fow.targetInFieldOfView.position);
		} else if (aoh.targetInAreaOfHearing != null) {
			navMeshAgent.SetDestination (aoh.targetInAreaOfHearing.position);
		} else if (startPosition != transform.position) {
			navMeshAgent.SetDestination (startPosition);
		}
	}
}
