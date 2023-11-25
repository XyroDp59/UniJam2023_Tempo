using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{ 
    public PlayerMovement Movement { get; private set; }
    
    public enum PlayerState
    {
        Default,    // le joueur idle ou marche ou jump
        WallJump,   // grimpe des vignes et fait un wall jump
        Cloak,      // cape de camouflage
        SeedGun,    // tire des graines pour avoir un mini boost de respiration + sonner les ennemis
        OakenShield, // shield
        HookShot,   // etire son bras pour attraper des objets, peut servir de lassot à la wind waker
        Swinging,   // le joueur se balance a la tarzan
        Infiltration, // le joueur s'infiltre dans des petits interstices, fissures ou canalisation -> gameplay type pacman
        TightRope,  // funambulisme
        Possession, // le joueur possede un ennemi pendant un temps, avant de basculer dans une scene correspondante
        Hit,        // le joueur vient de prendre un dégat et il y a un cooldown
    }
    public PlayerState CurrentState;
    
    
    private float _breath;
    [SerializeField] private float breathCapacity;
    [SerializeField] private int breathNum;
    public Text breathText;
    public float Breath
    { 
        get {
            return _breath;
        }
        set
        {
            _breath = Mathf.Clamp(value, 0, BreathDuration);
            breathText.text = "Breath : "  + Mathf.RoundToInt(_breath).ToString() + "/" + BreathDuration.ToString();
            if (_breath == 0) SoftGameOver();
        }
    }
    public float BreathDuration
    {
        get {
            return breathCapacity * breathNum;
        }
        set
        {
            breathNum = (int)value;
        }
    }

    
    private float _health;
    [SerializeField] private float _healthCapacity;
    [SerializeField] private int _ectoplasmNum;
    public Text healthText;
    public float Health
    { 
        get {
            return _health;
        }
        set
        {
            _health = Mathf.Clamp(value, 0, maxHealth);
            healthText.text = "Ectoplasm : "  + Mathf.RoundToInt(_health).ToString() + "/" + maxHealth.ToString();
            if (_health == 0) GameOver();
        }
    }
    public float maxHealth
    {
        get
        {
            return _healthCapacity * _ectoplasmNum;
        }
        set
        {
            _ectoplasmNum = (int)value;
        }
    }
    
    private void SoftGameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void Awake()
    {
        Breath = BreathDuration;
        Health = maxHealth;
        Movement = GetComponent<PlayerMovement>();
        CurrentState = PlayerState.Default;
    }

    private void FixedUpdate()
    {
        Breath -= Time.deltaTime;
    }
}
