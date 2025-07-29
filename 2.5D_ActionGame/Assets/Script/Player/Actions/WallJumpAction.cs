using Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpAction : IPlayerAction
{
    private PlayerContext playerContext;
    private IPlayerInput input;

    private float wallJumpCooldown = 0.2f;
    private float wallJumpTimer = 0f;
    private Vector3 lastWallNormal = Vector3.zero;

    public void Initialize(PlayerContext context, IPlayerInput input)
    {
        this.playerContext = context;
        this.input = input;
    }

    public void Execute()
    {
        // ジャンプボタンが押されたら、壁ジャンプのクールダウンをリセット
        wallJumpTimer -= Time.deltaTime;

        // プレイヤーが地面に接地している場合は、壁ジャンプの状態をリセット
        if (playerContext.state.isGrounded)
        {
            playerContext.state.isStickingToWall = false;
            playerContext.rigidbody.useGravity = true;
            return;
        }

        // 壁に張り付いている状態でジャンプボタンが押された場合、壁ジャンプを実行
        if (playerContext.state.isStickingToWall)
        {
            if (InputBuffer.Instance.PeekJump())
            {
                PerformWallJump(lastWallNormal);
                wallJumpTimer = wallJumpCooldown;
                return;
            }

            playerContext.rigidbody.velocity = Vector3.zero;
            return;
        }

        // 壁に張り付いていない状態でジャンプボタンが押された場合、壁ジャンプのクールダウンをリセット
        // ただし、壁に近い場合は壁に張り付く
        if (wallJumpTimer <= 0f && IsNearWall(out Vector3 wallNormal))
        {
            StickToWall(wallNormal);
        }
        else
        {
            if (playerContext.state.isStickingToWall)
            {
                playerContext.state.isStickingToWall = false;
                playerContext.rigidbody.useGravity = true;
            }
        }
    }

    /// <summary>
    /// 壁に張り付く処理
    /// </summary>
    /// <param name="wallNormal"></param>
    private void StickToWall(Vector3 wallNormal)
    {
        playerContext.state.isStickingToWall = true;
        playerContext.state.wallNormal = wallNormal;

        playerContext.rigidbody.useGravity = false;
        playerContext.rigidbody.velocity = Vector3.zero;
        playerContext.rigidbody.AddForce(-wallNormal * 2.0f, ForceMode.Force);
        Debug.Log("張り付いたよ");
    }

    /// <summary>
    /// 近くに壁ジャンプ可能な壁があるかを判定
    /// </summary>
    /// <param name="normal"></param>
    /// <returns></returns>
    private bool IsNearWall(out Vector3 normal)
    {
        if(playerContext.CheckSurroundingObject(TagInfo.WALL, out var hit))
        {
            normal = hit.normal;
            lastWallNormal = normal;
            return true;
        }

        normal = Vector3.zero;
        return false;
    }

    /// <summary>
    /// 壁ジャンプ処理
    /// </summary>
    /// <param name="wallNormal"></param>
    private void PerformWallJump(Vector3 wallNormal)
    {
        playerContext.state.isStickingToWall = false;
        playerContext.rigidbody.useGravity = true;

        playerContext.state.jumpCounter = 0;

        // 最後に張り付いていた壁の法線を使用してジャンプ方向を計算
        var jumpDirection = ((wallNormal * -1.0f) + (Vector3.up * 0.3f)).normalized;

        // 強制的に壁から引き剥がす
        playerContext.rigidbody.velocity = jumpDirection * playerContext.playerData.jumpForce * 1.5f;

        Debug.Log($"壁ジャンプ方向: {jumpDirection}");
        Debug.DrawRay(playerContext.rigidbody.position, jumpDirection * 2f, Color.cyan, 1f);
    }

}