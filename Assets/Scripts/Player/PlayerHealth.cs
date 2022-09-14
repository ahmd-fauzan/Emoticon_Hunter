using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    int startingHealth = 100;

    [SerializeField]
    Slider healthSlider = null;

    PlayerMovement playerMovement = null;

    public Animator playerAnim = null;

    public int currentHealth;

    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    [SerializeField]
    GameObject gameOverUI = null;

    [SerializeField]
    Image damageImage = null;

    public float flashSpeed = 5f;

    bool isDead = false;

    bool damaged = false;

    private void Start()
    {
        startingHealth = GameManager.data.dataCharacters[GameManager.data.dataUsers[GameManager.GetOnlineUser()].indexChar].hp;
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = startingHealth;
        playerAnim = GameObject.FindGameObjectWithTag("PlayerCharacter").GetComponent<Animator>();
        SetSlider();
    }

    private void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        //audio saat kena Hit

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void SetSlider()
    {
        healthSlider.maxValue = healthSlider.value = startingHealth;
        healthSlider.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, startingHealth);
    }

    void Death()
    {
        gameOverUI.SetActive(true);
        playerMovement.enabled = false;
        playerAnim.SetTrigger("Die");
    }
}
