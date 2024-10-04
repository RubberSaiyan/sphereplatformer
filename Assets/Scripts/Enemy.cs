using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask isGround, isPlayer;

    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkRange;

    public float sightRange;
    public bool playerInRange;

    public EnemyHead head;

    public ParticleSystem deathParticle;

    void Start()
    {
        
    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        playerInRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);

        if (playerInRange == true)
        {
            Chase();
        }
        if (playerInRange == false)
        {
            Patrol();
        }

        if (head.hit == true)
        {
            Destroy();
        }
    }

    public void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkRange, walkRange);
        float randomX = Random.Range(-walkRange, walkRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, isGround))
        {
            walkPointSet = true;
        }
    }

    public void Patrol()
    {
        if (walkPointSet == false)
        {
            SearchWalkPoint();
        }
        if (walkPointSet == true)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 walkPointDistance = transform.position - walkPoint;

        if(walkPointDistance.magnitude < 10f)
        {
            walkPointSet = false;
        }
    }

    public void Chase()
    {
        agent.SetDestination(player.position);
    }

    public void Destroy()
    {
        Instantiate(deathParticle, transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(gameObject);
    }
}
