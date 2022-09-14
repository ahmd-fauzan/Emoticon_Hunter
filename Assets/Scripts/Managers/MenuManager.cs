using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    Animator anim = null;

    [SerializeField]
    Sprite[] avatarSprites = null;
    [SerializeField]
    Image avatarImage = null;
    [SerializeField]
    Text nameProfileText = null;
    [SerializeField]
    Text scoreProfileText = null;

    [SerializeField]
    LevelLoader levelLoader;

    [SerializeField]
    AudioSource buttonAudio;

    private void Start()
    {
        anim.SetTrigger("Splash");
        SetProfile();
        SetAvatar();
    }

    public void PlayGame(int indexScene)
    {
        buttonAudio.Play();
        levelLoader.LoadLevel(indexScene);
    }

    public void Exit()
    {
        buttonAudio.Play();
        GameManager.SaveData(GameManager.data);
        Application.Quit();
    }

    public void ActiveGameObject(GameObject obj)
    {
        buttonAudio.Play();
        obj.SetActive(true);
    }

    public void InActiveGameObject(GameObject obj)
    {
        buttonAudio.Play();
        obj.SetActive(false);
    }

    public void SetAvatar()
    {
        int index = GameManager.data.dataUsers[GameManager.GetOnlineUser()].indexChar;
        avatarImage.sprite = avatarSprites[index];
    }

    void SetProfile()
    {
        nameProfileText.text = GameManager.data.dataUsers[GameManager.GetOnlineUser()].nameUser;
        scoreProfileText.text = GameManager.data.dataUsers[GameManager.GetOnlineUser()].score.ToString();
    }
}
