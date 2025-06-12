using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Move,
    Jump,
    Air,
    Attack
}

public class PlayerMaster : MonoBehaviour
{
    [Header("Playerの構成要素")]
    [SerializeField] protected PlayerData playerData;
    [SerializeField] public PlayerMove move;
    [SerializeField] public PlayerJump jump;

    [Header("コンポーネント")]
    [SerializeField] private CharacterController characterController;

    private PlayerContex playerContex;

    private PlayerState currentState;
    public PlayerState CurrentState
    {
        get => currentState;
        set
        {
            if(currentState != value)
            {
                Debug.Log($"[State] {currentState} => {value}");
                currentState = value;
            }
        }
    }


    private void Awake()
    {
        playerContex = new PlayerContex
        {
           playerData = this.playerData,
           characterController = this.characterController,
           master = this
        };

        move.Initialized(playerContex);
        jump.Initialized(playerContex);
    }

    private void Update()
    {
        HandlerState();

        playerContex.characterController.Move(playerContex.velocity * Time.deltaTime);
    }

    private void HandlerState()
    {
        move.HandleMove();
        jump.HandlerJump();
    }

}
