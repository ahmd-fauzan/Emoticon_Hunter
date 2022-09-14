using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{

    public int startingHealth = 100;
    public int currentHealth;

    public string name;

    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;

    EnemyMovement enemyMovement;
    EnemyManager enemyManager;

    public Animator anim;

    Animator myAnim;

    ParticleSystem hitParticle;
    SphereCollider collider;

    bool isDead;
    bool isSinking;

    // Start is called before the first frame update
    void Start()
    {
        hitParticle = GetComponentInChildren<ParticleSystem>();
        collider = GetComponent<SphereCollider>();
        GetComponent<NavMeshAgent>().enabled = true;
        enemyMovement = GetComponent<EnemyMovement>();
        enemyManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();
        myAnim = GetComponent<Animator>();
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public int TakeDamage(int amount, Vector3 point)
    {
        if (isDead)
            return 0;

        currentHealth -= amount;

        hitParticle.transform.position = point;

        hitParticle.Play();

        if (currentHealth <= 0)
        {
            Death();
        }

        return currentHealth;
    }

    void Death()
    {
        ScoreManager.score += scoreValue;
        isDead = true;
        collider.isTrigger = true;
        anim.SetTrigger("Die");
        myAnim.SetTrigger("Dead");
        enemyMovement.enabled = false;
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;

        enemyManager.jEnemy++;

        Destroy(gameObject, 2);
    }
}
