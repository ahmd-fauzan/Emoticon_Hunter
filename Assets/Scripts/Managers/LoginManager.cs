using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    Animator anim = null;

    [SerializeField]
    GameObject inputNameUI = null;
    [SerializeField]
    InputField nameInput = null;
    [SerializeField]
    GameObject[] buttons = null;

    [SerializeField]
    LevelLoader levelLoader;

    [SerializeField]
    AudioSource buttonAudio;

    [SerializeField]
    Button continueButton;

    [SerializeField]
    Sprite inActiveImage;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        GameManager.data = GameManager.LoadData();

        if(GameManager.data.dataUsers.Count == 0)
        {
            continueButton.image.sprite = inActiveImage;
            continueButton.interactable = false;
        }
        else
        {  
            continueButton.interactable = true;
        }
        Initialization();
    }

    public void ConfirmButton(int indexScene)
    {
        buttonAudio.Play();

        string name = nameInput.text;

        if (GameManager.SearchName(name) == 1)
        {
            GameManager.CreateNewUser(name, 0, 0, 0, 1, 0, 0);
            GameManager.OfflineUser(name);
            levelLoader.LoadLevel(indexScene);
        }
    }
    public void StartGame()
    {
        buttonAudio.Play();
        anim.SetBool("Start", true);
    }
    public void Back()
    {
        buttonAudio.Play();
        nameInput.text = "";
        inputNameUI.SetActive(false);
        ButtonStatus(true);

    }

    public void NewGame()
    {
        buttonAudio.Play();
        inputNameUI.SetActive(true);
        ButtonStatus(false);
    }
    public void ContinueGame(int indexScene)
    {
        buttonAudio.Play();
        if (GameManager.data.dataUsers.Count != 0)
        {
            levelLoader.LoadLevel(indexScene);
        }
    }
 
    void ButtonStatus(bool status)
    {
        for(int i = 0; i< buttons.Length; i++)
        {
            buttons[i].SetActive(status);
        }
    }

    void Initialization()
    {
        if (GameManager.data.dataCharacters.Count == 0)
        {
            GameManager.CreateDataChar("Arrierty", 15, 120);
            GameManager.CreateDataChar("Eckhart", 20, 150);
            GameManager.CreateDataChar("Doom", 30, 200);
        }

        if(GameManager.data.dataWeapons.Count == 0)
        {
            GameManager.CreateDataWeapon("Rv-920", 20, 100, 0.5f, 0, 0.9f, 0.04f);
            GameManager.CreateDataWeapon("Ls-520", 30, 75, 0.2f, 0, 0.03f, 0.9f);
            GameManager.CreateDataWeapon("Bzk-320", 50, 50, 0.7f, 0.9f, 0.7f, 0);
        }
    }
}
