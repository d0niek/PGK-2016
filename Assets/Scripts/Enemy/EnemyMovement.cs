using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public float stunnedTime = 5f;

	FieldOfView fow;
	AreaOfHearing aoh;
	NavMeshAgent navMeshAgent;
	Vector3 positionA;
	Vector3 positionB;
	bool isOnPositionA;
	bool isOnPositionB;
	bool targetInAttackRange;
	bool isStunned;
	bool isRunning;
	bool isWalking;
	float timer;
	Animator anim;

	void Awake ()
	{
		fow = GetComponent <FieldOfView> ();
		aoh = GetComponent<AreaOfHearing> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		positionA = transform.position;
		positionB = transform.Find ("Patrol Point").position;
		isOnPositionA = true;
		isOnPositionB = false;
		targetInAttackRange = false;
		isStunned = false;
		timer = 0f;
		anim = GetComponentInChildren<Animator> ();
	}

	void Update ()
	{
		timer += Time.deltaTime;

		if (timer > stunnedTime) {
			isStunned = false;
		}
		isRunning = false;
		isWalking = false;

		if (fow.targetInFieldOfView != null && targetInAttackRange == false && isStunned == false) {
			navMeshAgent.SetDestination (fow.targetInFieldOfView.position);
			isRunning = true;
		} else if (aoh.targetInAreaOfHearing != null && targetInAttackRange == false && isStunned == false) {
			navMeshAgent.SetDestination (aoh.targetInAreaOfHearing.position);
			isRunning = true;
		} else {
			Patrol ();
		}

		anim.SetBool ("IsStun", isStunned);
		anim.SetBool ("IsRunning", isRunning);
		anim.SetBool ("IsWalking", isWalking);
	}

	void Patrol()
	{
		if (positionA == positionB && Vector3.SqrMagnitude (positionA - transform.position) < 0.01) {
			return;
		}

		if (Vector3.SqrMagnitude (positionB - transform.position) < 0.01) {
			isOnPositionB = true;
			isOnPositionA = false;
		} else if (Vector3.SqrMagnitude (positionA - transform.position) < 0.01) {
			isOnPositionA = true;
			isOnPositionB = false;
		}

		if (Vector3.SqrMagnitude (positionB - transform.position) > 0.01 && isOnPositionA == true) {
			navMeshAgent.SetDestination (positionB);
			isWalking = true;
		} else if (Vector3.SqrMagnitude (positionA - transform.position) > 0.01 && isOnPositionB == true) {
			navMeshAgent.SetDestination (positionA);
			isWalking = true;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Coworker")) {
			targetInAttackRange = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Coworker")) {
			targetInAttackRange = false;
		}
	}

	public void Stun()
	{
		isStunned = true;
		timer = 0f;
	}

	public bool IsStun()
	{
		return isStunned;
	}
}
