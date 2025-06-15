using UnityEngine;

public enum ItemType 
{
    Coin,
}

public abstract class Item : MonoBehaviour
{
    public ItemType itemType;

    [SerializeField] protected ItemData itemData;
    protected abstract void ApplyEffect();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
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
