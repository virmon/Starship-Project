using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] Rigidbody playerRb;
    [SerializeField] GameObject engineTrails;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] AudioClip crashSound;
    [SerializeField] GameObject missilePrefab;
    private AudioSource playerAudio;

    private float horizontalInput;
    private float verticalInput;

    [SerializeField] float speed = 2;

    // ENCAPSULATION
    private float _xBound = 7.0f;
    public float xBound
    {
        get
        {
            return _xBound;
        }
        private set
        {
            _xBound = value;
        }
    }

    // ENCAPSULATION
    private float _yBound = 3.0f;
    public float yBound
    {
        get
        {
            return _yBound;
        }
        private set
        {
            _yBound = value;
        }
    }

    // ENCAPSULATION
    private int _points;
    public int points
    {
        get { return _points; }
        set
        {
            if (value < 0)
            {
                Debug.LogError("Points should only be positive integer");
            }
            else
            {
                _points = value;
            }
        }
    }
    public bool hasActiveBoost { get; private set; }
    public bool hasMissile { get; private set; }

    private float powerupTimer;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        playerAudio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ConstrainMovement();
        MovePlayer();
        SteerMovement();
        PowerupTimer();
    }

    public void IncrementPoints(int pointsToAdd)
    {
        points += pointsToAdd;
    }

    private void PowerupTimer()
    {
        if (powerupTimer > 0 && hasActiveBoost)
        {
            powerupTimer -= Time.deltaTime;
        }
        if (powerupTimer <= 0.0f)
        {
            speed = 2;
            hasActiveBoost = false;
        }
    }

    public void UseSpeedBoost()
    {
        if (!hasActiveBoost)
        {
            speed += 2;
            hasActiveBoost = true;
        }
        powerupTimer = 5.0f;
    }

    public void TakeMissile()
    {
        hasMissile = true;
    }

    public void FireMissile()
    {
        float xPos = gameObject.transform.position.x;
        float yPos = gameObject.transform.position.y + 2;
        float zPos = gameObject.transform.position.z;

        Vector3 missilePosition = new Vector3(xPos, yPos, zPos);
        Instantiate(missilePrefab, missilePosition, missilePrefab.transform.rotation);
        hasMissile = false;
    }

    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (gameManager.isGameActive)
        {
            if (horizontalInput != 0 || verticalInput != 0)
            {
                engineTrails.gameObject.SetActive(true);
            }
            else
            {
                engineTrails.gameObject.SetActive(false);
            }

            playerRb.AddForce(Vector3.right * speed * horizontalInput);
            playerRb.AddForce(Vector3.up * speed * verticalInput);
        }

        if (Input.GetKeyDown(KeyCode.Space) && hasMissile)
        {
            FireMissile();
        }
    }

    void ConstrainMovement()
    {
        if (transform.position.x < -xBound && gameManager.isGameActive)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xBound && gameManager.isGameActive)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.y < -yBound && gameManager.isGameActive)
        {
            transform.position = new Vector3(transform.position.x, -yBound, transform.position.z);
        }

        if (transform.position.y > yBound && gameManager.isGameActive)
        {
            transform.position = new Vector3(transform.position.x, yBound, transform.position.z);
        }

        //bool isOutOfBoundsX = transform.position.x < -xBound || transform.position.x > xBound;
        //bool isOutOfBoundsY = transform.position.y < -yBound || transform.position.y > yBound;

        //if (isOutOfBoundsX || isOutOfBoundsY)
        //{
        //    gameManager.GameOver();
        //}
    }

    void SteerMovement()
    {
        if (horizontalInput < 0)
        {
            transform.Rotate(0, 0, -horizontalInput, Space.Self);
        }
        else if (horizontalInput > 0)
        {
            transform.Rotate(0, 0, -horizontalInput, Space.Self);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            playerRb.AddForce(Vector3.right, ForceMode.Force);
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 2.0f);
        }
    }
}
