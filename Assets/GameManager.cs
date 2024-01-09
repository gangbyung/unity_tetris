using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverUI;
    public bool isGameOver = false;

    public int score = 0;
    public Text Score;
    public void clear()
    {
        score++;
        Score.text = string.Format($"Score : {score}");
        
    }
    public void addScore(int n)
    {
        score += n;
        Gamescore.instance.ScorePrint(score);
    }
    private void Awake()
    {
        instance = this;
    }

    public void EndGame()
    {
        gameOverUI.SetActive(true);
    }

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
