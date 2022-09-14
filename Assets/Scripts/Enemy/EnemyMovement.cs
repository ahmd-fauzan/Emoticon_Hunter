using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform target;

    NavMeshAgent agent;

    EnemyHealth enemyHealth;
    PlayerHealth playerHealth;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        playerHealth = target.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.enabled = false;
        }
        
    }
}
