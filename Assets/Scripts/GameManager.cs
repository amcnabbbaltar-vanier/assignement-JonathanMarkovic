using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public Text scoreText;
    public int score = 0;
    public Text healthText;
    public int health = 0;
    public Text timerText;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

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
        if (SceneManager.GetActiveScene().name != "EndGame" && SceneManager.GetActiveScene().name != "MainMenu")
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
        UpdateUI();
    }

    public void Reset()
    {
        timer = 0;
        health = 3;
        score = 0;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
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
        if (timerText != null)
        {
            timerText.text = $"Timer: {timer}";
        }
    }
}
