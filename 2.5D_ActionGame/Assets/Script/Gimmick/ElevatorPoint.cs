using System.Collections.Generic;
using UnityEngine;

public enum ElevatorID
{
    ElevatorA,
    ElevatorB,
    ElevatorC,
}

public class ElevatorPoint : MonoBehaviour
{
    public ElevatorID elevatorID;
    public bool isEntrance;

    public static List<ElevatorPoint> AllPoints { get; private set; } = new List<ElevatorPoint>();

    private void Awake()
    {
        Debug.Log($"[ElevatorPoint] Awake: elevatorID = {elevatorID}, isEntrance = {isEntrance}");

        if (!AllPoints.Contains(this))
        {
            AllPoints.Add(this);
        }
    }

    private void OnDestroy()
    {
        AllPoints.Remove(this);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = isEntrance ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position, 0.3f);
        UnityEditor.Handles.Label(transform.position + Vector3.up * 0.5f, $"{elevatorID} ({(isEntrance ? "入口" : "出口")})");
    }
#endif
}