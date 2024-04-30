using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class MissilePowerup : Powerup
{
    // Update is called once per frame
    void Update()
    {
        FireProjectile();
    }

    private void FireProjectile()
    {
        if (gameObject.CompareTag("Projectile"))
        {
            transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
        }
    }

    // POLYMORPHISM
    public override void InitializePowerup(GameObject playerGameObject)
    {
        AudioSource playerAudio = playerGameObject.GetComponent<AudioSource>();
        playerAudio.PlayOneShot(collectSound, 2.0f);

        playerGameObject.GetComponent<Player>().TakeMissile();
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
