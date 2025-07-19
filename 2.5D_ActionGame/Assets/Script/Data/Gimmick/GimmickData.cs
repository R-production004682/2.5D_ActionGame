using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GimmickData")]
public class GimmickData : ScriptableObject
{
    [field: Header("Gimmickの全体の基本パラメータ")]
    [field: SerializeField] public float speed { get; private set; } = 2.0f;
    [field: SerializeField] public float reachThreshold { get; private set; } = 0.1f;

    [SerializeField] public float pauseTime;
}
