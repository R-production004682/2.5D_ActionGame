using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private PlayerContex playerContex;

    public void Initialized(PlayerContex contex)
    {
        playerContex = contex;
    }

    /// <summary>
    /// Playerのジャンプロジック
    /// </summary>
    public void HandlerJump()
    {
        // Playerの接地判定
        if (playerContex.characterController.isGrounded == true)
        {
            playerContex.master.currentState = PlayerState.Idle;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerContex.master.currentState = PlayerState.Jump;
                playerContex.velocity.y = playerContex.playerData.jumpPower;
            }
        }
        else
        {
            playerContex.velocity.y -= playerContex.playerData.gravity * Time.deltaTime;
            playerContex.master.currentState = PlayerState.Air;
        }
    }
}
