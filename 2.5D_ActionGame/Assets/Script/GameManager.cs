using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [field: SerializeField] public GameObject playerRespawnPoint { get; private set; }

    private int currentCoinCount;

    public static GameManager Instance { get; private set;}

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if (uiManager == null)
        {
            Debug.LogError($"GameManager.cs, UIManager : {uiManager}");
        }

        if (playerRespawnPoint == null)
        {
            Debug.LogError("playerRespawnPointがGameManagerに設定されていません。");
        }
    }

    public void AddCoins(int amount)
    {
        currentCoinCount += amount;
        UpdateCoins(currentCoinCount);
    }

    public void SubCoins(int amount)
    {
        var afterCoinNum = currentCoinCount - amount;
        if (afterCoinNum < 0)
        {
            Debug.Log($"コインが足りないので、差し引けません。" +
                $"currentCoinCount : {currentCoinCount}, afterCoinNum : {afterCoinNum}");

            UpdateCoins(currentCoinCount);
        }
        else
        {
            Debug.Log($"コインが足りました。 currentCoinCount : {afterCoinNum}");
            UpdateCoins(afterCoinNum);
        }
    }

    public void UpdateCoins(int amount)
    {
        currentCoinCount = amount;
        uiManager?.UpdateCoinDisplay(currentCoinCount);
    }

    public int GetCoinCount()
    {
        return currentCoinCount;
    }
}
