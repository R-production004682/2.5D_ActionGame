using Const;
using UnityEngine;
using UnityEngine.UIElements;

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
    private IPlayerAction wallJumpAction;
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
        wallJumpAction = new WallJumpAction();
        playerInput = new DefaultPlayerInput();

        playerLives.Initialize(playerContext);
        moveAction.Initialize(playerContext, playerInput);
        jumpAction.Initialize(playerContext, playerInput);
        wallJumpAction.Initialize(playerContext, playerInput);
    }

    private void Update() {}

    private void FixedUpdate()
    {
        UpdateGroundCheck();
        wallJumpAction.Execute();
        jumpAction.Execute();

        if(!playerContext.state.isStickingToWall)
        {
            moveAction.Execute();
        }
    }

    /// <summary>
    /// 地面に接地しているか
    /// </summary>
    private void UpdateGroundCheck()
    {
        var origin = transform.position + Vector3.up * PhysicsInfo.VERSATILE_THRESHOLD;

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

            Debug.DrawRay(hit.point, hit.normal * 0.2f, Color.red);
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


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 origin = transform.position + Vector3.up * PhysicsInfo.VERSATILE_THRESHOLD;
        float radius = 0.4f;
        float length = playerData.groundCheckRaycastLength;

        // 開始点にワイヤースフィア
        Gizmos.DrawWireSphere(origin, radius);

        // レイ方向に線と、終点にワイヤースフィア
        Vector3 end = origin + Vector3.down * length;
        Gizmos.DrawLine(origin, end);
        Gizmos.DrawWireSphere(end, radius);
    }
}
