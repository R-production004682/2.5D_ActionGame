using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Player詳細")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float airSpeed;
    [SerializeField] public float jumpPower;
    [SerializeField] public int jumpCount;
    [SerializeField] public int lives;

    [Header("重力")]
    public float gravity = 9.8f;
}
