using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("PlayerÚ×")]
    [SerializeField] public float moveSpeed;

    [Header("d—Í")]
    public float gravity = 9.8f;
}
