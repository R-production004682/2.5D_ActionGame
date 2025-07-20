using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    public static ElevatorManager Instance {get; private set; }

    private Dictionary<ElevatorID, ElevatorPoint[]> elevatorMap = new();

    private void Awake()
    {
        if(Instance != null && Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        StartCoroutine(DelayedRegister());
    }

    private void RegisterAllElevators()
    {
        var grouped = ElevatorPoint.AllPoints.GroupBy(point => point.elevatorID);
        Debug.Log($"[ElevatorManager] RegisterAllElevators 開始: ElevatorPoint.AllPoints.Count = {ElevatorPoint.AllPoints.Count}");
        foreach (var group in grouped)
        {
            var points = group.ToArray();
            if(points.Length != 2)
            {
                Debug.LogError($"ElevatorID: {group.Key} に対するポイントが2つありません！");
                continue;
            }
            elevatorMap[group.Key] = points;
        }
    }

    public Transform GetExitPoint(ElevatorID id, ElevatorPoint elevatorPoint)
    {
        Debug.Log($"elevatorMap.Count : {elevatorMap.Keys.Count}, elevatorMap.Name : {elevatorMap.Keys}");


        // これをDebug出力してチェック
        foreach (var point in ElevatorPoint.AllPoints)
        {
            Debug.Log($"ElevatorID: {point.elevatorID}, isEntrance: {point.isEntrance}, pos: {point.transform.position}");
        }

        if (!elevatorMap.TryGetValue(id, out var pair))
        {
            Debug.LogWarning($"ElevatorID: {id} に対応するエレベータペアが見つかりません！");
            return null;
        }

        if (pair == null || pair.Length != 2)
        {
            Debug.LogError($"ElevatorID: {id} に対応するポイントが無効です。pairがnullか、数が2でない");
            return null;
        }

        if (pair[0] == null || pair[1] == null)
        {
            Debug.LogError($"ElevatorID: {id} のエレベータポイントにnullが含まれています");
            return null;
        }

        if (elevatorPoint == null)
        {
            Debug.LogError("呼び出し元の elevatorPoint が null です！");
            return null;
        }

        if (Vector3.Distance(pair[0].transform.position, elevatorPoint.transform.position) < 0.5f)
        {
            return pair[1].transform;
        }
        else
        {
            return pair[0].transform;
        }
    }

    private IEnumerator DelayedRegister()
    {
        yield return null;
        RegisterAllElevators();
    }

}
