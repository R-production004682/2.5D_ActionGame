using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [field : Header("Player詳細")]
    [field: SerializeField, Range(0, 50)] public float moveSpeed { get; private set; }
    [field: SerializeField, Range(0, 10)] public float airSpeed { get; private set; }
    [field: SerializeField, Range(0, 10)] public float jumpPower { get; private set; }
    [field: SerializeField, Range(1, 3)] public int jumpCount { get; private set; }

    [Range(0, 20)]
    [SerializeField] public int lives;

    [field: Header("重力")]
    [field: SerializeField] public float gravity { get; private set; }
}
