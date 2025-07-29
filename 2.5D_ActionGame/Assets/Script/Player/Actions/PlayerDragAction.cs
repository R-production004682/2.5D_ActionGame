using Const;
using UnityEngine;
using static PlayerStateData;

public class PlayerDragAction : IPlayerAction
{
    private PlayerContext playerContext;
    private IPlayerInput input;
    private Transform heldObjectTransform;

    public void Initialize(PlayerContext context, IPlayerInput input)
    {
        playerContext = context;
        this.input = input;
    }

    public void Execute()
    {
        var state = playerContext.state;

        // 掴む処理
        if (state.holdState == HoldState.None &&
            playerContext.CheckSurroundingObject(TagInfo.MOVABLE_OBJECT, out var hit) &&
            InputBuffer.Instance.ConsumeHoldObject())
        {
            state.holdState = HoldState.Holding;

            heldObjectTransform = hit.collider.transform;
            heldObjectTransform.SetParent(playerContext.rigidbody.transform); // プレイヤーにくっつける
            Debug.Log("物を掴んだ（親子化）");
            return;
        }

        // 離す処理
        if (state.holdState == HoldState.Holding &&
            InputBuffer.Instance.ConsumeReleaseObject())
        {
            state.holdState = HoldState.Releasing;

            if (heldObjectTransform != null)
            {
                heldObjectTransform.SetParent(null);
                heldObjectTransform = null;
                Debug.Log("物を離した（親子解除）");
            }
            return;
        }

        // 状態戻す
        if (state.holdState == HoldState.Releasing)
        {
            state.holdState = HoldState.None;
        }
    }
}
