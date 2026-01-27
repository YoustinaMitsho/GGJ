using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] enemies;
    public Transform spawnpoint1;
    public Transform spawnpoint2;

    int number_ememies_left = 25;
    int number_ememies_right = 25;
    int number_ememies_middle = 25;




    int wave = 1;
    string status;



    void Start()
    {
        StartCoroutine(SpawnRepeatedly(spawnpoint1));
        StartCoroutine(SpawnRepeatedly(spawnpoint2));
    }

    IEnumerator SpawnRepeatedly(Transform spa)
    {
        while (true) 
        {
            Spawn(spa);
            yield return new WaitForSeconds(1f);
        }
    }

    void Spawn(Transform spa)
    {
        if (enemies.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, enemies.Length);
            GameObject selectedObject = enemies[randomIndex];
            Instantiate(selectedObject, spa.position, spa.rotation);
        }
    }

  
}