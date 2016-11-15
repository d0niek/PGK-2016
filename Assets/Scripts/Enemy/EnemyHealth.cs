using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 100;          
	public int currentHealth;                        
	public int scoreValue = 10;         

	CapsuleCollider capsuleCollider;        
	bool isDead;                                                 

	void Awake ()
	{
		capsuleCollider = GetComponent <CapsuleCollider> ();
		currentHealth = startingHealth;
	}

	void Update ()
	{
	}

	public void TakeDamage (int amount, Vector3 hitPoint)
	{
		if (isDead) {
			return;
		}

		currentHealth -= amount;

		if (currentHealth <= 0) {
			Death ();
		}
	}
		
	void Death ()
	{
		isDead = true;

		// Turn the collider into a trigger so shots can pass through it.
		capsuleCollider.isTrigger = true;
	}

	public void StartSinking ()
	{
		// Find and disable the Nav Mesh Agent.
		GetComponent <NavMeshAgent> ().enabled = false;

		// Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
		GetComponent <Rigidbody> ().isKinematic = true;

		// Increase the score by the enemy's score value.
		//ScoreManager.score += scoreValue;

		Destroy (gameObject, 2f);
	}
}
