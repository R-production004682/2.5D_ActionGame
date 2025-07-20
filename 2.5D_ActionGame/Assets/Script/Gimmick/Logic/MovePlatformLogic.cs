using Const;
using UnityEditor;
using UnityEngine;

public class MovePlatformLogic : MonoBehaviour, IGimmickLogic
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private GimmickContext context;
    private float pauseTimer;
    private bool isReturning;
    private bool isPaused;
    private Vector3 lastVelocity = Vector3.zero;

    public Vector3 CurrentVelocity { get; private set; } = Vector3.zero;

    public void Initialize(GimmickContext context)
    {
        this.context = context;
    }

    public void Execute()
    {
        if (isPaused)
        {
            pauseTimer -= Time.fixedDeltaTime;
            if (pauseTimer <= 0f)
            {
                isPaused = false;
            }
            CurrentVelocity = Vector3.zero;
            return;
        }

        var rb = context.rigidbody;
        var prevPos = rb.position;

        MovePlatformStep();
        TryToggleDirection();

        var rawVelocity = (rb.position - prevPos) / Time.fixedDeltaTime * GimmickInfo.VELOCITY_SCALE_FACTOR;
        CurrentVelocity = Vector3.Lerp(lastVelocity, rawVelocity, 0.2f);
        lastVelocity = CurrentVelocity;
    }

    private Vector3 CurrentTargetPosition => isReturning ? startPoint.position : endPoint.position;

    private void MovePlatformStep()
    {
        var target = Vector3.MoveTowards(
            context.rigidbody.position,
            CurrentTargetPosition,
            context.data.speed * Time.fixedDeltaTime
        );
        context.rigidbody.MovePosition(target);
    }

    private void TryToggleDirection()
    {
        var currentPosition = context.rigidbody.position;
        if (Vector3.Distance(currentPosition, CurrentTargetPosition) < context.data.reachThreshold)
        {
            isReturning = !isReturning;
            isPaused = true;
            pauseTimer = context.data.pauseTime;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (startPoint == null || endPoint == null) return;

        // 線の色
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(startPoint.position, endPoint.position);

        // 開始・終了点のスフィア
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(startPoint.position, 0.3f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(endPoint.position, 0.3f);

        // ラベル表示
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        Handles.Label(startPoint.position + Vector3.up * 0.5f, "Start", style);
        Handles.Label(endPoint.position + Vector3.up * 0.5f, "End", style);

        // → 向きの矢印（中間点から方向を示す）
        Vector3 dir = (endPoint.position - startPoint.position).normalized;
        Vector3 mid = Vector3.Lerp(startPoint.position, endPoint.position, 0.5f);
        Handles.color = Color.yellow;
        Handles.ArrowHandleCap(0, mid, Quaternion.LookRotation(dir), 1.0f, EventType.Repaint);
    }
#endif
}
