using Const;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (startPoint != null) Gizmos.DrawSphere(startPoint.position, 0.5f);
        if (endPoint != null) Gizmos.DrawSphere(endPoint.position, 0.5f);
        if (startPoint != null && endPoint != null) Gizmos.DrawLine(startPoint.position, endPoint.position);
    }
}
