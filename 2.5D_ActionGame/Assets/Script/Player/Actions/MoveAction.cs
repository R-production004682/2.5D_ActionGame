using UnityEngine;
using Const;

public class MoveAction : IPlayerAction
{
    private PlayerContext context;
    private IPlayerInput input;

    public void Initialize(PlayerContext context, IPlayerInput input)
    {
        this.context = context;
        this.input = input;
    }

    public void Execute()
    {
        var horizontal = input.GetHorizontal();

        var defaultSpeed = context.playerData.moveSpeed;
        var airSpeed = context.playerData.airSpeed;
        var appliedSpeed = context.state.isGrounded ? defaultSpeed : airSpeed;

        var velocity = context.rigidbody.velocity;
        velocity.x = horizontal * appliedSpeed;

        // 床速度を合成
        if(context.state.isGrounded)
        {
            velocity.x += context.playerController.GetPlatformVelocity().x;
        }

        // 壁との衝突面に対して滑らせる
        var direction = Vector3.right * Mathf.Sign(velocity.x);
        var maxDistance = context.playerData.wallCheckRaycastLength;

        if (Physics.Raycast(context.rigidbody.position, direction, out var hit, maxDistance))
        {
            var hitObjectTag = hit.collider.gameObject.tag;
            if (hitObjectTag == TagInfo.WALL || hitObjectTag == TagInfo.PLANE)
            {
                // TODO : 壁や、地面に引っかかった際に、無理やり通ると反発して飛んで行ってしまうので、
                // よじ登る動作を実装する必要がある。（現在は必要性が無いため不要）

                Vector3 planeNormal = new Vector3(hit.normal.x, hit.normal.y, 0f).normalized;
                velocity = Vector3.ProjectOnPlane(velocity, planeNormal);
            }
            else
            {
                return;
            }
        }
        context.rigidbody.velocity = velocity;
    }
}
