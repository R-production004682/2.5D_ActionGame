using UnityEngine;
using Const;
public class DefaultPlayerInput : IPlayerInput
{
    public float GetHorizontal() => Input.GetAxis(InputType.HORIZONTAL);
}
