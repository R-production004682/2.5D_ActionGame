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
        wallJumpTimer -= Time.deltaTime;

        if (playerContext.state.isGrounded)
        {
            playerContext.state.isStickingToWall = false;
            playerContext.rigidbody.useGravity = true;
            return;
        }

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

    private void StickToWall(Vector3 wallNormal)
    {
        playerContext.state.isStickingToWall = true;
        playerContext.state.wallNormal = wallNormal;

        playerContext.rigidbody.useGravity = false;
        playerContext.rigidbody.velocity = Vector3.zero;
        playerContext.rigidbody.AddForce(-wallNormal * 2.0f, ForceMode.Force);
        Debug.Log("張り付いたよ");
    }

    private void PerformWallJump(Vector3 wallNormal)
    {
        playerContext.state.isStickingToWall = false;
        playerContext.rigidbody.useGravity = true;

        playerContext.state.jumpCounter = 0;

        var jumpDirection = ((wallNormal * -1.0f) + (Vector3.up * 0.3f)).normalized;

        // 強制的に壁から引き剥がす
        playerContext.rigidbody.velocity = jumpDirection * playerContext.playerData.jumpForce * 1.5f;

        Debug.Log($"壁ジャンプ方向: {jumpDirection}");
        Debug.DrawRay(playerContext.rigidbody.position, jumpDirection * 2f, Color.cyan, 1f);
    }

    private bool IsNearWall(out Vector3 normal)
    {
        var wallCheckDistance = PhysicsInfo.WALL_CHECK_RAYCAST_LENGTH;
        Vector3[] directions = { Vector3.left, Vector3.right };

        foreach (var dir in directions)
        {
            var origin = playerContext.rigidbody.position;
            var ray = dir * wallCheckDistance;

            var hitResult = Physics.Raycast(origin, dir, out var hit, wallCheckDistance);
            Debug.DrawRay(origin, ray, hitResult ? Color.green : Color.red, 1f);

            if (hitResult && hit.collider.CompareTag(TagInfo.WALL))
            {
                normal = hit.normal;
                lastWallNormal = hit.normal;
                return true;
            }
        }

        normal = Vector3.zero;
        return false;
    }
}
