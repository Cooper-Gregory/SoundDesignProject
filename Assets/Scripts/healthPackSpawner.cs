using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPackSpawner : MonoBehaviour
{
    public GameObject healthPackPrefab;
    public float timeToHealthSpawn;
    protected float healthSpawnTimer = 0.0f;
    public GameObject gameManager;

    void Start()
    {
        timeToHealthSpawn = Random.Range(15, 60);
    }

    void Update()
    {
        healthSpawnTimer += Time.deltaTime;
        if (healthSpawnTimer >= timeToHealthSpawn)
        {
            GameObject healthPack = Instantiate(healthPackPrefab);
            healthPack.transform.position = transform.position;
            healthPack.GetComponent<heathPack>().gameManager = gameManager;
            gameObject.SetActive(false);
        }
    }
}
