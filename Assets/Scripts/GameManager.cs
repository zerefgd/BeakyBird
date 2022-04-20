using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField]
    private GameObject _startPanel,
        _helpPanel,
        _gamePanel,
        _gameOverPanel,
        _player,
        _obstaclePrefab;

    [SerializeField]
    TMP_Text _scoreText, _endScoreText, _highScoreText;

    private int score,highScore;
    private bool hasGameFinished;
    private const string HIGHSCORE = "HIGHSCORE";


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _startPanel.SetActive(true);
        _helpPanel.SetActive(false);
        _gamePanel.SetActive(false);
        _gameOverPanel.SetActive(false);
        _player.SetActive(false);

        score = 0;
        hasGameFinished = false;
    }

    public void StartHelp()
    {
        _startPanel.SetActive(false);
        _helpPanel.SetActive(true);
    }

    public void UpdateScore(int v)
    {
        score += v;
        _scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        hasGameFinished = true;

        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var item in obstacles)
        {
            item.gameObject.SetActive(false);
        }

        highScore = PlayerPrefs.HasKey(HIGHSCORE) ? PlayerPrefs.GetInt(HIGHSCORE) : 0;
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HIGHSCORE, highScore);
        }
        _highScoreText.text = "HIGHSCORE " + highScore.ToString();
        _endScoreText.text = "SCORE " + score.ToString();

        _gameOverPanel.SetActive(true);
        _gamePanel.SetActive(false);
    }

    public void StartGame()
    {
        _helpPanel.SetActive(false);
        _gamePanel.SetActive(true);
        _player.SetActive(true);

        StartCoroutine(Spawner());

    }

    public void GameRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    IEnumerator Spawner()
    {
        while(!hasGameFinished)
        {
            Instantiate(_obstaclePrefab, Vector3.zero, _obstaclePrefab.transform.rotation);
            yield return new WaitForSeconds(2f);
        }
    }
}
