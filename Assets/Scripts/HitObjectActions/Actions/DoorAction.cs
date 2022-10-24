using UnityEngine;
using ConstantsNames;

public class DoorAction : AbstractHitObjectAction
{
	public override void ShowText() 
	{
		//Call function for door
		if (playerActions.hitObject.GetComponent<LockedDoor>().lockedDoor == true)
		{
			playerActions.gamePlayCanvas.LockedDoorOn();//text locked
			if (playerActions.holdingItem?.data?.id == ObjectId.InventoryKey)
			{
				playerActions.gamePlayCanvas.PressFUnlock();//text press to Unlcok
			}
		}
		else if(playerActions.hitObject.GetComponent<LockedDoor>()?.doorOpened==false)
		{
			playerActions.gamePlayCanvas.PressFTextDoorOn();//text press F on		
		}
	}
	public override void DoAction()
    {
		if (playerActions.hitObject.GetComponent<LockedDoor>().lockedDoor == false)
		{
			/*play animation*/
			playerActions.hitObject.GetComponent<Animator>().enabled = true;
			playerActions.hitObject.GetComponent<Animator>().SetBool("closeDoor", false);
			playerActions.hitObject.GetComponent<Animator>().SetBool("openDoor", true);

			if (playerActions.hitObject.GetComponent<LockedDoor>()?.doorOpened == false)
			{
				playerActions.hitObject.GetComponent<LockedDoor>().PlayDoorOpenSound();
			}
		}
		else
		{
			if (playerActions.holdingItem != null) //if player holding item.
			{
				var holdingItem = playerActions.holdingItem;
				playerActions.hitObject.GetComponent<LockedDoor>()?.PlayLockedDoorSound();

				if (holdingItem.data?.id == ObjectId.InventoryKey) //there is a key in inventory and player holding it.
				{
					playerActions.hitObject.GetComponent<LockedDoor>().lockedDoor = false;
					playerActions.gamePlayCanvas.TextOff(); //texts off
					playerActions.hitObject.GetComponent<LockedDoor>().PlayLockOpenSound();
				}
			}
		}
	}
}
