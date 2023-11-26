using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
	private Animator animator;
	private SpriteRenderer sprite;

	private int healthPoint;
	private float immunityTimer;
	[SerializeField] private float immunityTime = 1f;
	
	[SerializeField] private float coyoteTime = 0.05f;
    private float _coyoteTimer;

    [SerializeField] private float jumpBufferTime = 0.15f;
    private float _jumpBufferTimer;

    public static PlayerController instance;
	
	
	const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_WALK = "Player_Walk";
    const string PLAYER_AIR = "Player_InAir";
	const string PLAYER_LAND = "Player_Landing";
	private bool _haslanded = true;
	private string currentState;


	

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
		healthPoint = 3;
		jumpForce = Mathf.Sqrt(2 * rb.gravityScale * 9.81f * jumpHeight);
		currentState = PLAYER_IDLE;
		immunityTimer = 0f;
    }

    private void Awake()
    {
	    instance = this;
    }

    void Update()
    {
	    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2.5f, LayerMask.GetMask("Ground"));
	    if (hit.collider != null)
	    {
		    isGrounded = true;
		    _coyoteTimer = coyoteTime;
	    }
	    else
	    {
		    isGrounded = false;
	    }
	    
		// On decremente les compteurs
	    if (_jumpBufferTimer > -1) _jumpBufferTimer -= Time.deltaTime;
	    if (_coyoteTimer > -1) _coyoteTimer -= Time.deltaTime;
	    if (immunityTimer > -1) immunityTimer -= Time.deltaTime;
	    
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
		
		if(Vector2.Distance(rb.velocity, Vector2.zero) <= 0.1)
			sprite.flipX = (horizontalInput < 0);

        if (isGrounded)
        {
	        _coyoteTimer = coyoteTime;
			
			if(_haslanded){
				if(Vector2.Distance(rb.velocity, Vector2.zero) <= 0.1) changeAnimationState(PLAYER_IDLE);
				else changeAnimationState(PLAYER_WALK);
			} else {
				changeAnimationState(PLAYER_LAND);
				_haslanded = true;
			}
        } 
		else changeAnimationState(PLAYER_AIR);
        
        // Jump input
        if (Input.GetKeyDown(KeyCode.Space))
        {
			
			// Jump pendant le coyoteTime
			if (_coyoteTimer >= 0f)
			{
				Jump(jumpForce);
				_coyoteTimer = -1f;
			}
			
			else if (!isGrounded && canDoubleJump)
			{
				Jump(doubleJumpForce);
				canDoubleJump = false;
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
                }


                //Le joueur �tant au sol, on remets a jour les variables de mouvements
				canDoubleJump = true;
				isDashing = false;
                _jumpBufferTimer = -1;

				
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


    private void changeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
	
    

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Enemy") && immunityTimer < 0)
		{
			healthPoint -= 1;
			immunityTimer = immunityTime;
			Destroy(other.gameObject);
			if (healthPoint == 0)
			{
				GameOver();
			}
		}
	}


	private void GameOver()
	{
		rb.isKinematic = true;
	}
}