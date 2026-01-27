using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MaskRotation : MonoBehaviour
{
    [Header("Floating Settings")]
    [SerializeField] float floatHeight = 0.5f;
    [SerializeField] float floatSpeed = 2f;
    [SerializeField] EnemyColor maskColor;
    [SerializeField] int increaseValue = 1;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // hide for 20 secs
            StartCoroutine(HideMaskTemporarily());
            // increase color bar count
            ColoredBars.instance.IncreaseBar(maskColor, increaseValue);
        }
    }

    IEnumerator HideMaskTemporarily()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(20f);
        gameObject.SetActive(true);
    }
}
