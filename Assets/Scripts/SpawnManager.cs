using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    private Player player;

    [SerializeField] float spawnRate = 5.0f;
    private int starCount = 1;
    private int speedBoostCount = 0;
    private int missileCount = 0;
    private int obstacleCount = 3;

    private int nextMissileSpawnPoints = 5;
    private int nextSpeedBoostSpawnPoints = 10;

    public GameObject starPrefab;
    public GameObject speedBoostPrefab;
    public GameObject asteroidPrefab;
    public GameObject missilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnPowerups()
    {
        starCount = FindObjectsOfType<PointsPowerup>().Length;

        if (starCount == 0 && gameManager.isGameActive)
        {
            SpawnStar();
        }

        speedBoostCount = FindObjectsOfType<SpeedBoostPowerup>().Length;
        if (player.points > nextSpeedBoostSpawnPoints && speedBoostCount == 0 && gameManager.isGameActive)
        {
            nextSpeedBoostSpawnPoints += 10;
            SpawnSpeedBoost();
        }

        missileCount = FindObjectsOfType<MissilePowerup>().Length;
        if (player.points > nextMissileSpawnPoints && missileCount == 0 && gameManager.isGameActive)
        {
            nextMissileSpawnPoints += 10;
            SpawnMissile();
        }
    }

    public IEnumerator SpawnObstacle()
    {
        while (gameManager.isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnAsteroid(obstacleCount);
        }
    }

    public void SpawnStar()
    {
        Instantiate(starPrefab, GenerateSpawnPosition(), starPrefab.transform.rotation);
    }

    public void SpawnSpeedBoost()
    {
        Instantiate(speedBoostPrefab, GenerateSpawnPosition(), speedBoostPrefab.transform.rotation);
    }

    public void SpawnMissile()
    {
        Instantiate(missilePrefab, GenerateSpawnPosition(), missilePrefab.transform.rotation);
    }

    public void SpawnAsteroid(int obstaclesToSpawn)
    {
        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            Instantiate(asteroidPrefab);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        
        float spawnPosX = Random.Range(-player.xBound, player.xBound);
        float spawnPosY = Random.Range(-player.yBound, player.yBound);

        Vector3 randomPos = new Vector3(spawnPosX, spawnPosY, -2);

        return randomPos;
    }
}
