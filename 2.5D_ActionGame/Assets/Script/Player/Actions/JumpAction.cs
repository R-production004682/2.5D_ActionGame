using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : IPlayerAction
{
    private PlayerContext playerContext;
    private bool wasGroundedLastFrame;

    public void Initialize(PlayerContext context, IPlayerInput input)
    {
        this.playerContext = context;
    }

    public void Execute()
    {
        // 接地していてかつ、前フレームで接地していなかった場通す
        if (playerContext.state.isGrounded && !wasGroundedLastFrame)
        {
            playerContext.state.jumpCounter = 0; 
        }

        // 壁ジャンプをしている際はジャンカウンターをリセット
        if (playerContext.state.isStickingToWall)
        {
            // 壁ジャンプの場合はカウンターをリセット
            playerContext.state.jumpCounter = 0;
        }


        // ジャンプの入力があり、ジャンプカウンターがプレイヤーデータのジャンプ回数より少ない場合通す
        if (playerContext.playerController.ConsumeJumpRequest() &&
            playerContext.state.jumpCounter < playerContext.playerData.jumpCount)
        {
            playerContext.rigidbody.AddForce(
                Vector3.up * playerContext.playerData.jumpForce,
                ForceMode.Impulse);

                playerContext.state.jumpCounter++;
        }

        // 前のフレームで地面に接地していたかどうかを記録
        wasGroundedLastFrame = playerContext.state.isGrounded;
    }
}
