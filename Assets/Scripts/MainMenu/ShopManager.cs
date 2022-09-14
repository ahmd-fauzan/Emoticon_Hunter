using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] characterModel = null;
    [SerializeField]
    GameObject[] weaponModels = null;

    [SerializeField]
    Transform characterParent = null;

    [SerializeField]
    Transform weaponParent = null;

    GameObject showObject = null;

    public int index = 0;

    [SerializeField]
    Text nameText = null;
    [SerializeField]
    Text attackText = null;
    [SerializeField]
    Text hPText = null;
    [SerializeField]
    Text rangeText = null;

    [SerializeField]
    MenuManager menuManager = null;

    [SerializeField]
    Button selectButton = null;

    [SerializeField]
    Sprite[] hasSelect = null;

    [SerializeField]
    GameObject lockImage = null;

    bool character;

    int maxIndex;

    [SerializeField]
    GameObject textLock = null;

    [SerializeField]
    AudioSource buttonAudio = null;

    // Start is called before the first frame update
    void Start()
    {
        character = true;
        maxIndex = characterModel.Length;
        LockItemChar(0);
        showObject = SpawnObject(characterParent, characterModel[index]);
        GetDataCharacter(0);

        if (GameManager.data.dataUsers[GameManager.GetOnlineUser()].score >= 2000)
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
        if (index == GameManager.data.dataUsers[GameManager.GetOnlineUser()].indexChar && character)
        {
            selectButton.image.sprite = hasSelect[1];
            selectButton.interactable = false;
        }

        if (index == GameManager.data.dataUsers[GameManager.GetOnlineUser()].indexWeapon && !character)
        {
            selectButton.image.sprite = hasSelect[1];
            selectButton.interactable = false;
            
        }

        showObject.GetComponent<Transform>().Rotate(Vector3.up);

        if (index == 1)
        {
            if (character)
            {
                textLock.GetComponent<Text>().text = "Get Score 2000 to Unlock this character";
            }
            else
            {
                textLock.GetComponent<Text>().text = "Get Score 2500 to Unlock this Weapon";
            }
        }
        if(index == 2)
        {
            if (character)
            {
                textLock.GetComponent<Text>().text = "Get Score 3000 to Unlock this character";
            }
            else
            {
                textLock.GetComponent<Text>().text = "Get Score 3500 to Unlock this Weapon";
            }
        }



    }

    public void Select()
    {
        buttonAudio.Play();
        if (character)
        {
            GameManager.data.dataUsers[GameManager.GetOnlineUser()].indexChar = index;
            menuManager.SetAvatar();
        }
        else
        {
            GameManager.data.dataUsers[GameManager.GetOnlineUser()].indexWeapon = index;
        }
    }

    public void Next()
    {
        buttonAudio.Play();
        if (index < maxIndex - 1)
        {
            Destroy(showObject);
            index++;
            ShowObject();  
        }
    }
    public void Prev()
    {
        buttonAudio.Play();
        if (index > 0)
        {
            Destroy(showObject);
            index--;
            ShowObject();
        }
       
    }

    public void Character()
    {
        buttonAudio.Play();
        index = 0;
        character = true;
        maxIndex = characterModel.Length;
        Destroy(showObject);
        ShowObject();
    }

    public void Weapon()
    {
        buttonAudio.Play();
        index = 0;
        character = false;
        maxIndex = weaponModels.Length;
        Destroy(showObject);
        ShowObject();
    }

    void GetDataCharacter(int index)
    {
        nameText.text = "Name : " + GameManager.data.dataCharacters[index].nameChar;
        attackText.text = "Attack : " + GameManager.data.dataCharacters[index].damage.ToString();
        hPText.text = "HP : " + GameManager.data.dataCharacters[index].hp.ToString();
    }

    void GetDataWeapon(int index)
    {
        nameText.text = "Name : " + GameManager.data.dataWeapons[index].nameWeapon;
        attackText.text = "Damage : " + GameManager.data.dataWeapons[index].damage;
        hPText.text = "RateOfFire : " + GameManager.data.dataWeapons[index].rateOfFire;
        rangeText.text = "Range : " + GameManager.data.dataWeapons[index].range;
    }

    GameObject SpawnObject(Transform parent, GameObject model)
    {
        return (Instantiate(model, parent.position, parent.rotation) as GameObject); ;
    }

    void ShowObject()
    {
        

        if (character)
        {
            LockItemChar(index);
            showObject = SpawnObject(characterParent, characterModel[index]);
            GetDataCharacter(index);
        }
        else
        {
            LockItemWeapon(index);
            showObject = SpawnObject(weaponParent, weaponModels[index]);
            GetDataWeapon(index);
        }
    }

    void LockItemChar(int index)
    {
        if (GameManager.data.dataUsers[GameManager.GetOnlineUser()].lockItemChar >= index)
        {
            ButtonSelect(true, 0);
            textLock.SetActive(false);
        }
        else
        {
            ButtonSelect(false, 1);
            textLock.SetActive(true);
        }
    }

    void LockItemWeapon(int index)
    {
        if (GameManager.data.dataUsers[GameManager.GetOnlineUser()].lockItemWeapon >= index)
        {
            ButtonSelect(true, 0);
            textLock.SetActive(false);
        }
        else
        {
            ButtonSelect(false, 1);
            textLock.SetActive(true);
        }
    }

    void ButtonSelect(bool status, int index)
    {
        selectButton.interactable = status;
        selectButton.image.sprite = hasSelect[index];
        lockImage.SetActive(!status);
    }
}
