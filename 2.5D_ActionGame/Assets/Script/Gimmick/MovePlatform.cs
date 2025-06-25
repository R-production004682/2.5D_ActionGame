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

    private const float reachThreshold = 0.01f;

    private void Update()
    {
        if(isStopped)
        {
            return;
        }

        MovePlatformStep();
        CheckArraivalAndToggleDirection();
    }

    /// <summary>
    /// 床を動かす
    /// </summary>
    private void MovePlatformStep()
    {
        var targetPoint = isReturning ? startPoint.position : endPoint.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 目的地までの到着を確認し、方向転換させる
    /// </summary>
    private void CheckArraivalAndToggleDirection()
    {
        var targetPoint = isReturning ? startPoint.position : endPoint.position;

        if(Vector3.Distance(transform.position, targetPoint) < reachThreshold)
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

        if(startPoint != null)
        {
            Gizmos.DrawSphere(startPoint.position, 0.5f);
        } 

        if(endPoint != null)
        {
            Gizmos.DrawSphere(endPoint.position, 0.5f);
        }
    }

}
