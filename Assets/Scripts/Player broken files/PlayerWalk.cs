using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    [SerializeField] private float runningSpeed = 11f;
    private bool hasRunningAsc = true;
    private float runningAcceleration = 200f;
    private bool hasRunningDesc = true;
    private float runningDesceleration = 200f;

    [SerializeField] private float fallingSpeed = 10f;
    private bool hasFallingAsc = true;
    private float fallingAcceleration = 50f;
    private bool hasFallingDesc = true;
    private float fallingDesceleration = 50f;

    private PlayerLogic playerLogic;
    private bool onGround;
    private float dirX;


    // Start is called before the first frame update
    void Start()
    {
        playerLogic = GetComponent<PlayerLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = playerLogic.Movement.onGround;
        dirX = Input.GetAxisRaw("Horizontal");

        // deplacement horizontale -----------------------------------------------------------------------------------------
        if (playerLogic.CurrentState == PlayerLogic.PlayerState.Default || playerLogic.CurrentState == PlayerLogic.PlayerState.Cloak)
        {
            if (onGround)
            {
                horizontalMovement(dirX, runningSpeed, runningAcceleration, runningDesceleration, hasRunningAsc,
                    hasRunningDesc);
            }
            else
            {
                horizontalMovement(dirX, fallingSpeed, fallingAcceleration, fallingDesceleration, hasFallingAsc,
                    hasFallingDesc);
            }
        }


        // lock la rotation du perso
        transform.rotation = Quaternion.identity;

    }



    private void horizontalMovement(float dirX, float speed, float acceleration, float desceleration, bool hasAsc, bool hasDesc)
    {
        //Acceleration
        if (playerLogic.Movement.rb.velocity.x * dirX < speed && dirX != 0)
        {
            playerLogic.Movement.rb.velocity += new Vector2(dirX * acceleration * Time.deltaTime, 0);
            if (!hasAsc)
            {
                playerLogic.Movement.rb.velocity = new Vector2(dirX * speed, playerLogic.Movement.rb.velocity.y);
            }
        }
        //vitesse constante
        if (playerLogic.Movement.rb.velocity.x * dirX >= speed && dirX != 0)
        {
            playerLogic.Movement.rb.velocity = new Vector2(dirX * speed, playerLogic.Movement.rb.velocity.y);
        }
        //desceleration
        if (System.Math.Abs(playerLogic.Movement.rb.velocity.x) - desceleration * Time.deltaTime >= 0 && dirX == 0)
        {
            if (playerLogic.Movement.rb.velocity.x > 0) { playerLogic.Movement.rb.velocity -= new Vector2(desceleration * Time.deltaTime, 0); }
            else { playerLogic.Movement.rb.velocity -= new Vector2(-desceleration * Time.deltaTime, 0); }

            if (!hasDesc)
            {
                playerLogic.Movement.rb.velocity = new Vector2(0, playerLogic.Movement.rb.velocity.y);
            }
        }

    }
}
