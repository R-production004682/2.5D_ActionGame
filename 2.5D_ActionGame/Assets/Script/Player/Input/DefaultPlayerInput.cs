using UnityEngine;

public class DefaultPlayerInput : IPlayerInput
{
    public float GetHorizontal() => Input.GetAxis("Horizontal");

    public bool isJumpPressed() => Input.GetKeyDown(KeyCode.Space);
}
