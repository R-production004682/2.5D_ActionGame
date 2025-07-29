using Const;
using UnityEngine;

public class InputBuffer : MonoBehaviour
{
    public static InputBuffer Instance { get; private set; }

    /* ─────────  バッファ保持用フラグ  ───────── */
    public bool JumpRequested { get; private set; }
    public bool ElevatorUseRequested { get; private set; }
    public bool HoldObjectRequested { get; private set; }
    public bool ReleaseObjectRequested {get; private set; }


    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) JumpRequested = true;

        if (Input.GetKeyDown(KeyCode.W)) ElevatorUseRequested = true;

        if (Input.GetMouseButtonDown(InputType.MOUSE_RIGHT))
        {
            HoldObjectRequested = true;
        }

        if(Input.GetMouseButtonUp(InputType.MOUSE_RIGHT))
        {
            ReleaseObjectRequested = true;
        }

    }

    public bool PeekJump() => JumpRequested;
    public bool ConsumeJump() 
    {
        if (!JumpRequested) 
        {
            return false; 
        }
        JumpRequested = false; 
        return true;
    }

    public bool ConsumeElevator() 
    {
        if (!ElevatorUseRequested) 
        {
            return false; 
        }
        ElevatorUseRequested = false;
        return true; 
    }

    // もし、ホールドがFixedUpdateで拾えなかった場合はバッファを立てる
    public bool ConsumeHoldObject()
    {
        if (!HoldObjectRequested)
        {
            return false;
        }
        HoldObjectRequested = false;
        return true;
    }

    public bool ConsumeReleaseObject()
    {
        if (!ReleaseObjectRequested)
        {
            return false;
        }
        ReleaseObjectRequested = false;
        return true;
    }
}