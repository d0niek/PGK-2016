using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public float stunnedTime = 5f;

	FieldOfView fow;
	AreaOfHearing aoh;
	NavMeshAgent navMeshAgent;
	Vector3 startPosition;
	bool targetInAttackRange;
	bool isStunned;
	bool isRunning;
	float timer;
	Animator anim;

	void Start ()
	{
		fow = GetComponent <FieldOfView> ();
		aoh = GetComponent<AreaOfHearing> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		startPosition = transform.position;
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

		if (fow.targetInFieldOfView != null && targetInAttackRange == false && isStunned == false) {
			navMeshAgent.SetDestination (fow.targetInFieldOfView.position);
			isRunning = true;
		} else if (aoh.targetInAreaOfHearing != null && targetInAttackRange == false && isStunned == false) {
			navMeshAgent.SetDestination (aoh.targetInAreaOfHearing.position);
			isRunning = true;
		} else if (startPosition != transform.position && targetInAttackRange == false && isStunned == false) {
			navMeshAgent.SetDestination (startPosition);
			isRunning = true;
		}

		anim.SetBool ("IsStun", isStunned);
		anim.SetBool ("IsRunning", isRunning);
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
