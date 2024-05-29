using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameObject audioSetPanel;
    [SerializeField] private TextMeshProUGUI[] highScores;


    private void Start()
    {
        ShowHighScore();
        Time.timeScale = 1;
    }

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    } 
    
    public void CloseApp()
    {
        Application.Quit();
    }

    public void ShowHighScore()
    {
        int[] scores = scoreManager.GetScores();

        for (int i = 0; i < highScores.Length; i++)
        {
            if (i < scores.Length)
                highScores[i].text = scores[i].ToString();
            else
                highScores[i].text = "0";
        }
    }
}
