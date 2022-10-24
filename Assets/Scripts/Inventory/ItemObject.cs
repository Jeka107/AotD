using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] public InventoryItemData referenceItem;

    public void OnPickUp() //pick up item.
    {
        FindObjectOfType<InventorySystem>().Add(referenceItem); //add item to inventory system.
        Destroy(gameObject);
    }
}
