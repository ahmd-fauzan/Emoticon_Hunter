using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damagePerHit = 10;

    public Animator anim;

    public float TimeBetweenAttack = 0.5f;

    GameObject player;

    PlayerHealth playerHealth;

    bool playerInRange;

    float timer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (playerInRange && timer >= TimeBetweenAttack)
        {
            Attack();
        }
        if (!playerInRange)
        {
            anim.SetBool("IsWalking", true);
        }
            

    }

    void Attack()
    {
        timer = 0;

        if(playerHealth.currentHealth >= 0)
        {
            playerHealth.TakeDamage(damagePerHit);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
            anim.SetBool("IsWalking", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            
        }
    }
}
