using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColour = new Color (1f, 0f, 0f, 0.1f);
    SceneManager sManager;

	PlayerMovement playerMovement;
	//PlayerShooting playerShooting;
	bool isDead;
	bool damaged;

	void Awake ()
	{
		playerMovement = GetComponent <PlayerMovement> ();
		//playerShooting = GetComponentInChildren <PlayerShooting> ();

		currentHealth = startingHealth;
	}

	void Update ()
	{
		damageImage.color = damaged ?
			flashColour :
			Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

		damaged = false;
	}

	public void TakeDamage (int amount)
	{
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;

		if (currentHealth <= 0 && !isDead) {
			Death ();
		}
	}

	void Death ()
	{
		isDead = true;

		playerMovement.enabled = false;
        SceneManager.LoadScene("Death");
		//playerShooting.enabled = false;
	}
}
