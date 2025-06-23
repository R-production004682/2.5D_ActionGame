using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set;}

    private int currentCoinCount;

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

    public void AddCoin(int amount)
    {
        currentCoinCount += amount;
        Debug.Log($"[GameManager] コイン : {currentCoinCount}");
        
        // TODO : UIの表示更新や、サウンドを鳴らす処理は下記に記載する。
    }

    public int GetCoinCount() => currentCoinCount;
}
