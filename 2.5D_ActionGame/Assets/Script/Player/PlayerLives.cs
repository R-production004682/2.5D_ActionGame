using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private PlayerContex playerContex;
    private int currentLives;

    public void Initialized(PlayerContex contex)
    {
        playerContex = contex;
        currentLives = playerContex.playerData.lives;
    }

    private void Start()
    {
        if(uiManager == null)
        {
            Debug.LogError($"UIManagerが設定されていません : {uiManager}");
        }
        uiManager?.UpdateLivesDisplay(playerContex.playerData.lives);
    }

    /// <summary>
    /// ダメージ計算の結果を返す。
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(int amount)
    {
        currentLives -= amount;

        if (currentLives <= 0)
        {
            currentLives = 0;
            Debug.Log("Playerの体力が0になりました。");
        }

        uiManager?.UpdateLivesDisplay(currentLives);
    }
}
