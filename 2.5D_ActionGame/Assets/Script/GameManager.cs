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

    public void AddCoin(int amount)
    {
        currentCoinCount += amount;
        
        // TODO : UIの表示更新や、サウンドを鳴らす処理は下記に記載する。
        uiManager?.UpdateCoinDisplay(currentCoinCount);
    }
}
