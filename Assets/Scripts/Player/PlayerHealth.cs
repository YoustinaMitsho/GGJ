using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Camera playerCamera;
    [SerializeField] AudioSource hitsound;
    [SerializeField] GameObject canvas;
    [SerializeField] TextMeshProUGUI currPlayer;
    [SerializeField] TextMeshProUGUI currScore;

    int currentHealth;
    bool isDead = false;
    void Awake()
    {
        Time.timeScale = 1f;
        gameOverScreen.SetActive(false);
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
        hitsound.Play();
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        if (isDead) return;
        isDead = true;
        Debug.Log("Player has died.");
        Cursor.lockState = CursorLockMode.None;
        if (playerCamera != null) playerCamera.GetComponent<FirstPersonLook>().enabled = false;
        GetComponent<FirstPersonMovement>().enabled = false;
        StartCoroutine(GameOverDelay());
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
        //Time.timeScale = 0f;
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

    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(2f);
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
        //gameOverScreen.SetActive(false);
        Manager.instance.UpdateHighestScore(currPlayer.text, int.Parse(currScore.text));
        Destroy(canvas);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 1);
    }
}
