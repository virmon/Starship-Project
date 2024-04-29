using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private GameManager gameManager;

    public int pointValue;

    private float xRange = 7;
    private float ySpawnPos = 4;

    [SerializeField] AudioClip collectSound;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        transform.position = new Vector3(Random.Range(-xRange, xRange), Random.Range(-ySpawnPos, ySpawnPos), -2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource playerAudio = other.gameObject.GetComponent<AudioSource>();
            playerAudio.PlayOneShot(collectSound, 2.0f);

            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
        }
    }
}
