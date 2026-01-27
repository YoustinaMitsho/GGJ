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
    [SerializeField] float intensity = 1f;
    [SerializeField] PostProcessVolume volume;
    [SerializeField] Vignette vignette;
    int currentHealth;
    void Awake()
    {
        Time.timeScale = 1f;
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = 1f;

        if (volume == null)
        {
            volume = GetComponentInChildren<PostProcessVolume>();
        }

        if (volume != null)
        {
            if (volume.profile.TryGetSettings<Vignette>(out vignette))
            {
                vignette.enabled.Override(false);
                vignette.intensity.Override(0f);
            }
        }
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
        float duration = 1.0f;
        float elapsed = 0f;

        vignette.enabled.Override(true);
        vignette.intensity.Override(0.4f);

        yield return new WaitForSeconds(0.4f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float newIntensity = Mathf.Lerp(0.4f, 0f, elapsed / duration);
            vignette.intensity.Override(newIntensity);
            yield return null;
        }

        vignette.enabled.Override(false);
    }
}
