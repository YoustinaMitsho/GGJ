using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyColor
{
    Red,
    Green,
    Blue
}

public class ColoredBars : MonoBehaviour
{
    public static ColoredBars instance;
    [SerializeField] int maxEnemyCount = 10;
    [SerializeField] int increaseValue = 2;
    [SerializeField] int decreaseValue = 1;
    [SerializeField] Image redBar;
    [SerializeField] Image greenBar;
    [SerializeField] Image blueBar;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        redBar.fillAmount = 0f;
        greenBar.fillAmount = 0f;
        blueBar.fillAmount = 0f;
    }

    public void IncreaseBar(EnemyColor color)
    {
        Image bar = DecideColor(color);
        if (bar != null)
        {
            bar.fillAmount += (float)increaseValue / maxEnemyCount;
            bar.fillAmount = Mathf.Min(bar.fillAmount, 1f);
        }
    }

    public void DecreaseBar(EnemyColor color)
    {
        Image bar = DecideColor(color);
        if (bar != null)
        {
            bar.fillAmount -= (float)decreaseValue / maxEnemyCount;
            bar.fillAmount = Mathf.Max(bar.fillAmount, 0f);
        }
    }

    private Image DecideColor(EnemyColor color)
    {
        switch (color)
        {
            case EnemyColor.Red:
                return redBar;
            case EnemyColor.Green:
                return greenBar;
            case EnemyColor.Blue:
                return blueBar;
            default:
                return null;
        }
    }



    // testing functions

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            IncreaseBar(EnemyColor.Red);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            IncreaseBar(EnemyColor.Green);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            IncreaseBar(EnemyColor.Blue);
        }
    }
    public void TestIncreaseRed()
    {
        IncreaseBar(EnemyColor.Red);
    }
    public void TestIncreaseGreen()
    {
        IncreaseBar(EnemyColor.Green);
    }
    public void TestIncreaseBlue()
    {
        IncreaseBar(EnemyColor.Blue);
    }
    public void TestDecreaseRed()
    {
        DecreaseBar(EnemyColor.Red);
    }
    public void TestDecreaseGreen()
    {
        DecreaseBar(EnemyColor.Green);
    }
    public void TestDecreaseBlue()
    {
        DecreaseBar(EnemyColor.Blue);
    }
}
