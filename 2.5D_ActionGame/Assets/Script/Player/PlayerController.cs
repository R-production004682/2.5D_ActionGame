using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("依存データ")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerLives playerLives;

    private Rigidbody rb;
    private PlayerContext playerContext;
    private PlayerStateData state;

    private IPlayerAction moveAction;
    private IPlayerAction jumpAction;
    private IPlayerInput playerInput;

    private Transform currentPlatform;
    private Vector3 platformLastPos;
    private Vector3 platformVelocity;

    private bool jumpRequested = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = true;

        state = new PlayerStateData();
        playerContext = new PlayerContext(
            playerData,
            rb,
            state,
            this
        );

        moveAction = new MoveAction();
        jumpAction = new JumpAction();
        playerInput = new DefaultPlayerInput();

        playerLives.Initialize(playerContext);
        moveAction.Initialize(playerContext, playerInput);
        jumpAction.Initialize(playerContext, playerInput);
    }

    private void Update()
    {
        // Fixedupdate内では入力検知漏れがある為、バッファを作成する
        if (playerInput.isJumpPressed())
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        UpdateGroundCheck();
        moveAction.Execute();
        jumpAction.Execute();
    }

    /// <summary>
    /// 地面に接地しているか
    /// </summary>
    private void UpdateGroundCheck()
    {
        var origin = transform.position + Vector3.up * 0.1f;
        if (Physics.SphereCast(origin, 0.2f, Vector3.down, out RaycastHit hit, playerData.groundCheckRaycastLength))
        {
            state.isGrounded = true;

            if (hit.transform != currentPlatform)
            {
                currentPlatform = hit.transform;
                platformLastPos = currentPlatform.position;
            }

            if (currentPlatform != null)
            {
                platformVelocity = (currentPlatform.position - platformLastPos) / Time.fixedDeltaTime;
                platformLastPos = currentPlatform.position;
            }
        }
        else
        {
            state.isGrounded = false;
            currentPlatform = null;
            platformVelocity = Vector3.zero;
        }
    }

    public Vector3 GetPlatformVelocity()
    {
        return platformVelocity;
    }

    /// <summary>
    /// ジャンプリクエストを消費する。
    /// </summary>
    /// <returns></returns>
    public bool ConsumeJumpRequest()
    {
        if (!jumpRequested) return false;
        jumpRequested = false;
        return true;
    }
}
