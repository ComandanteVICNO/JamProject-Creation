using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    public static GameOverUIManager instance;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] private TMP_Text timeTitle;
    [SerializeField] private TMP_Text killCountTitle;
    
    int killCount = 0;
    float timer = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }


    public void GameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
        
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        timeTitle.text = "Time Survived: " + formattedTime;
        killCountTitle.text = "Kills: " + killCount;
        
        
    }

   

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    
    public void AddKillToStats()
    {
        killCount++;
    }
    
}
