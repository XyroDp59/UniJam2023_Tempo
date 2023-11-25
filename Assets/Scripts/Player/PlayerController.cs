using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
	[SerializeField] private float jumpHeight = 5f;
    private float jumpForce;
    [SerializeField] private float doubleJumpForce = 7f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.5f;

    private bool isGrounded;
    private bool canDoubleJump;
    private bool isDashing;

    private Rigidbody2D rb;
	
	
	[SerializeField] private float coyoteTime = 0.05f;
    private float _coyoteTimer;

    [SerializeField] private float jumpBufferTime = 0.15f;
    private float _jumpBufferTimer;
	
	
	

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		jumpForce = Mathf.Sqrt(2 * rb.gravityScale * 9.81f * jumpHeight);
    }

	
    void Update()
    {
		isGrounded = Physics.Raycast(transform.position, -Vector3.up * 0.05f, out hit);
		
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
			
			// Jump pendant le coyoteTime
			if (_coyoteTimer >= 0f)
			{
				Jump(jumpForce);
				_coyoteTimer = -1f;
				Debug.Log("Coyote");
			}
			
			else if (!isGrounded && canDoubleJump)
			{
				Jump(doubleJumpForce);
				canDoubleJump = false;
				Debug.Log("Double Jump");
			}
						// ou on active le timer pour le jump buffer
			else if (!isGrounded)
			{
				_jumpBufferTimer = jumpBufferTime;
			}
			
			
			if (isGrounded)
            {
                //Si le joueur touche le sol alors qu'il avait pr�vu de sauter via un jump buffer, alors il saute
                if (_jumpBufferTimer > 0)
                {
                    Jump(jumpForce);
					Debug.Log("JumpBuffer");
                }


                //Le joueur �tant au sol, on remets a jour les variables de mouvements
                _coyoteTimer = coyoteTime;
				canDoubleJump = true;
				isDashing = false;
                _jumpBufferTimer = -1;

				// On decremente les compteurs
				Debug.Log("grounded");
				if (_coyoteTimer > -1) _coyoteTimer -= Time.deltaTime;
				if (_jumpBufferTimer > -1) _jumpBufferTimer -= Time.deltaTime;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
		Debug.Log(collision.gameObject);
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
		Debug.Log(collision.gameObject);
        isGrounded = false;
    }
	


}