using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private PlayerContex playerContex;
    private int jumpCounter;
    private bool wasGroundedLastFrame;

    public void Initialized(PlayerContex contex)
    {
        playerContex = contex;
    }

    /// <summary>
    /// ジャンプのロジック
    /// </summary>
    public void HandlerJump()
    {
        var isGround = playerContex.characterController.isGrounded;

        // 着地した瞬間だけ jumpCounter をリセット
        if (isGround && !wasGroundedLastFrame)
        {
            jumpCounter = 0;
        }

        if(IsJumpInputPressed() && CanJump())
        {
            ApplyJump();
        }

        // 重力の適用
        if (!isGround)
        {
            playerContex.velocity.y -= playerContex.playerData.gravity * Time.deltaTime;

            if (0.0f < playerContex.velocity.y)
            {
                playerContex.master.CurrentState = PlayerState.Air;
            }
        }
        else if(0.0f > playerContex.velocity.y)
        {
            playerContex.velocity.y = 0.0f;
            playerContex.master.CurrentState = PlayerState.Idle;
        }

        wasGroundedLastFrame = isGround;
    }

    private bool IsJumpInputPressed() 
        => Input.GetKeyDown(KeyCode.Space);

    private bool CanJump() 
        => jumpCounter < playerContex.playerData.jumpCount;

    private void ApplyJump()
    {
        playerContex.velocity.y = playerContex.playerData.jumpPower;
        jumpCounter++;
        playerContex.master.CurrentState = PlayerState.Jump;
    }
}
