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

    private void Update() {}

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

        // Ray式の当たり判定ではなく、球面キャストを使用してより広い範囲での接地判定を行う
        if (Physics.SphereCast(origin, 0.4f, Vector3.down, out RaycastHit hit, playerData.groundCheckRaycastLength))
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
    public bool ConsumeJumpRequest() => InputBuffer.Instance.ConsumeJump();
}
