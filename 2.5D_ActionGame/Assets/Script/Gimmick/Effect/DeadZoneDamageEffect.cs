using Const;
using UnityEngine;

public class DeadZoneDamageEffect : MonoBehaviour, IGimmickEffect
{
    [SerializeField] private DamageData damageData;

    private GimmickContext context;

    public void Initialize(GimmickContext context)
    {
        this.context = context;
    }

    public void ApplyEffect(Collider other)
    {
        if(!other.CompareTag(TagInfo.PLAYER))
        {
            return;
        }

        var gameManager = GameManager.Instance;
        var player = other.GetComponent<PlayerController>();
        var lives = other.GetComponent<PlayerLives>();

        if (player == null || lives == null)
        {
            return;
        }

        lives.TakeDamage(damageData.deadZoneAmount);

        var rb = player.GetComponent<Rigidbody>();
        if (rb != null && gameManager.playerRespawnPoint != null)
        {
            rb.velocity = Vector3.zero;
            player.transform.position = gameManager.playerRespawnPoint.transform.position;
        }
    }
}
