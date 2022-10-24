using UnityEngine;

public class ItemPedestal : MonoBehaviour
{
    public delegate void OnCheckPuzzle();
    public static event OnCheckPuzzle onCheckPuzzle;

    [SerializeField] private Vector3 putPosition;
    [SerializeField] private InventoryItemData pedestalCorrectItem;
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] public FinalPuzzleScript actionsHome;
    [SerializeField] private int itemNum;

    private InventoryItem itemOnOedestal;
    private GameObject currentItemOn;
    private bool itemPlaced;

    public void PutItem(InventoryItem holdingItem) //putting item on table.
    {
        itemOnOedestal = holdingItem; //to remember the item placed
        inventorySystem.Remove(holdingItem?.data); //remove from inventory

        if (pedestalCorrectItem == holdingItem?.data) //check if the item is on the correct table.
        {
            actionsHome.order[itemNum] = true;
            onCheckPuzzle?.Invoke(); //to check if puzzle is completed correctly.
        }

        currentItemOn =Instantiate(holdingItem?.data.itemPrefab,putPosition,Quaternion.identity); //item appears on table.
        currentItemOn.GetComponent<Collider>().enabled=false;
  
        itemPlaced = true;
    }

    public void RemoveItem()//remove item from table.
    {
        if (pedestalCorrectItem == itemOnOedestal?.data) //check if the item is on the correct table.
        {
            actionsHome.order[itemNum] = false;
        }

        inventorySystem.Add(itemOnOedestal.data);

        if (currentItemOn!=null)
            Destroy(currentItemOn);
        
        itemPlaced = false;
    }
    public bool GetItemPlaced()
    {
        return itemPlaced;
    }
}
