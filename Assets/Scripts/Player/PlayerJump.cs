using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    // Initialise Generic variables -----------------------
    private PlayerMovement playerMvmt;
    private bool onGround;
    private int onWall;

    // Initialise Mouvement variables ----------------------
    [SerializeField] private float jumpHeight;
    [SerializeField] private float hopHeight;
    private float hopForce;
    private float jumpForce;

    [SerializeField] private float maxHoldingJumpTime = 0.35f;
    private float _jumpTimer;
    private bool _currentlyJumping;

    [SerializeField] private float coyoteTime = 0.05f;
    private float _coyoteTimer;

    [SerializeField] private float jumpBufferTime = 0.15f;
    private float _jumpBufferTimer;

    [SerializeField] private int maxDoubleJumpNum;
    private int maxDoubleJumpCount;


    private void Start()
    {
        playerMvmt = GetComponent<PlayerMovement>();
        jumpForce = Mathf.Sqrt(2 * playerMvmt.rb.gravityScale * 9.81f * (jumpHeight-hopHeight));
        hopForce = hopHeight / maxHoldingJumpTime;
    }


    private void Update()
    {
        onGround = playerMvmt.onGround;
        onWall = playerMvmt.onWall;


        if (onWall == 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                // Jump pendant le coyoteTime
                if (_coyoteTimer >= 0f)
                {
                    Jump();
                    _coyoteTimer = -1f;
                }

                // ou alors le joueur double jump
                else if (!onGround && maxDoubleJumpCount < maxDoubleJumpNum - 1)
                {
                    Jump();
                    maxDoubleJumpCount += 1;
                }

                // ou on active le timer pour le jump buffer
                else if (!onGround)
                {
                    _jumpBufferTimer = jumpBufferTime;
                }
            }

            // Saut normal, qui peut durer plus ou moins longtemps
            if (Input.GetButton("Jump") && _currentlyJumping)
            {
                if (_jumpTimer <= maxHoldingJumpTime)
                {
                    playerMvmt.rb.velocity = new Vector2(playerMvmt.rb.velocity.x, hopForce);
                }

                if (_jumpTimer <= Time.deltaTime && _jumpTimer >= 0)
                {
                    Jump();
                    _currentlyJumping = false;
                }
            }

            if (Input.GetButtonUp("Jump")) _currentlyJumping = false;
            if (onGround)
            {
                //Si le joueur touche le sol alors qu'il avait pr�vu de sauter via un jump buffer, alors il saute
                if (_jumpBufferTimer > 0)
                {
                    Jump();
                }


                //Le joueur �tant au sol, on remets a jour les variables de mouvements
                _coyoteTimer = coyoteTime;
                maxDoubleJumpCount = 0;
                _jumpBufferTimer = -1;
                _jumpTimer = maxHoldingJumpTime;

                if (Input.GetButtonDown("Jump")) _currentlyJumping = true;
            }


            // On decremente les compteurs
            if (_coyoteTimer > -1) _coyoteTimer -= Time.deltaTime;
            if (_jumpBufferTimer > -1) _jumpBufferTimer -= Time.deltaTime;
            if (_jumpTimer > -1) _jumpTimer -= Time.deltaTime;
        }
    }

    private void Jump()
    {
        playerMvmt.rb.velocity = new Vector2(playerMvmt.rb.velocity.x, jumpForce);
    }

}