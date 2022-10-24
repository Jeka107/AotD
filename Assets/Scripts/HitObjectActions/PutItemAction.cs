
public class PutItemAction : AbstractHitObjectAction
{
    private InventoryItem currentItem;

    public override void ShowText()
    {
        if (playerActions.hitObject.TryGetComponent<ItemPedestal>(out ItemPedestal itemPed)) //check if looking on pedstel table.
        {
            if (itemPed.GetItemPlaced()) //if there is item on table.
            {
                playerActions.gamePlayCanvas.PressFTextCollectOn();
            }
            else
            {
                playerActions.gamePlayCanvas.PressFToRemoveTool();
            }
        }
    }
    public override void DoAction()
    {

        if (playerActions.hitObject.TryGetComponent<ItemPedestal>(out ItemPedestal itemPed) && !itemPed.GetItemPlaced())//check if looking on pedstel table and table is empty.
        {
            currentItem = playerActions.holdingItem;

            if (currentItem != null)
            {
                playerActions.ClearHoldingItem();     //clear what player holding when we put the item on the table.
                itemPed.PutItem(currentItem);        //put the item on the table.
            }
        }
        else if (itemPed.GetItemPlaced())
        {
            itemPed.RemoveItem();
        }
    }
}
