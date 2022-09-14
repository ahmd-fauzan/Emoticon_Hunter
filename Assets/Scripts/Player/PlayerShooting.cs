using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShoot = 20;
    
    public float range = 100f;

    float timer;
    Ray shootRay;
    RaycastHit shootHIt;
    int shootableMask;
    ParticleSystem gunParticle;
    LineRenderer gunLine;
    Light gunLight;
    AudioSource gunAudio;
    float effectDisplayTime = 0.2f;

    PlayerHealth playerHealth;

    public MainManager mainManager;

    PlayerMovement playerMovement;

    [SerializeField]
    Material lineRenderMaterial;

    private void Start()
    {
        int indexChar = GameManager.data.dataUsers[GameManager.GetOnlineUser()].indexWeapon;

        range = (int)GameManager.data.dataWeapons[indexChar].range;

        damagePerShoot = GameManager.data.dataWeapons[indexChar].damage;

        shootableMask = LayerMask.GetMask("Shootable");
        gunParticle = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        gunAudio = GetComponent<AudioSource>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        mainManager.enemyHealth.SetActive(false);
        mainManager.nameEnemyText.SetActive(false);

        ParticleSystem.MainModule main = GetComponent<ParticleSystem>().main;
        main.startColor = new Color(GameManager.data.dataWeapons[indexChar].r, GameManager.data.dataWeapons[indexChar].g, GameManager.data.dataWeapons[indexChar].b);
        lineRenderMaterial.color = new Color(GameManager.data.dataWeapons[indexChar].r, GameManager.data.dataWeapons[indexChar].g, GameManager.data.dataWeapons[indexChar].b);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= playerMovement.timeBetweenBullet * effectDisplayTime)
        {
            DisableEffect();
        }
    }

    void DisableEffect()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
    public int Shoot()
    {
        timer = 0f;

        gunLight.enabled = true;
        gunAudio.Play();

        gunParticle.Stop();
        gunParticle.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast(shootRay, out shootHIt, range, shootableMask)){
            EnemyHealth enemyHealth = shootHIt.collider.GetComponent<EnemyHealth>();

            if(enemyHealth != null)
            {
                mainManager.enemyHealth.SetActive(true);
                mainManager.nameEnemyText.SetActive(true);

                mainManager.nameEnemyText.GetComponent<Text>().text = enemyHealth.name;
                mainManager.enemyHealth.GetComponent<Slider>().maxValue = enemyHealth.startingHealth;
                mainManager.enemyHealth.GetComponent<Slider>().value = enemyHealth.TakeDamage(damagePerShoot, shootHIt.point);

                if (enemyHealth.currentHealth <= 0)
                {
                    mainManager.enemyHealth.SetActive(false);
                    mainManager.nameEnemyText.SetActive(false);
                }
            }
            else
            {
                mainManager.enemyHealth.SetActive(false);
                mainManager.nameEnemyText.SetActive(false);
            }
            
            gunLine.SetPosition(1, shootHIt.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }

        return 0;
    }
}
