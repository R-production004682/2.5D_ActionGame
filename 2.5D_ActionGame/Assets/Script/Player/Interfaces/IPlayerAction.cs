/// <summary>
/// Playerのアクションを定義するインターフェース
/// </summary>
public interface IPlayerAction
{
    void Initialize(PlayerContext context, IPlayerInput input);
    void Execute();
}
