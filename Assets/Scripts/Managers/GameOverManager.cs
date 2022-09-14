using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    Transform player = null;
    PlayerHealth playerHealth = null;

    [SerializeField]
    GameObject scoreUI = null;

    [SerializeField]
    Text scoreResult = null;

    Animator anim;

    [SerializeField]
    Sprite[] avatarSprites = null;
    [SerializeField]
    Image avatarImage = null;

    EnemyManager enemyManager = null;

    [SerializeField]
    AudioSource buttonAudio;

    private void Start()
    {
        enemyManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();

        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        SetAvatar();

        if (GameManager.data.dataUsers[GameManager.GetOnlineUser()].score < ScoreManager.score)
        {
            
            GameManager.data.dataUsers[GameManager.GetOnlineUser()].score = ScoreManager.score;
        }
            
        if(GameManager.data.dataUsers[GameManager.GetOnlineUser()].score >= 2000)
        {
            GameManager.data.dataUsers[GameManager.GetOnlineUser()].lockItemChar = 1;
        }

        if (GameManager.data.dataUsers[GameManager.GetOnlineUser()].score >= 3000)
        {
            GameManager.data.dataUsers[GameManager.GetOnlineUser()].lockItemChar = 2;
        }

        if (GameManager.data.dataUsers[GameManager.GetOnlineUser()].score >= 2500)
        {
            GameManager.data.dataUsers[GameManager.GetOnlineUser()].lockItemWeapon = 1;
        }

        if (GameManager.data.dataUsers[GameManager.GetOnlineUser()].score >= 3500)
        {
            GameManager.data.dataUsers[GameManager.GetOnlineUser()].lockItemWeapon = 2;
        }
    }

    private void Update()
    {
        if(playerHealth.currentHealth <= 0)
        {
            scoreUI.SetActive(false);
            
            scoreResult.text = ScoreManager.score.ToString();
            anim.SetTrigger("GameOver");
        }
    }

    public void GameOver()
    {
        buttonAudio.Play();
        Time.timeScale = 0;
    }

    void SetAvatar()
    {
        int index = GameManager.data.dataUsers[GameManager.GetOnlineUser()].indexChar;
        avatarImage.sprite = avatarSprites[index];
    }
}
