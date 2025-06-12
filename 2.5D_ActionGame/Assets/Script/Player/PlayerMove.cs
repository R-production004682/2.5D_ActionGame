using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerContex playerContex;

    public void Initialized(PlayerContex contex)
    {
        playerContex = contex;
    }

    /// <summary>
    /// �ړ��̃��W�b�N
    /// </summary>
    public void HandleMove()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var direction = new Vector2(horizontal, 0);
        
        var moveSpeed = (playerContex.master.CurrentState == PlayerState.Air)
                      ? playerContex.playerData.airSpeed 
                      : playerContex.playerData.moveSpeed;

        playerContex.velocity.x = direction.x * moveSpeed;

        // ��ԑJ�ڂ̐���i�󒆂ɂ���Ԃ͍X�V���Ȃ��j
        if(playerContex.master.CurrentState != PlayerState.Air && playerContex.master.CurrentState != PlayerState.Jump)
        {
            // ���͂�0�ɋ߂������Idle�֕ω�������
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
