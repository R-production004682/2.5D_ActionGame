using UnityEngine;

/// <summary>
/// Playerの状態管理用データクラス
/// </summary>
public class PlayerStateData
{
    public bool isGrounded { get;set; }
    public int jumpCounter { get; set; }
    public bool isStickingToWall { get; set; }
    public Vector3 wallNormal { get; set; }

    public enum HoldState
    {
        None,
        Holding,
        Releasing
    }

    public HoldState holdState = HoldState.None;
    public bool isHoldingObject => holdState == HoldState.Holding;
}