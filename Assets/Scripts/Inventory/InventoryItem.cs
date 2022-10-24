using System;

[Serializable]
public class InventoryItem
{
    public InventoryItemData data;
    public int slotNum;
    public int stackSize;

    public InventoryItem(InventoryItemData source)
    {
        data = source;
        
        AddToStack();
    }
    public void SlotNum(int slotNum)
    {
        this.slotNum = slotNum;
    }
    public void AddToStack()
    {
        stackSize++;
    }
    public void RemoveFromStack()
    {
        stackSize--;
    }
}
