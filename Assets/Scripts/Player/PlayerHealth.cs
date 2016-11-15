using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;                                  
	public Slider healthSlider;
	public Image damageImage;                             
	public float flashSpeed = 5f;                             
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);   

	PlayerMovement playerMovement;                              // Reference to the player's movement.
	//PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.

	void Awake ()
	{
		playerMovement = GetComponent <PlayerMovement> ();
		//playerShooting = GetComponentInChildren <PlayerShooting> ();

		currentHealth = startingHealth;
	}

	void Update ()
	{
		if(damaged) {
			damageImage.color = flashColour;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		// Reset the damaged flag.
		damaged = false;
	}
		
	public void TakeDamage (int amount)
	{
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;

		if(currentHealth <= 0 && !isDead) {
			Death ();
		}
	}

	void Death ()
	{
		isDead = true;

		//playerShooting.DisableEffects ();

		playerMovement.enabled = false;
		//playerShooting.enabled = false;
	}       
}
