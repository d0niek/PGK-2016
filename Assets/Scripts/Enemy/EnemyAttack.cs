using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 25;

	GameObject player;
	PlayerHealth playerHealth;
	//EnemyHealth enemyHealth;
	bool playerInRange;
	float timer;
	Animator anim;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		//enemyHealth = GetComponent<EnemyHealth>();
		anim = GetComponentInChildren<Animator> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player) {
			playerInRange = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject == player) {
			playerInRange = false;
		}
	}

	void Update ()
	{
		timer += Time.deltaTime;

		if (timer >= timeBetweenAttacks && playerInRange /*&& enemyHealth.currentHealth > 0*/ && IsStunned() == false) {
			Attack ();
		} else if (playerInRange == false) {
			anim.SetBool ("IsAttacking", false);
		}
	}

	bool IsStunned()
	{
		return GetComponent<EnemyMovement> ().IsStun ();
	}

	void Attack ()
	{
		timer = 0f;

		if (playerHealth.currentHealth > 0) {
			anim.SetBool ("IsStun", false);
			anim.SetBool ("IsRunning", false);
			anim.SetBool ("IsAttacking", true);
			playerHealth.TakeDamage (attackDamage);
		}
	}
}
