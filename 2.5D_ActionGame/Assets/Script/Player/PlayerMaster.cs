using Const;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Move,
    Jump,
    Air,
    Attack
}

public class PlayerMaster : MonoBehaviour
{
    [Header("Playerの構成要素")]
    [SerializeField] protected PlayerData playerData;

    private PlayerContex playerContex;
    private PlayerMove move;
    private PlayerJump jump;
    private PlayerLives lives;

    private CharacterController characterController;
    
    private Vector3 platformVelocity = Vector3.zero;

    private PlayerState currentState;
    public PlayerState CurrentState
    {
        get => currentState;
        set
        {
            if(currentState != value)
            {
                currentState = value;
            }
        }
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        playerContex = new PlayerContex
        {
           playerData = this.playerData,
           characterController = this.characterController,
           master = this
        };

        move = GetComponent<PlayerMove>();
        jump = GetComponent<PlayerJump>();
        lives = GetComponent<PlayerLives>();

        move?.Initialized(playerContex);
        jump?.Initialized(playerContex);
        lives?.Initialized(playerContex);
    }

    private void Update()
    {
        HandlerState();
    }

    private void FixedUpdate()
    {
        if(!characterController.isGrounded)
        {
            platformVelocity = Vector3.zero;
        }

        var totalVelocity = playerContex.velocity + platformVelocity;
        characterController.Move(totalVelocity * Time.fixedDeltaTime);
    }

    private void HandlerState()
    {
        move.HandleMove();
        jump.HandlerJump();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag(TagInfo.MOVE_PLATFORM))
        {
            var platform = hit.collider.GetComponent<MovePlatform>();
            if(platform != null)
            {
                platformVelocity = platform.CurrentVelocity;
                return;
            }
        }
        platformVelocity = Vector3.zero;
    }
}
