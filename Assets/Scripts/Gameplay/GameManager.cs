using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI endScoreText;
    [SerializeField] private TextMeshProUGUI[] highScores;

    private Player player;

    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject infoPanel;

/*    [SerializeField] private SpriteRenderer backgroundSpriteRenderer;
    [SerializeField] private Sprite[] backgroundSprites;*/

    private void Start()
    {
        player = FindObjectOfType<Player>();
        endPanel.SetActive(false);
        infoPanel.SetActive(false);
        pausePanel.SetActive(false);

/*        if (backgroundSprites != null && backgroundSprites.Length > 0)
        {
            int randomIndex = Random.Range(0, backgroundSprites.Length);
            Sprite randomSprite = backgroundSprites[randomIndex];

            backgroundSpriteRenderer.sprite = randomSprite;
        }*/
    }

    private void Update()
    {
        UpdateScoreText();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void InfoGame()
    {
        Time.timeScale = 0;
        infoPanel.SetActive(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        infoPanel.SetActive(false);
    }

    public void ChangeScene(int scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    private void UpdateScoreText()
    {
        scoreText.text = player.score.ToString();
        endScoreText.text = player.score.ToString();
    }

    public void GameOver(int score)
    {
        scoreManager.AddScore(score);
        ShowHighScore();
        endPanel.SetActive(true);
    }

    private void ShowHighScore()
    {
        int[] scores = scoreManager.GetScores();
        Debug.Log("Retrieved Scores: " + string.Join(", ", scores));

        for (int i = 0; i < highScores.Length; i++)
        {
            int scoreToShow = (i < scores.Length) ? scores[i] : 0;
            highScores[i].text = scoreToShow.ToString();
            Debug.Log("Assigning score " + scoreToShow + " to UI element at index " + i);
        }
    }
}
