using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement movement;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
        rb = GetComponent<Rigidbody>();
        animator.SetBool("isGrounded", true);
    }

    // Update is called once per frame
    public void Update()
    {
        animator.SetFloat("CharacterSpeed", rb.velocity.magnitude);
        //Debug.Log(rb.velocity.magnitude);
        animator.SetBool("isGrounded", movement.isGrounded());
        if (Input.GetButtonUp("Jump") && !movement.isGrounded())
        {
            animator.SetTrigger("doFlip");
        }
    }
}
