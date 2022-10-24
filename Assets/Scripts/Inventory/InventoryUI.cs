using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private GameObject slotPrefab;

    private ItemSlot slot;
    private InventoryItem item;

    void Start()
    {
        InventorySystem.OnChange += OnUpdateInventory;
    }
    private void OnDestroy()
    {
        InventorySystem.OnChange -= OnUpdateInventory;
    }

    private void OnUpdateInventory() //update inventory UI.
    {
        foreach (Transform t in transform)//destroy correct inventory UI.
        {
            Destroy(t.gameObject);
        }
        DrawInventory();//drew updated inventory UI.
    }

    public void DrawInventory()
    {
        int slotNum = 1;

        foreach (InventoryItem item in inventorySystem.inventory)
        {
            if(slotNum<= inventorySystem.inventory.Count)//numer of item.
                item.SlotNum(slotNum);

            AddInventorySlot(item);

            if(this.item==item)//selected item.
            {
                slot.GetComponent<ItemSlot>().ActivateSelected();
            }

            slotNum++;
        }
    }
    public void AddInventorySlot(InventoryItem item) //add slot to inventory UI.
    {
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(transform, false);

        slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);
    }
    public void SelectedItem(InventoryItem item)
    {
        foreach (Transform slot in GetComponentInChildren<Transform>())//deavtivate all selected itemslots.
        {
            slot.GetComponent<ItemSlot>().DeActivateSelected();
        }
        foreach (Transform slot in GetComponentInChildren<Transform>())//activate current selected itemslot.
        {
            if (slot.GetComponent<ItemSlot>()?.item==item)//if item is equal to current item then activate selected item.
            {
                this.item = item;
                slot.GetComponent<ItemSlot>().ActivateSelected();//selected itemslot.
                return;
            }
        }
    }
}
