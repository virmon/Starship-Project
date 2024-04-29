using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPowerup : Powerup
{
    public override void InitializePowerup(GameObject playerGameObject)
    {
        AudioSource playerAudio = playerGameObject.GetComponent<AudioSource>();
        playerAudio.PlayOneShot(collectSound, 2.0f);

        gameManager.UpdateScore(pointValue);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InitializePowerup(other.gameObject);
            Destroy(gameObject);
        }
    }
}
