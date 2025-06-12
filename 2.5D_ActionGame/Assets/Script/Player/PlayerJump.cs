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
    /// Player�̃W�����v���W�b�N
    /// </summary>
    public void HandlerJump()
    {
        // Player�̐ڒn����
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
