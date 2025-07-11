using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "DamageData")]
public class DamageData : ScriptableObject
{
    /*ダメージ計算は、基本的に基礎ダメージを用いるが、
      確率で「基礎ダメージ × 倍率」で算出するクリティカルダメージがある。*/

    [Header("基礎ダメージパラメータ")]
    [SerializeField] public int deadZoneAmount;
    [SerializeField] public int normalAmount;
    [SerializeField] public int magicAmount;
    [SerializeField] public int trapAmount;

    [Header("「基礎ダメージ * 倍率」のような乗算値")]
    [SerializeField] public float criticalDamageRate;
    [SerializeField] public float magicCriticalDamageRate;
}
