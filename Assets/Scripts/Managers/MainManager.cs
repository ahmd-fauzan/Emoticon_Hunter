using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] characterModels = null;

    [SerializeField]
    GameObject[] weaponModels = null;

    public Transform characterParent = null, weaponParent = null;

    [SerializeField]
    Text nameUserText = null;

    public GameObject enemyHealth = null;

    public GameObject nameEnemyText;

    GameObject character = null, weapon = null;
    
    // Start is called before the first frame update
    void Start()
    {
        character = SpawnObject(characterModels[GameManager.data.dataUsers[GameManager.GetOnlineUser()].indexChar], characterParent);
        character.transform.parent = characterParent;
        weapon = SpawnObject(weaponModels[GameManager.data.dataUsers[GameManager.GetOnlineUser()].indexWeapon], weaponParent);
        weapon.transform.parent = weaponParent;

        character.GetComponent<Transform>().localScale = new Vector3(characterParent.localScale.x * 2f, characterParent.localScale.y * 2f, characterParent.localScale.z * 2f);
        nameUserText.text = GameManager.data.dataUsers[GameManager.GetOnlineUser()].nameUser;
    }

    // Update is called once per frame

    

    public GameObject SpawnObject(GameObject model, Transform parent)
    {
        GameObject _model = Instantiate(model, parent.position, parent.rotation)as GameObject;

        return _model;
    }
    
    public void HealthEnemy(int health)
    {
        enemyHealth.GetComponent<Slider>().value = health;
    }
}
