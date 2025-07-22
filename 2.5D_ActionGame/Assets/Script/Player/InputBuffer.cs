using UnityEngine;

public class InputBuffer : MonoBehaviour
{
    public static InputBuffer Instance { get; private set; }

    /* ─────────  バッファ保持用フラグ  ───────── */
    public bool JumpRequested { get; private set; }
    public bool JumpRequestedThisFrame { get; private set; }
    public bool ElevatorUseRequested { get; private set; }

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
    }

    public bool PeekJump() => JumpRequested || JumpRequestedThisFrame;
    public bool ConsumeJump() { if (!JumpRequested) return false; JumpRequested = false; return true; }

    public bool ConsumeElevator() { if (!ElevatorUseRequested) return false; ElevatorUseRequested = false; return true; }
}