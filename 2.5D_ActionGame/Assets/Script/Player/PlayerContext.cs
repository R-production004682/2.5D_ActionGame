using Const;
using UnityEngine;

public class PlayerContext
{
    public PlayerData playerData{ get; }
    public Rigidbody rigidbody { get; }
    public PlayerStateData state { get; }
    public PlayerController playerController { get; }

    /// <summary>
    /// Playerのコンテキストを初期化する。
    /// </summary>
    /// <param name="playerData">Playerのパラメータ群</param>
    /// <param name="rigidbody">重力</param>
    /// <param name="state">Playerの状態</param>
    /// <param name="playerController">Playerの操作統括</param>
    public PlayerContext(
        PlayerData playerData,
        Rigidbody rigidbody,
        PlayerStateData state,
        PlayerController playerController)
    {
        this.playerData = playerData;
        this.rigidbody = rigidbody;
        this.state = state;
        this.playerController = playerController;
    }

    /// <summary>
    /// プレイヤー周辺衝突判定処理
    /// </summary>
    /// <param name="tagToCheck">接触してほしいタグ名</param>
    /// <param name="hitInfo">ヒットした情報を渡す</param>
    /// <returns></returns>
    public bool CheckSurroundingObject(string tagToCheck, out RaycastHit hitInfo)
    {
        var origin = rigidbody.position;
        var checkDistance = PhysicsInfo.PLAYER_CONTACT_RAYCAST_LENGTH;
        Vector3[] directions = { Vector3.left, Vector3.right};

        foreach(var dir in directions)
        {
            var ray = dir * checkDistance;
            var hit = Physics.Raycast(origin, dir, out hitInfo, checkDistance);
            Debug.DrawRay(origin, ray, hit ? Color.red : Color.gray, 1f);

            if (hit && hitInfo.collider.CompareTag(tagToCheck))
            {
                return true;
            }
        }

        hitInfo = default;
        return false;
    }

}
