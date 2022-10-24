using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstantsNames;
using UnityEngine.Timeline;

public class FriendAction : AbstractHitObjectAction
{
	public override void ShowText() 
	{
		if (playerActions.hitObject.GetComponent<FreeAlexisScript>()?.alexisIsDown == false)//check which friend.
		{
			if (playerActions.holdingItem?.data?.id == ObjectId.InventoryKnife) //check if holding correct item.
			{
				playerActions.gamePlayCanvas.PressFToFree();//text press F to free
			}
			else
            {
				playerActions.gamePlayCanvas.FindToolToFree();//text prompt to find the right tool	
			}
		}
        else if (playerActions.hitObject.GetComponent<FreeTravisScript>()?.travisIsFree == false)//check which friend.
		{
			if (playerActions.holdingItem?.data?.id == ObjectId.InventorySyringe)//check if holding correct item.
			{
				playerActions.gamePlayCanvas.PressFToFree();//text press F to free
			}
			else
			{
				playerActions.gamePlayCanvas.FindToolToFree();//text prompt to find the right tool	
			}
		}
	}
	public override void DoAction()
    {
		var hitobject = playerActions.hitObject;

		if (playerActions?.holdingItem != null)
		{
            if (playerActions.hitObject.gameObject.name== "Alexis")//check which friend.
			{
				var freeAlexis = hitobject.GetComponent<FreeAlexisScript>();
				var hitobjectAnimator = hitobject.GetComponent<Animator>();

				if (playerActions.holdingItem.data.id == ObjectId.InventoryKnife) //check if player holding correct item.
				{
					freeAlexis.gotKnife = true;

					/*activate animation*/
					hitobjectAnimator.enabled = true;
					hitobjectAnimator.SetBool("FreeAlexis", true);
					freeAlexis.GravityOn();
					freeAlexis.rope.SetActive(false);

					playerActions.gameObject.GetComponent<CheckpointScript>().firstFriend = true;//friend saved,apdate checkpoint.
					/*freeAlexis.alexisTimeline.SetActive(true);*/

					playerActions.gamePlayCanvas.TextOff(); //texts off

					if (playerActions.hitObject.GetComponent<FreeAlexisScript>().alexisIsDown == true)
					{
						playerActions.gamePlayCanvas.TextOff(); //texts off
					}
				}
				else
				{
					playerActions.gamePlayCanvas.TextOff(); //texts off
				}
			}
			else if (playerActions.hitObject.gameObject.name == "Travis")//check which friend.
			{
				var FreeTravis = hitobject.GetComponent<FreeTravisScript>();
				var hitobjectAnimator = hitobject.GetComponent<Animator>();

				if (playerActions.holdingItem.data.id == ObjectId.InventorySyringe) //check if player holding correct item.
				{
					FreeTravis.travisIsFree = true;
					
					/*activate animation*/
					hitobjectAnimator.enabled = true;
					hitobjectAnimator.SetBool("FreeTravis", true);
					FreeTravis.travisTimeline.SetActive(true);

					FreeTravis.coughSound.SetActive(false);
					FreeTravis.finalBlock.SetActive(false);

					playerActions.gameObject.GetComponent<CheckpointScript>().thirdFriend = true; //friend saved,apdate checkpoint.
					playerActions.gamePlayCanvas.TextOff(); //texts off

					if (FreeTravis.travisIsFree == true)
					{
						playerActions.gamePlayCanvas.TextOff(); //texts off
					}
				}
				else
				{
					playerActions.gamePlayCanvas.TextOff(); //texts off
				}
			}
		}
	}
}