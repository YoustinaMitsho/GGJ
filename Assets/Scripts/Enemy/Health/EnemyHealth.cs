using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] EnemyColor enemyColor;
    [SerializeField] int maxHealth = 100;
    int currentHealth;

    public TMP_Text score;
    public TMP_Text highest_score;
    public TMP_Text highest_scorer;


    void Awake()
    {
        score = GameObject.Find("Current_Score").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            UpdateScore(10); 
            Die();
        }
    }

    void UpdateScore(int pointsToAdd)
    {
        if (score == null)
        {
            Debug.LogError("Score TMP_Text not assigned!");
            return;
        }

        int currentScore;
        string numericText = score.text;

        if (score.text.Contains(":"))
        {
            numericText = score.text.Split(':')[1].Trim();
        }

        if (int.TryParse(numericText, out currentScore))
        {
            currentScore += pointsToAdd;
            if (score.text.Contains(":"))
                score.text = "Score: " + currentScore;
            else
                score.text = currentScore.ToString();
        }
        else
        {
            Debug.LogWarning("ScoreText contained invalid number: " + score.text);
            score.text = pointsToAdd.ToString(); // reset to points if invalid
        }
    }

    void Die()
    {
        ColoredBars.instance.IncreaseBar(enemyColor);
        Destroy(gameObject);
    }
}
