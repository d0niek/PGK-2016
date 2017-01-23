using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FollowerMovement : MonoBehaviour
{
    Transform player;
    NavMeshAgent nav;
    bool shouldStop;
    ParticleSystem exp;
    Vector3 vec;
    Renderer[] childrenRenderer;
    Dictionary<string, GameObject> enemies;
    GameObject targetIndicator;
    Image cooldownButton;
    public AudioClip[] audioClip;
    private float timeStamp;
    public float cooldownPeriod = 4.0f;

    void Start()
    {
        childrenRenderer = gameObject.GetComponentsInChildren<MeshRenderer>();
        vec = new Vector3(0, 0, -3);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cooldownButton = GameObject.FindGameObjectWithTag("BurstButton").GetComponent<Image>();
        targetIndicator = GameObject.FindGameObjectWithTag("TargetIndicator");
        nav = GetComponent<NavMeshAgent>();
        exp = GetComponentInChildren<ParticleSystem>();
        enemies = new Dictionary<string, GameObject>();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (timeStamp <= Time.time)
            {
                timeStamp = Time.time + cooldownPeriod;
                reloadSkill();
                StunEnemies();
                StartCoroutine(appearAgain());
                explosionSound(0);                
            }
        }
    }

    void Update()
    {
        if (nav.remainingDistance < 1)
            targetIndicator.SetActive(false);

        if (!shouldStop)
        {
            followPlayer();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            targetIndicator.SetActive(true);
            GoTo();
        }

        if (Input.GetKeyDown(KeyCode.B))
            shouldStop = false;
    }

    void reloadSkill ()
    {
        cooldownButton.fillAmount = 0;
        cooldownButton.color = new Color(255, 0, 0);

        StartCoroutine(startReloadingSkill());
    }

    IEnumerator startReloadingSkill()
    {
        while (timeStamp >= Time.time)
        {
            cooldownButton.fillAmount += 0.025f;
            yield return new WaitForSecondsRealtime(0.2f);
            cooldownButton.fillAmount += 0.025f;
        }
        cooldownButton.fillAmount = 1;
        cooldownButton.color = new Color(255, 255, 255);
    }

    void StunEnemies()
    {
        foreach (GameObject enemy in enemies.Values)
        {
            EnemyMovement em = enemy.GetComponent<EnemyMovement>();
            em.Stun();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && enemies.ContainsKey(other.gameObject.name) == false)
        {
            enemies.Add(other.gameObject.name, other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject.name);
        }
    }

    IEnumerator appearAgain()
    {
        exp.Play();
        foreach (MeshRenderer abc in childrenRenderer)
            abc.enabled = false;

        yield return new WaitForSeconds(1);

        nav.Warp(player.position + vec);
        foreach (MeshRenderer abc in childrenRenderer)
            abc.enabled = true;

        shouldStop = false;
    }

    void followPlayer()
    {
        nav.SetDestination(player.position + vec);
        nav.stoppingDistance = 3;
    }

    public void GoTo()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            nav.stoppingDistance = 1;
            nav.SetDestination(hit.point);
            var transform = targetIndicator.GetComponent<Transform>();
            transform.position = hit.point;
        }

        shouldStop = true;
    }

    public void explosionSound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().Play();
    }
}
