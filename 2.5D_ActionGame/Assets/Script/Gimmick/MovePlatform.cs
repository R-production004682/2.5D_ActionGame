using Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private Transform startPoint, endPoint;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float waitTime = 0.5f;

    private bool isReturning = false;
    private bool isStopped = false;

    public Vector2 CurrentVelocity {get; private set; } = Vector2.zero;

    private void Awake()
    {
        if(startPoint == null || endPoint == null)
        {
            Debug.LogError($"移動床{this.gameObject}の開始地点または、終了地点が設定されていません。");
        }
    }


    private void FixedUpdate()
    {
        if(isStopped)
        {
            CurrentVelocity = Vector2.zero;
            return;
        }
        
        var oldPosition = transform.position;

        MovePlatformStep();
        CheckArraivalAndToggleDirection();

        CurrentVelocity = (transform.position - oldPosition) / Time.fixedDeltaTime;
    }

    // 現在のターゲット地点を返す
    private Vector3 CurrentTargetPosition => isReturning ? startPoint.position : endPoint.position;

    /// <summary>
    /// 床を動かす
    /// </summary>
    private void MovePlatformStep()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentTargetPosition, moveSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// 目的地までの到着を確認し、方向転換させる
    /// </summary>
    private void CheckArraivalAndToggleDirection()
    {
        if(Vector3.Distance(transform.position, CurrentTargetPosition) < GimmickInfo.MOVE_FLOOR_REACH_THRESHOLD)
        {
            isReturning = !isReturning;
            StartCoroutine(PlatformPauseCoroutine());
        }
    }
    
    /// <summary>
    /// 目的地到着後数秒待つ
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlatformPauseCoroutine() 
    {
        isStopped = true;
        yield return new WaitForSeconds(waitTime);
        isStopped = false;
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
