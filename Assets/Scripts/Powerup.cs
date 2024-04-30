using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ABSTRACTION
public abstract class Powerup : MonoBehaviour
{
    protected GameManager gameManager;

    [SerializeField] protected int pointValue;
    [SerializeField] protected AudioClip collectSound;
    [SerializeField] protected float projectileSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // ABSTRACTION
    public abstract void InitializePowerup(GameObject playerGameObject);
}
