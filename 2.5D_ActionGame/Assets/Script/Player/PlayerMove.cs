using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerContex playerContex;

    public void Initialized(PlayerContex contex)
    {
        playerContex = contex;
    }

    /// <summary>
    /// 移動のロジック
    /// </summary>
    public void HandleMove()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var direction = new Vector2(horizontal, 0);
        
        var moveSpeed = (playerContex.master.CurrentState == PlayerState.Air)
                      ? playerContex.playerData.airSpeed 
                      : playerContex.playerData.moveSpeed;

        playerContex.velocity.x = direction.x * moveSpeed;

        // 状態遷移の制御（空中にいる間は更新しない）
        if(playerContex.master.CurrentState != PlayerState.Air && playerContex.master.CurrentState != PlayerState.Jump)
        {
            // 入力が0に近しければIdleへ変化させる
            if (Mathf.Abs(horizontal) < 0.01f)
            {
                playerContex.master.CurrentState = PlayerState.Idle;
            }
            else
            {
                playerContex.master.CurrentState = PlayerState.Move;
            }
        }
    }
}
