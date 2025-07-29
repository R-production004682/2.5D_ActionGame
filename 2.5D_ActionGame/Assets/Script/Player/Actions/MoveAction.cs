using UnityEngine;
using Const;

public class MoveAction : IPlayerAction
{
    private PlayerContext playerContext;
    private IPlayerInput input;

    public void Initialize(PlayerContext context, IPlayerInput input)
    {
        this.playerContext = context;
        this.input = input;
    }

    public void Execute()
    {
        var horizontal = input.GetHorizontal();

        var defaultSpeed = playerContext.playerData.moveSpeed;
        var airSpeed = playerContext.playerData.airSpeed;

        // 地上にいる場合は通常の移動速度を適用し、空中にいる場合は空中移動速度を適用する
        var appliedSpeed = playerContext.state.isGrounded ? defaultSpeed : airSpeed;

        // 掴んでいる場合は、掴んでいるオブジェクトの移動速度を適用する
        if (playerContext.state.holdState == PlayerStateData.HoldState.Holding)
        {
            appliedSpeed = playerContext.playerData.objectDragingSpeed;
        }

        var velocity = playerContext.rigidbody.velocity;
        // 床速度を合成
        if(playerContext.state.isGrounded)
        {
            velocity.x += playerContext.playerController.GetPlatformVelocity().x;
        }

        velocity.x = (input.GetHorizontal() * appliedSpeed) + playerContext.playerController.GetPlatformVelocity().x;
        playerContext.rigidbody.velocity = velocity;

        // 壁との衝突面に対して滑らせる
        var direction = Vector3.right * Mathf.Sign(velocity.x);
        var maxDistance = playerContext.playerData.wallCheckRaycastLength;

        if (Physics.Raycast(playerContext.rigidbody.position, direction, out var hit, maxDistance))
        {
            var hitObjectTag = hit.collider.gameObject.tag;
            if (hitObjectTag == TagInfo.WALL || hitObjectTag == TagInfo.PLANE)
            {
                Vector3 planeNormal = new Vector3(hit.normal.x, hit.normal.y, 0f).normalized;
                velocity = Vector3.ProjectOnPlane(velocity, planeNormal);
            }
        }
        playerContext.rigidbody.velocity = velocity;
    }
}
