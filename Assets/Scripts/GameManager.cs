using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text ScoreUI;
    public Text FinishScoreUI;

    public int Score = 0;

    public UnityEvent OnGameOver;
    public UnityEvent OnGameStart;
    public UnityEvent OnFinish;
    

    private GeoData _geo;

    private void Start()
    {
        Pause();
        UpdateScore();
        CheckLocation();
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public void StartGame()
    {
        if (_geo.country != "UA")
        {
            Debug.Log("User is not in Ukraine. Redirecting to Wikipedia.");
            Application.OpenURL("https://www.wikipedia.org/");
        }
        else
        {
            Debug.Log("User is in Ukraine. Proceed with the game.");
            OnGameStart?.Invoke();
        }
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Exit()
    {
        Application.Quit();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Finish()
    {
        Pause();
        FinishScoreUI.text = Score.ToString();
        OnFinish?.Invoke();
    }
    
    public void IncreaseScore()
    {
        Score++;
        UpdateScore();
    }

    private void UpdateScore()
    {
        ScoreUI.text = Score.ToString();
    }

    private void CheckLocation()
    {
        string ip = new WebClient().DownloadString("https://api.ipify.org");
        Debug.Log("User IP: " + ip);

        string url = $"https://ipinfo.io/{ip}/json";
        WebClient client = new WebClient();
        string response = client.DownloadString(url);

        _geo = JsonUtility.FromJson<GeoData>(response);
    }
}
