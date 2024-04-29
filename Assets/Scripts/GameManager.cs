using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public Button startGameButton;

    public GameObject starPrefab;
    public GameObject asteroidPrefab;

    public bool isGameActive;
    private int score;
    private float spawnRate = 2.0f;

    public int starCount = 1;
    public int waveNumber = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        starCount = FindObjectsOfType<PointsPowerup>().Length;

        if (starCount == 0 && isGameActive)
        {
            SpawnStar();
        }
    }

    IEnumerator SpawnObstacle()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnAsteroid(waveNumber);
        }
    }

    public void SpawnStar()
    {
        Instantiate(starPrefab, GenerateSpawnPosition(), starPrefab.transform.rotation);
    }

    void SpawnAsteroid(int obstaclesToSpawn)
    {
        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            Instantiate(asteroidPrefab);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-7, 7);
        float spawnPosY = Random.Range(-4, 4);

        Vector3 randomPos = new Vector3(spawnPosX, spawnPosY, -2);

        return randomPos;
    }

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        UpdateScore(score);
        SpawnStar();
        StartCoroutine(SpawnObstacle());

        gameOverScreen.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(false);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
