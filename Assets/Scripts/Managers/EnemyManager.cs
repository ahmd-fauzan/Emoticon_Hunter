using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    GameObject objectEnemy;

    public int jEnemy;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    // Update is called once per frame

    void Spawn()
    {
        int indexSpawn = Random.Range(0, spawnPoints.Length);

        objectEnemy = Instantiate(enemy, spawnPoints[indexSpawn].position, spawnPoints[indexSpawn].rotation);
        objectEnemy.transform.parent = spawnPoints[indexSpawn];
        objectEnemy.GetComponent<Transform>().localScale = new Vector3(spawnPoints[indexSpawn].localScale.y, spawnPoints[indexSpawn].localScale.y, spawnPoints[indexSpawn].localScale.y);

    }
}
