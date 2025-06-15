using UnityEngine;
using Const;

public class Coin : Item
{
    protected override void ApplyEffect()
    {
        var value = tag switch 
        {
            CoinInfo.COIN_TAG_NORMAL => CoinInfo.COIN_VALUE_NORMAL,
            CoinInfo.COIN_TAG_SILVER => CoinInfo.COIN_VALUE_SILVER,
            CoinInfo.COIN_TAG_GOLD   => CoinInfo.COIN_VALUE_GOLD,
            _ => 0 // 設定が特にないバグコインの場合は０とする。
        };

        if(value == 0)
        {
            Debug.LogError($"[Bug] 取得したコインにタグの設定がされてません。" +
                $"objectName : {this.name}, objectTag : {tag} ,objectValue : {value}");

#if !UNITY_EDITOR && !DEBUG
            throw new InvalidCoinTagException($"[Bug] 取得したコインにタグの設定がされてません。objectName: {this.name}, objectTag: {tag}");
#endif
        }
        GameManager.Instance.AddCoin(value);
    }
}
