using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    private PlayerLogic playerLogic;
    private float _wallJumpTimer;
    [SerializeField] private float climbingVineVelocity;
    [SerializeField] private float WallJumpClimbingVineTime;
    [SerializeField] private float WallJumpForce;
    
    void Start()
    {
        playerLogic = GetComponent<PlayerLogic>();
    }

    void Update()
    {
        if (playerLogic.Movement.onWall != 0 && (playerLogic.CurrentState == PlayerLogic.PlayerState.Default || playerLogic.CurrentState == PlayerLogic.PlayerState.Cloak ))
        {
            // Commence a grimper
            if (Input.GetButtonDown("Fire1") )
            {
                playerLogic.CurrentState = PlayerLogic.PlayerState.WallJump;
                _wallJumpTimer = WallJumpClimbingVineTime;
                playerLogic.Movement.rb.velocity = new Vector2(0, climbingVineVelocity); 
            }
        }

        if (playerLogic.CurrentState == PlayerLogic.PlayerState.WallJump)
        {
            //Est en train de grimper
            if (Input.GetButton("Fire1") && _wallJumpTimer >= 0)
            {
                playerLogic.Movement.rb.velocity = new Vector2(0, climbingVineVelocity);
                _wallJumpTimer -= Time.deltaTime;
                //saute alors qu'il grimpe
                if (Input.GetButton("Jump"))
                {
                    playerLogic.Movement.rb.velocity = new Vector2(-1 * playerLogic.Movement.onWall, 1) * WallJumpForce;
                    _wallJumpTimer = -1;
                    playerLogic.CurrentState = PlayerLogic.PlayerState.Default;
                }
                //arrive a la fin en hauteur du mur et passe par dessus
                else if (playerLogic.Movement.onWall == 0)
                {
                    playerLogic.Movement.rb.velocity = 0.5f*WallJumpForce * new Vector2(playerLogic.Movement.onWall, 1);
                    _wallJumpTimer = -1;
                    playerLogic.CurrentState = PlayerLogic.PlayerState.Default;
                }
            }
            //a fini de grimper et retombe
            else
            {
                playerLogic.Movement.rb.velocity = WallJumpForce * 0.3f * new Vector2(-1 * playerLogic.Movement.onWall, -1);
                _wallJumpTimer = -1;
                playerLogic.CurrentState = PlayerLogic.PlayerState.Default;
            }
        }
    }
}

