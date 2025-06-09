using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerContex playerContex;

    public void Initialized(PlayerContex contex)
    {
        playerContex = contex;
    }

    public void HandleMove()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var direction = new Vector2(horizontal, 0);
        var velocity = (direction * playerContex.playerData.moveSpeed) * Time.deltaTime;

        // ���͂�0�ɋ߂������Idle�֕ω�������
        if (Mathf.Abs(horizontal) < 0.01f)
        {
            playerContex.master.currentState = PlayerState.Idle;
        }
        else
        {
            playerContex.master.currentState = PlayerState.Move;
            playerContex.characterController.Move(velocity);
        }
    }
}
