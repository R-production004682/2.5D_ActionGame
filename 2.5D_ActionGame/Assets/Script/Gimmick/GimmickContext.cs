using UnityEngine;
public class GimmickContext
{
    public GimmickData data { get; }
    public Rigidbody rigidbody { get; }
    public GimmickController controller { get; }

    /// <summary>
    /// Gimmickのコンテキストを初期化。
    /// </summary>
    /// <param name="data"></param>
    /// <param name="rb"></param>
    /// <param name="controller"></param>
    public GimmickContext(
        GimmickData data,
        Rigidbody rb,
        GimmickController controller)
    {
        this.data = data;
        this.rigidbody = rb;
        this.controller = controller;
    }
}
