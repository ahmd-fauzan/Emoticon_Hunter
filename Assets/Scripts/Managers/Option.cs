using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Option : MonoBehaviour
{
    [SerializeField]
    GameObject OptionUI = null;

    [SerializeField]
    GameObject optionButton = null;

    [SerializeField]
    Text nameText;

    [SerializeField]
    AudioSource bgmAudio = null;
    [SerializeField]
    AudioSource effectAudio = null;
    [SerializeField]
    Slider volumeSlider = null;
    [SerializeField]
    Slider effectSlider = null;

    [SerializeField]
    LevelLoader levelLoader;

    [SerializeField]
    GameObject scoreUi;

    [SerializeField]
    AudioSource buttonAudio;

    private void Update()
    {
        bgmAudio.volume = volumeSlider.value;
        effectAudio.volume = effectSlider.value;
    }
    public void OpenOption()
    {
        buttonAudio.Play();
        OptionUI.SetActive(true);
        Time.timeScale = 0;
        optionButton.SetActive(false);
        scoreUi.SetActive(false);
    }

    public void CloseOption()
    {
        buttonAudio.Play();
        OptionUI.SetActive(false);
        Time.timeScale = 1;
        optionButton.SetActive(true);
        scoreUi.SetActive(true);
    }

    public void RestartButton(int indexScene)
    {
        buttonAudio.Play();
        Time.timeScale = 1;
        ScoreManager.score = 0;
        scoreUi.SetActive(true);
        levelLoader.LoadLevel(indexScene);
    }

    public void ExitButton(int indexScene)
    {
        buttonAudio.Play();
        Time.timeScale = 1;
        levelLoader.LoadLevel(indexScene);
    }
}
