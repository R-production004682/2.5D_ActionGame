using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [field : Header("Player詳細")]
    [field: SerializeField] public float moveSpeed { get; private set; }
    [field: SerializeField] public float airSpeed { get; private set; }
    [field: SerializeField] public float jumpPower { get; private set; }
    [field: SerializeField] public int jumpCount { get; private set; }
    [field: SerializeField] public int lives { get; private set; }

    [field: Header("重力")]
    [field: SerializeField] public float gravity { get; private set; }
}
