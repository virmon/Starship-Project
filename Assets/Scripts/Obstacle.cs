using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody obstacle;
    private GameManager gameManager;

    [SerializeField] float force = 5;

    private float minSpeed = 1;
    private float maxSpeed = 2;
    private float maxTorque = 10;
    private float xRange = 7;
    private float ySpawnPos = 7;

    [SerializeField] AudioClip crashSound;
    [SerializeField] ParticleSystem explosionParticle;

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
            //Destroy(gameObject);
            gameManager.GameOver();

            AudioSource playerAudio = collision.gameObject.GetComponent<AudioSource>();
            playerAudio.PlayOneShot(crashSound, 2.0f);
            //explosionParticle.Play();

            //Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            //playerRb.AddForce(Vector3.up * force, ForceMode.Impulse);
        }

        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("Asteroids collided");
            // Destroy colliding Asteroids
            // Then Spawn smaller asteroids
        }
    }
}
