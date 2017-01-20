using UnityEngine;
using System.Collections;

public class MagicalSphereAttack : MonoBehaviour {

    public float timeBetweenAttacks = 0.8f;
    public int attackDamage = 25;

    GameObject player;
    PlayerHealth playerHealth;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
