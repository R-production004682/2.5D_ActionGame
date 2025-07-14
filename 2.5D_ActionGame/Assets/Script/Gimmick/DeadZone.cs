using UnityEngine;

public class DeadZone : DamageManager
{
    protected override DamageType GetDamageType()
    {
        return DamageType.DeadZone;
    }

    protected override void ApplyDamageEffect(PlayerController player)
    {
        var gameManager = GameManager.Instance;
        var rb = player.GetComponent<Rigidbody>();

        if (rb != null && gameManager.playerRespawnPoint != null)
        {
            rb.velocity = Vector3.zero;
            player.transform.position = gameManager.playerRespawnPoint.transform.position;
        }
    }
}
