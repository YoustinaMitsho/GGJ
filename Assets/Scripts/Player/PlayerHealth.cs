using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int damage = 10;
    [SerializeField] Image healthBar;
    [SerializeField] Image vignette;
    int currentHealth;
    void Awake()
    {
        Time.timeScale = 1f;
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = 1f;
        vignette.color = Color.clear;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = (float)currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Player has died.");
        Time.timeScale = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(TakeDamageEffect());
            TakeDamage(damage);
        }
    }

    private IEnumerator TakeDamageEffect()
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Color originalColor = vignette.color;
        Color targetColor = Color.red;

        while (elapsed < duration)
        {
            vignette.color = Color.Lerp(originalColor, targetColor, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < duration)
        {
            vignette.color = Color.Lerp(targetColor, originalColor, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        vignette.color = originalColor;
    }
}
