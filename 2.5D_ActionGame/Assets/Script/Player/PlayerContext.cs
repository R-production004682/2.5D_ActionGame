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
}
