using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
	[SerializeField] private float jumpHeight = 5f;
    public float jumpForce;
    public float doubleJumpForce = 7f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.5f;

    private bool isGrounded;
    private bool canDoubleJump;
    private bool isDashing;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		jumpForce = Mathf.Sqrt(2 * rb.gravityScale * 9.81f * jumpHeight);
    }

    void Update()
    {

        // Reset double jump ability when grounded
        if (isGrounded)
        {
            canDoubleJump = true;
            isDashing = false;
        }

        // Handle player input
        float horizontalInput = Input.GetAxis("Horizontal");

        // Dash input
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            StartCoroutine(Dash());
        }

        // Move the player horizontally
        Vector3 movement = new Vector3(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump(jumpForce);
            }
            else if (canDoubleJump)
            {
                Jump(doubleJumpForce);
                canDoubleJump = false;
            }
        }
    }

    void Jump(float jumpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        float dashTime = 0f;

        while (dashTime < dashDuration)
        {
            rb.velocity = new Vector2(transform.localScale.x * dashSpeed, rb.velocity.y);
            dashTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }




}