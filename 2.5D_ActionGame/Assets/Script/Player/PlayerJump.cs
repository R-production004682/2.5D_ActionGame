using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private PlayerContex playerContex;

    public void Initialized(PlayerContex contex)
    {
        playerContex = contex;
    }

    /// <summary>
    /// �W�����v���W�b�N
    /// </summary>
    public void HandlerJump()
    {
        // �ڒn���Ă���Ȃ�΃W�����v�\
        if (playerContex.characterController.isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerContex.master.CurrentState = PlayerState.Jump;
                playerContex.velocity.y = playerContex.playerData.jumpPower;
            }
        }
        else
        {
            // �󒆂ɂ���Ԃ͏d�͓K�p
            playerContex.velocity.y -= playerContex.playerData.gravity * Time.deltaTime;
            playerContex.master.CurrentState = PlayerState.Air;
        }
    }
}
