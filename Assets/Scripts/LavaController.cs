using UnityEngine;
using System.Collections;

public class LavaController : MonoBehaviour {

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 25;

    PlayerHealth playerHealth;
    GameObject player;

    bool playerInRange;
    float timer;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange /*&& enemyHealth.currentHealth > 0*/)
        {
            Attack();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == player)
        {
            playerInRange = true;
        }
    }
    void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
