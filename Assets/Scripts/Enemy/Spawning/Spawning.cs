using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] GameObject[] enemies;
    [SerializeField] Transform spawnpoint;
    [SerializeField] float cooldown = 1f;
    [Header("Spawn Control")]
    [SerializeField] int maxEnemies = 10;
    public bool canSpawn = false;

    private int currentEnemyCount = 0;

    void Update()
    {
        if (canSpawn)
        {
            StartSpawning();
        }
    }

    void StartSpawning()
    {
        StartCoroutine(SpawnRepeatedly(spawnpoint));
    }
    IEnumerator SpawnRepeatedly(Transform spa)
    {
        while (currentEnemyCount < maxEnemies)
        {
            Spawn(spa);
            yield return new WaitForSeconds(cooldown);
        }

        Debug.Log($"Spawn point at {spa.name} is now exhausted.");
    }

    void Spawn(Transform spa)
    {
        if (enemies.Length > 0 && currentEnemyCount < maxEnemies)
        {
            int randomIndex = UnityEngine.Random.Range(0, enemies.Length);
            GameObject selectedObject = enemies[randomIndex];

            Instantiate(selectedObject, spa.position, spa.rotation);
            currentEnemyCount++;
        }
    }
}