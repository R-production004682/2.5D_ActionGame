using Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovePlatform : MonoBehaviour
{
    [SerializeField] private Transform startPoint, endPoint;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float waitTime = 0.5f;

    private bool isReturning = false;
    private bool isStopped = false;
    private Coroutine pauseCoroutine;
    private Vector3 lastVelocity = Vector3.zero;

    public Vector3 CurrentVelocity {get; private set; } = Vector3.zero;

    private Rigidbody rb;

    private void Awake()
    {
        if (startPoint == null || endPoint == null)
        {
            Debug.LogError($"移動床{this.gameObject}の開始地点または、終了地点が設定されていません。");
        }

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void FixedUpdate()
    {
        if (isStopped)
        {
            CurrentVelocity = Vector3.zero;
            return;
        }

        var prevPos = rb.position;

        MovePlatformStep();
        TryToggleDirection();

        var rawVelocity = (rb.position - prevPos) / Time.fixedDeltaTime * GimmickInfo.VELOCITY_SCALE_FACTOR;
        CurrentVelocity = Vector3.Lerp(lastVelocity, rawVelocity, 0.2f);
        lastVelocity = CurrentVelocity;
    }

    // 現在のターゲット地点を返す
    private Vector3 CurrentTargetPosition => isReturning ? startPoint.position : endPoint.position;

    /// <summary>
    /// 床を動かす
    /// </summary>
    private void MovePlatformStep()
    {
        Vector3 target = Vector3.MoveTowards(rb.position, CurrentTargetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(target);
    }

    /// <summary>
    /// 目的地までの到着を確認し、方向転換させる
    /// </summary>
    private void TryToggleDirection()
    {
        if (Vector3.Distance(transform.position, CurrentTargetPosition) <
                GimmickInfo.MOVE_FLOOR_REACH_THRESHOLD && pauseCoroutine == null)
        {
            isReturning = !isReturning;
            pauseCoroutine = StartCoroutine(PauseBeforeReverse());
        }
    }

    /// <summary>
    /// 目的地到着後数秒待つ
    /// </summary>
    /// <returns></returns>
    private IEnumerator PauseBeforeReverse()
    {
        isStopped = true;
        yield return new WaitForSeconds(waitTime);
        isStopped = false;
        pauseCoroutine = null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (startPoint != null)
        {
            Gizmos.DrawSphere(startPoint.position, 0.5f);
        } 

        if(endPoint != null)
        {
            Gizmos.DrawSphere(endPoint.position, 0.5f);
        }

        if (startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(startPoint.position, endPoint.position);
        }
    }
}
