using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const int MaxScores = 5;

    public void AddScore(int scoreToAdd)
    {
        int[] scores = GetScores();
        for (int i = 0; i < MaxScores; i++)
        {
            if (!PlayerPrefs.HasKey("Score" + i) || scoreToAdd > scores[i])
            {
                SaveScore("Score" + i, scoreToAdd);
                break;
            }
        }
    }

    public int[] GetScores()
    {
        int[] scores = new int[MaxScores];
        for (int i = 0; i < MaxScores; i++)
        {
            scores[i] = PlayerPrefs.GetInt("Score" + i, 0);
        }
        return scores;
    }

    private void SaveScore(string key, int score)
    {
        PlayerPrefs.SetInt(key, score);
        PlayerPrefs.Save();
    }
}
