using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "DamageData")]
public class DamageData : ScriptableObject
{
    /*ダメージ計算は、基本的に基礎ダメージを用いるが、
      確率で「基礎ダメージ × 倍率」で算出するクリティカルダメージがある。*/

    [field: Header("基礎ダメージパラメータ")]
    [field: SerializeField, Range(1, 10)] public int deadZoneAmount { get; private set; }
    [field: SerializeField, Range(1, 50)] public int normalAmount { get; private set; }
    [field: SerializeField, Range(1, 50)] public int magicAmount { get; private set; }
    [field: SerializeField, Range(1, 10)] public int trapAmount { get; private set; }

    [field: Header("「基礎ダメージ * 倍率」のような乗算値")]
    [field: SerializeField, Range(0, 100)] public float criticalDamageRate { get; private set; }
    [field: SerializeField, Range(0, 100)] public float magicCriticalDamageRate { get; private set; }
}
