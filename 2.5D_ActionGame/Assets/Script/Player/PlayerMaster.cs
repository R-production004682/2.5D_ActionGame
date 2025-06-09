using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Move,
    Jump,
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

    public PlayerState currentState;
    private PlayerContex contex;

    private void Awake()
    {
        contex = new PlayerContex
        {
           playerData = this.playerData,
           characterController = this.characterController,
           master = this
        };

        move.Initialized(contex);
        jump.Initialized(contex);
    }

    private void Update()
    {
        HandlerState();
    }

    private void HandlerState()
    {
        move.HandleMove();
    }

}
