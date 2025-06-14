using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Player�ڍ�")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float airSpeed;
    [SerializeField] public float jumpPower;
    [SerializeField] public int jumpCount;

    [Header("�d��")]
    public float gravity = 9.8f;
}
