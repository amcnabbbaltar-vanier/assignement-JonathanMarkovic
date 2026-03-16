using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
    public Text scoreText;
    public GameObject endGamePanel;

    // Start is called before the first frame update
    void Start()
    {
        endGamePanel.SetActive(true);

        if (GameManager.Instance)
        {
            scoreText.text = "Score: " + GameManager.Instance.scoreText.ToString();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
