using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 0.5f;
    public float jumpForce = 5.0f;
    private Text scoreText;
    public int health = 3;
    private bool canDoubleJump;
    public LayerMask ground;
    private float timer = 0;

    private float raycastDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //scoreText = GameObject.FindWithTag("Score").GetComponent<Text>();
        canDoubleJump = true;
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
            timer += Time.deltaTime;
        }

        //* Get Jumping
        if (Input.GetButtonUp("Jump") || timer >= 3)
        {
            if (isGrounded() || canDoubleJump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                if (!isGrounded())
                {
                    canDoubleJump = false;
                } else
                {
                    canDoubleJump = true;
                }
            }

            timer = 0;
        } 
    }

    private void FixedUpdate()
    {
        //* Moving the player based on input
        Vector3 moveVector = transform.position + horizontalInput * Vector3.right * speed;
        rb.MovePosition(moveVector);
    }

    /// <summary>
    /// Causes the player's health to drop. 
    /// </summary>
    /// <param name="dmg">The amount of health the player will lose</param>
    private void takeDamage(int dmg)
    {
        health -= dmg;
    }

    /// <summary>
    /// Checks if the player character is on the ground
    /// </summary>
    /// <returns>Returns true if the player is on the ground</returns>
    private bool isGrounded()
    {
        Vector3 rayStartPosition = transform.position + Vector3.up * 0.1f;

        Vector3 rayDirection = Vector3.down;

        bool grounded =  Physics.Raycast(rayStartPosition, rayDirection, raycastDistance, ground);
        Debug.Log(grounded);
        
        return grounded;
    }
}
