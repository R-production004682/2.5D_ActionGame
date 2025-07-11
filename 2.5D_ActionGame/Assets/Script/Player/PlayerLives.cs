using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private PlayerContex playerContex;

    public void Initialized(PlayerContex contex)
    {
        playerContex = contex;
    }

    private void Start()
    {
        if(uiManager == null)
        {
            Debug.LogError($"PlayerLives.cs, UIManager : {uiManager}");
        }

        uiManager?.UpdateLivesDisplay(playerContex.playerData.lives);
    }

}
