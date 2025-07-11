using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ダメージ周りの処理を統括するクラス
/// </summary>
public abstract class DamageManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] protected PlayerData playerData;
    [SerializeField] protected DamageData damageData;

    protected enum DamageType { DeadZone, Normal, Magic, Trap }

    // 今後、クリティカルダメージの種類が増える可能性があるため、追加しておく。
    protected enum CriticalDamageType { Critical, MagicCritical }

    protected DamageType damageType;
    protected CriticalDamageType criticalDamageType;

    /// <summary>
    /// ダメージを受けたことによる効果を適用
    /// </summary>
    protected abstract void ApplyDamageEffect(PlayerMaster player);

    /// <summary>
    /// ダメージを受けた際のダメージタイプを取得
    /// </summary>
    /// <returns></returns>
    protected abstract DamageType GetDamageType();


    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var amount = GetDamageAmount(GetDamageType());
            var currentLives = playerData.lives - amount;

            PlayerMaster player = other.GetComponent<PlayerMaster>();
            ApplyDamageEffect(player);

            if (0 < currentLives)
            {
                uiManager?.UpdateLivesDisplay(currentLives);
            }
        }
    }

    /// <summary>
    /// ダメージタイプに応じたダメージ量を取得
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private int GetDamageAmount(DamageType type)
    {
        return type switch
        {
            DamageType.DeadZone => damageData.deadZoneAmount,
            DamageType.Normal   => damageData.normalAmount,
            DamageType.Magic    => damageData.magicAmount,
            DamageType.Trap     => damageData.trapAmount,
            _ => 0
        };
    }

}
