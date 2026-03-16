using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public Text scoreText;
    public int score = 0;
    public Text healthText;
    public int health = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        if (healthText != null)
        {
            health = 3;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        UpdateUI();
    }

    // Update is called once per frame
    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
        if (healthText != null)
        {
            healthText.text = $"Health: {health}";
        }
    }
}
