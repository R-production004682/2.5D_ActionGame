using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private PlayerContex playerContex;

    public void Initialized(PlayerContex contex)
    {
        playerContex = contex;
    }

    /// <summary>
    /// ジャンプロジック
    /// </summary>
    public void HandlerJump()
    {
        // 接地しているならばジャンプ可能
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
            // 空中にいる間は重力適用
            playerContex.velocity.y -= playerContex.playerData.gravity * Time.deltaTime;
            playerContex.master.CurrentState = PlayerState.Air;
        }
    }
}
