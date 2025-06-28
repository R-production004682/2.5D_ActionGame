using Const;
using UnityEngine;

public enum ItemType 
{
    Coin,
}

public abstract class Item : MonoBehaviour
{
    public ItemType itemType;

    [SerializeField] protected ItemData itemData;

    /// <summary>
    /// Playerがアイテムを取得した際の効果（機能）をまとめる
    /// </summary>
    protected abstract void ApplyEffect();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(TagInfo.PLAYER))
        {
            PlayerMaster player = other.GetComponent<PlayerMaster>();
            if(player != null)
            {
                ApplyEffect();
                Destroy(this.gameObject);
            }
        }
    }
}
