
public class CollectableAction : AbstractHitObjectAction
{
    public override void ShowText() 
    {
        playerActions.gamePlayCanvas.PressFTextCollectOn(); //show collectable text.
    }
    public override void DoAction()
    {
        if (playerActions.hitObject.TryGetComponent<ItemObject>(out ItemObject item))//if hitobject is from type itemobject.
        {
            item.OnPickUp();//collect item.
            playerActions.gamePlayCanvas.TextOff(); //texts off
        }
        if (playerActions.hitObject.TryGetComponent<NotebookObject>(out NotebookObject notebookItem))//if hitobject is from type notebookItem.
        {
            notebookItem.OnPickUp();//collect notebook item.
            playerActions.gamePlayCanvas.TextOff(); //texts off
        }
    }
}
