using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [field : Header("Player詳細")]
    [field: SerializeField, Range(0, 10)] public float moveSpeed { get; private set; }
    [field: SerializeField, Range(0, 10)] public float airSpeed { get; private set; }
    [field: SerializeField, Range(0, 5)] public float objectDragingSpeed { get; private set; }
    [field: SerializeField, Range(0, 50)] public float jumpForce { get; private set; }
    [field: SerializeField, Range(1, 3)] public int jumpCount { get; private set; }

    [Range(0, 20)]
    [SerializeField] public int lives;

    [field: Header("物理計算パラメータ")]
    [field: SerializeField, Range(0, 1)] public float groundCheckRaycastLength { get; private set; }
    [field: SerializeField, Range(0, 1)] public float wallCheckRaycastLength { get; private set; }
    [field: SerializeField, Range(0, 1)] public float wallJumpGravityScale { get; private set; }
    [field: SerializeField] public bool wallJumpGravityScaleAvailable { get; private set; } = true;
}
