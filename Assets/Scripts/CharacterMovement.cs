using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Text scoreText;
    private bool canDoubleJump;
    public LayerMask ground;
    private float raycastDistance = 1.0f;
    public ParticleSystem particle;

    //status timers
    private float jumpBoostTimer = 0;
    private float speedBoostTimer = 0;

    //Character stats
    public float jumpForce = 10.0f;
    public int health = 3;
    private float jumpTimer = 0;
    public float speed = 5.0f;
    private float speedMultiplier = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //scoreText = GameObject.FindWithTag("Score").GetComponent<Text>();
        canDoubleJump = false;
    }

    private float horizontalInput;

    // Update is called once per frame
    void Update()
    {
        //* Getting the player movement direction
        horizontalInput = Input.GetAxis("Horizontal");

        //Respawn and take damage if player falls out of map
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            takeDamage(1);
        }

        if (Input.GetButton("Jump"))
        {
            jumpTimer += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
        } else
        {
            speed = 5f;
        }

        //* Get Jumping
        if (Input.GetButtonUp("Jump") || jumpTimer >= 3)
        {
            if (isGrounded() || canDoubleJump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                if (!isGrounded())
                {
                    canDoubleJump = false;
                    particle.Play();
                    Invoke("stopParticles", 1.0f);
                }
                else
                {
                    if (jumpBoostTimer > 0)
                    {
                        canDoubleJump = true;
                    }
                }
            }

            jumpTimer = 0;
        } 

        if (jumpBoostTimer > 0)
        {
            //Debug.Log("Bouncy");
            jumpBoostTimer -= Time.deltaTime;
        } else
        {
            jumpBoostTimer = 0;
        }

        if (speedBoostTimer > 0)
        {
            speedBoostTimer -= Time.deltaTime;
            //Debug.Log("Super Fast" + speedMultiplier);
        }
        else
        {
            speedBoostTimer = 0;
            speedMultiplier = 1.0f;
        }
    }

    private void stopParticles()
    {
        particle.Stop();
    }

    private void FixedUpdate()
    {
        //* Moving the player based on input
        //Vector3 moveVector = transform.position + horizontalInput * Vector3.right * speed * speedMultiplier;
        //rb.MovePosition(moveVector);
        Vector3 velocity = new Vector3(horizontalInput * speed * speedMultiplier, rb.velocity.y, 0);

        rb.velocity = velocity;
    }

    /// <summary>
    /// Causes the player's health to drop. 
    /// </summary>
    /// <param name="dmg">The amount of health the player will lose</param>
    private void takeDamage(int dmg)
    {
        //health -= dmg;
        GameManager.Instance.TakeDamage(1);

        if (GameManager.Instance.health <= 0)
        {
            GameManager.Instance.health = 3;
            GameManager.Instance.score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    /// <summary>
    /// Checks if the player character is on the ground
    /// </summary>
    /// <returns>Returns true if the player is on the ground</returns>
    public bool isGrounded()
    {
        Vector3 rayStartPosition = transform.position + Vector3.up * 0.1f;

        Vector3 rayDirection = Vector3.down;

        bool grounded =  Physics.Raycast(rayStartPosition, rayDirection, raycastDistance, ground);
        //Debug.Log(grounded);
        
        return grounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Trap")
        {
            takeDamage(1);
        }

        if (collision.collider.tag == "JumpBoost")
        {
            jumpBoostTimer = 30;
        }

        if (collision.collider.tag == "SpeedBoost")
        {
            speedBoostTimer = 5;
            speedMultiplier = 2;
        }

        if (collision.collider.tag == "ScorePickup")
        {
            Destroy(collision.gameObject);
            //Debug.Log("Score added");
            GameManager.Instance.AddScore(50);
        }

        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("Loading Next Scene");
        }
    }
}
