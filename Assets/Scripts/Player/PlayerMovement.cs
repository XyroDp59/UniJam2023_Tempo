using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool onGround { get; private set; }
    public int onWall { get; private set; }

    [SerializeField] private LayerMask groundLayer;
    public Collider2D col;
    public Rigidbody2D rb;
    
    private float _hurtTimer;
    private bool _isPlayerHurt = false;
    [SerializeField] private float hurtMaxTime = 0.5f;

    private PlayerLogic _playerLogic;
    
    
    
    
    private bool hasSideCollided(Vector2 vect, LayerMask layer, float percent)
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size * percent, 0f, vect, .1f + col.bounds.size.x*(1-percent)/2, layer);
    }

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        _playerLogic = GetComponent<PlayerLogic>();
    }
    
    private void FixedUpdate()
    {
        onGround = hasSideCollided(Vector2.down, groundLayer, 1f);
        if (hasSideCollided(Vector2.right, groundLayer, 0.5f)) onWall = 1;
        else if (hasSideCollided(Vector2.left, groundLayer,0.5f)) onWall = -1;
        else onWall = 0;
    }
    
    
    public void PlayerIsHurt()
    {
        _playerLogic.CurrentState = PlayerLogic.PlayerState.Hit;
        _hurtTimer = hurtMaxTime;
        _isPlayerHurt = true;
    }

    private void ToggleCloak()
    {
        if (_playerLogic.CurrentState == PlayerLogic.PlayerState.Default)
        {
            _playerLogic.CurrentState = PlayerLogic.PlayerState.Cloak;
        }
        else if (_playerLogic.CurrentState == PlayerLogic.PlayerState.Cloak)
        {
            _playerLogic.CurrentState = PlayerLogic.PlayerState.Default;
        }
    }
    

    void Update()
    {
        if (_isPlayerHurt)
        {
            if (_hurtTimer <= 0)
            {
                _playerLogic.CurrentState = PlayerLogic.PlayerState.Default;
                _isPlayerHurt = false;
            }
            else _hurtTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            ToggleCloak();
        }

    }
    
    
    
    
    
    
 
    
    
    
    
    
    
    
    
}
