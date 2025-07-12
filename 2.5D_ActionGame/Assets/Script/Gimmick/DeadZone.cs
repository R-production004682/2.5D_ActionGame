using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : DamageManager
{
    protected override DamageType GetDamageType()
    {
        return DamageType.DeadZone;
    }

    protected override void ApplyDamageEffect(PlayerMaster player)
    {
        var gameManager = GameManager.Instance;
        var characterController = player.GetComponent<CharacterController>();

        if (characterController != null && gameManager.playerRespawnPoint != null)
        {
            // 一時的にCharacterControllerを無効化して、Playerの位置をリセットし、リスポーンさせる
            characterController.enabled = false;
            player.transform.position = gameManager.playerRespawnPoint.transform.position;
            characterController.enabled = true;
        }
    }
}