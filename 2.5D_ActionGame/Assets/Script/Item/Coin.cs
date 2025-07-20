using UnityEngine;
using Const;

public class Coin : Item
{
    protected override void ApplyEffect()
    {
        var value = tag switch 
        {
            TagInfo.COIN_NORMAL => CoinInfo.COIN_VALUE_NORMAL,
            TagInfo.COIN_SILVER => CoinInfo.COIN_VALUE_SILVER,
            TagInfo.COIN_GOLD   => CoinInfo.COIN_VALUE_GOLD,
            _ => 0
        };

        if(value == 0)
        {
            Debug.LogError($"[Bug] 取得したコインにタグの設定がされてません。" +
                $"objectName : {this.name}, objectTag : {tag} ,objectValue : {value}");

#if !UNITY_EDITOR && !DEBUG
            throw new InvalidCoinTagException($"[Bug] 取得したコインにタグの設定がされてません。objectName: {this.name}, objectTag: {tag}");
#endif
        }
        GameManager.Instance.AddCoins(value);
    }
}
