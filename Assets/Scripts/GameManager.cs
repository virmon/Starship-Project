using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private SpawnManager spawnManager;
    private Player player;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] Button startGameButton;
    [SerializeField] Button restartButton;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject gameOverScreen;

    public bool isGameActive { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnManager.SpawnPowerups();
    }

    public void InitializeGameData()
    {
        isGameActive = true;
        player.points = 0;
    }

    public void StartGame()
    {
        InitializeGameData();

        spawnManager.SpawnPowerups();
        StartCoroutine(spawnManager.SpawnObstacle());

        scoreText.text = $"Score: {player.points}";
        gameOverScreen.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(false);
    }

    public void UpdatePoints(int pointsToAdd)
    {
        player.IncrementPoints(pointsToAdd);
        scoreText.text = $"Score: {player.points}";
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
