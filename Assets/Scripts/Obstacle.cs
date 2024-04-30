using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody obstacle;
    private GameManager gameManager;

    private float minSpeed = 1;
    private float maxSpeed = 2;
    private float maxTorque = 10;
    private float xRange = 7;
    private float ySpawnPos = 7;

    [SerializeField] float yOutOfBounds = -10;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        obstacle = GetComponent<Rigidbody>();

        obstacle.AddForce(RandomForce(), ForceMode.Impulse);
        obstacle.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = new Vector3(Random.Range(-xRange, xRange), ySpawnPos, -2);
    }

    // Update is called once per frame
    void Update()
    {
        DestroyOutOfBounds();
    }

    Vector3 RandomForce()
    {
        return Vector3.down * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.GameOver();
        }

        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("Asteroids collided");
            // Destroy colliding Asteroids
            // Then Spawn smaller asteroids
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void DestroyOutOfBounds()
    {
        if (transform.position.y < yOutOfBounds)
        {
            Destroy(gameObject);
        }
    }
}
