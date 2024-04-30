using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class SpeedBoostPowerup : Powerup
{
    // POLYMORPHISM
    public override void InitializePowerup(GameObject playerGameObject)
    {
        AudioSource playerAudio = playerGameObject.GetComponent<AudioSource>();
        playerAudio.PlayOneShot(collectSound, 2.0f);

        playerGameObject.GetComponent<Player>().UseSpeedBoost();
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
