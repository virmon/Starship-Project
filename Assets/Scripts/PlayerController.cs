using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] GameObject engineTrails;

    private float horizontalInput;
    private float verticalInput;

    [SerializeField] float speed = 10;
    [SerializeField] float xBound = 10.0f;
    [SerializeField] float yBound = 5.0f;

    [SerializeField] ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ConstrainMovement();
        MovePlayer();
        SteerMovement();
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            playerRb.AddForce(Vector3.right, ForceMode.Force);
            explosionParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(collision.gameObject);
        }
    }

    void ConstrainMovement()
    {
        bool isOutOfBoundsX = transform.position.x < -xBound || transform.position.x > xBound;
        bool isOutOfBoundsY = transform.position.y < -yBound || transform.position.y > yBound;

        if (isOutOfBoundsX || isOutOfBoundsY)
        {
            gameManager.GameOver();
        }
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
}
