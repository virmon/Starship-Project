using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    protected GameManager gameManager;

    [SerializeField] protected int pointValue;
    [SerializeField] protected AudioClip collectSound;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnPowerup()
    {
        //float xSpawnPos = Random.Range(-xRange, xRange);
        //float ySpawnPos = Random.Range(-yRange, yRange);
        //float zSpawnPos = -2;

        //transform.position = new Vector3(xSpawnPos, ySpawnPos, zSpawnPos);
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-7, 7);
        float spawnPosY = Random.Range(-4, 4);

        Vector3 randomPos = new Vector3(spawnPosX, spawnPosY, -2);

        return randomPos;
    }

    public abstract void InitializePowerup(GameObject playerGameObject);
    
}
